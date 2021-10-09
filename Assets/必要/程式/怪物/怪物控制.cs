using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

public class 怪物控制 : MonoBehaviour
{
    public UnityEvent 受傷取消傷害;
    public float 跳躍冷卻cd = 1;
    public float 跳躍高度;
    [Header("攻擊,受傷,死亡")]
    public AudioClip[] 怪物音效;
    public float 偵測距離 = 7f;
    public int 追敵距離 = 12;
    private float 跳躍冷卻 = 0;
    private bool 可跳躍;
    public float 必定發現距離 = 4;
    private float 雙方距離;
    private bool 啟用;
    public Image 圖片hp;
    private GameObject 主角位置;
    protected player控制 playerh;
    private bool 離地狀態;
    protected bool 追敵狀態;
    private bool 跑步狀態;
    protected Collider2D hit;
    [Header("攻擊間隔，自訂攻擊數量")]
    public float[] atkdelay;
    [Header("攻擊完成回復")]
    public float 攻擊回復time = 1;
    [Header("基本能力")]
    [Range(0, 5000)]
    public float hp;
    [Range(0, 1000)]
    private float 最大hp;
    public float atk;
    public float speed = 100;
    [SerializeField]
    protected StateEnemy 動作區;

    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;

    private float 待機timer;
    private float 待機time;
    private float 走路timer;
    private float 走路time;
    private float 攻擊timer;
    [Header("怪物攻擊間隔")]
    public float 攻擊time = 1.3f;

    private Collider2D[] 碰到物1;
    private Collider2D[] hit物排除;

    public Vector2 待機時間 = new Vector2(1, 5);
    public Vector2 走路時間 = new Vector2(3, 6);
    [Header("檢查地板的圓")]
    public Vector2 圓位置 = new Vector2(1.2f, -1.5f);
    public float 圓大小 = 0.3f;
    [Header("檢查離地的方塊")]
    public Vector2 方塊位置 = new Vector2(0, -1.3f);
    public Vector3 方塊大小 = new Vector3(1, 0.4f, 0.2f);
    [Header("檢查牆壁的方塊")]
    public Vector2 牆壁位置 = new Vector2(0.6f, 0f);
    public Vector3 牆壁大小 = new Vector3(1, 0.5f, 0.2f);
    [Header("檢查底下牆壁的方塊")]
    public Vector2 底下位置 = new Vector2(0.6f, 0.5f);
    public Vector3 底下大小 = new Vector3(1, 0.5f, 0.2f);

    private bool 生成過了bo;
    //----------------------------------
    public enum StateEnemy
    {
        待機, 走路, 追敵, 攻擊, 死亡,
    }
    //----------------------------------
    void Start()
    {
        最大hp = hp;
        生成過了bo = false;
        啟用 = false;
        追敵狀態 = false;
        待機time = Random.Range(待機時間.x, 待機時間.y);
        主角位置 = GameObject.Find("主角");
        playerh = GameObject.Find("主角").GetComponent<player控制>();
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }
    protected virtual void Update()
    {
        狀態();
        啟用狀態();
    }
    private void FixedUpdate()
    {
        檢查地板();
        fix重力();
        檢查牆壁跳躍();
    }
    //----------------------------------
    private void 啟用狀態()
    {
        雙方距離 = Vector2.Distance(主角位置.transform.position, transform.position);
        if (Mathf.Abs(雙方距離) < 偵測距離 && !生成過了bo)
        {
            生成過了bo = true;
            ani.SetTrigger("生成");
            Invoke("已經生成", 1);
        }
        if (生成過了bo)
        {
            啟用 = true;
        }
        if(雙方距離 > 必定發現距離)
        {
            if (主角位置.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (主角位置.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = Vector3.up * 180;
            }
        }
    }
    private void 已經生成()
    {
        ani.SetBool("已經生成", true);
    }
    private void 狀態()
    {
        switch (動作區)
        {
            case StateEnemy.待機:
                待機();
                break;
            case StateEnemy.走路:
                走路();
                break;
            case StateEnemy.追敵:
                追敵跑();
                break;
            case StateEnemy.攻擊:
                攻擊預備();
                break;
            case StateEnemy.死亡:

                break;
        }
    }
    private void 檢查地板()
    {
        碰到物1 = Physics2D.OverlapCircleAll(transform.position + transform.right * 圓位置.x + transform.up * 圓位置.y, 圓大小);
        Collider2D 碰地 = Physics2D.OverlapBox(transform.position + transform.right * 方塊位置.x + transform.up * 方塊位置.y, 方塊大小, 0, 1 << 3);
        //碰到物2 = Physics2D.OverlapCircleAll(transform.position + transform.right * 地板1位置.x + transform.up * 地板1位置.y, 地板1大小);
        //碰到物3 = Physics2D.OverlapCircleAll(transform.position + transform.right * 地板2位置.x + transform.up * 地板2位置.y, 地板2大小);
        hit物排除 = 碰到物1.Where(x => x.name != "地板" && x.name != "普通跳台" && x.name != "可穿越跳台" && x.name != "主角").ToArray();
        if (碰地)
        {
            離地狀態 = false;
            ani.SetBool("跳躍", false);
        }
        else
        {
            離地狀態 = true;
        }
        if ((碰到物1.Length == 0 || hit物排除.Length > 0) && !離地狀態 && !追敵狀態)
        {
            //print("沒有地板或者有障礙物");
            轉彎();
        }
    }
    private void 檢查牆壁跳躍()
    {
        Collider2D[] 上範圍 = Physics2D.OverlapBoxAll(transform.position + transform.right * 牆壁位置.x + transform.up * 牆壁位置.y, 牆壁大小, 1 << 3);
        Collider2D[] 上限制 = 上範圍.Where(x => x.tag == "地板" || x.tag == "Player").ToArray();
        Collider2D[] 下範圍 = Physics2D.OverlapBoxAll(transform.position + transform.right * 底下位置.x + transform.up * 底下位置.y, 底下大小, 1 << 3);
        Collider2D[] 下限制 = 下範圍.Where(x => x.tag == "地板").ToArray();
        if (上限制.Length == 0 && 下限制.Length > 0)
        {
            if (!離地狀態 && 可跳躍)
            {
                if (動作區 == StateEnemy.走路 || 動作區 == StateEnemy.追敵)
                {
                    rig.AddForce(new Vector2(0, 跳躍高度 * 10000));
                    ani.SetBool("跳躍", true);
                    可跳躍 = false;
                }
            }
        }
        if (跳躍冷卻 < 跳躍冷卻cd)
        {
            跳躍冷卻 += 1 * Time.deltaTime;
        }
        else
        {
            跳躍冷卻 = 0;
            可跳躍 = true;
        }
    }
    private void fix重力()
    {
        if (動作區 == StateEnemy.走路)
        {
            rig.velocity = transform.right * speed * Time.fixedDeltaTime + Vector3.up * rig.velocity.y;
        }
        if (動作區 == StateEnemy.追敵)
        {
            rig.velocity = transform.right * speed * Time.fixedDeltaTime + Vector3.up * rig.velocity.y;
        }
        if (動作區 == StateEnemy.待機)
        {
            rig.velocity = Vector3.up * rig.velocity.y;
        }
        if (動作區 == StateEnemy.攻擊)
        {
            rig.velocity = Vector3.up * rig.velocity.y;
        }
        if (動作區 == StateEnemy.死亡)
        {
            rig.velocity = Vector2.zero;
        }
    }
    private void 待機()
    {
        if (啟用)
        {
            //rig.constraints = RigidbodyConstraints2D.FreezeAll;
            if (待機timer < 待機time)
            {
                待機timer += Time.deltaTime;
                rig.velocity = Vector2.zero; //消除滑行
                ani.SetBool("走路", false);
            }
            else
            {
                待機timer = 0;
                動作區 = StateEnemy.走路; //前往狀態
                走路time = Random.Range(走路時間.x, 走路時間.y);
                隨機方向();
            }
        }
    }
    private void 走路()
    {
        if (啟用)
        {
            if (走路timer < 走路time)
            {
                走路timer += Time.deltaTime;
                //print("走路中");
                ani.SetBool("走路", true);
            }
            else
            {
                走路timer = 0;
                動作區 = StateEnemy.待機;
                待機time = Random.Range(待機時間.x, 待機時間.y);
            }
        }
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void 追敵跑()
    {
        //print("雙方距離" + 雙方距離);
        if (追敵狀態)
        {
            if (主角位置.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (主角位置.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = Vector3.up * 180;
            }
            if (雙方距離 > 5)
            {
                跑步狀態 = true;
                speed = 200;
                ani.SetBool("跑步", true);
            }
            else if (雙方距離 <= 3)
            {
                跑步狀態 = false;
                speed = 100;
                ani.SetBool("跑步", false);
            }
            if (雙方距離 > 追敵距離)
            {
                追敵狀態 = false;
                speed = 100;
                ani.SetBool("跑步", false);
                跑步狀態 = false;
                動作區 = StateEnemy.走路;
                隨機方向();
            }
        }
    }
    private void 隨機方向()
    {
        int rand = Random.Range(0, 2); //= 0 or 1 !=2
        if (rand == 0) transform.eulerAngles = Vector2.up * 0;
        if (rand == 1) transform.eulerAngles = Vector2.up * 180;

    }
    public void 轉彎()
    {
        float y = transform.eulerAngles.y;
        if (y == 0) transform.eulerAngles = Vector3.up * 180;
        else transform.eulerAngles = Vector3.zero;

    }
    protected virtual void 攻擊預備()
    {
        if (啟用)
        {
            if (攻擊timer < 攻擊time)
            {
                攻擊timer += Time.deltaTime;
                ani.SetBool("走路", false);
            }
            else
            {
                攻擊();
            }
        }
    }
    protected virtual void 攻擊()
    {
        if (追敵狀態 && 跑步狀態)
        {
            ani.SetTrigger("攻擊2");
            aud.clip = 怪物音效[0];
            aud.Play();
        }
        else
        {
            ani.SetTrigger("攻擊1");
            aud.clip = 怪物音效[0];
            aud.Play();
        }
        攻擊timer = 0;
    }
    public void 受傷(float 攻擊力)
    {
        受傷取消傷害.Invoke();
        動作區 = StateEnemy.追敵;
        hp -= 攻擊力;
        ani.SetTrigger("受傷");
        aud.clip = 怪物音效[1];
        aud.Play();
        圖片hp.fillAmount = (hp / 最大hp);
        if (hp <= 0)
        {
            死亡();
        }
        if (!追敵狀態)
        {
            動作區 = StateEnemy.追敵;
            追敵狀態 = true;
        }
    }
    private void 死亡()
    {
        aud.clip = 怪物音效[2];
        aud.Play();
        hp = 0;
        ani.SetBool("死亡", true);
        動作區 = StateEnemy.死亡;
        GetComponent<CapsuleCollider2D>().enabled = false;
        //rig.velocity = Vector2.zero;
        rig.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        //掉落物();
    }
    public void 復活怪物()
    {
        ani.SetBool("死亡", false);
        動作區 = StateEnemy.待機;
        GetComponent<CapsuleCollider2D>().enabled = true;
        hp = 最大hp;
        圖片hp.fillAmount = (hp / 最大hp);
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        enabled = true;
    }
    protected virtual void OnDrawGizmos() //畫圓
    {
        Gizmos.color = new Color(1, 0.3f, 0.3f, 0.3f);
        Gizmos.DrawSphere(transform.position + transform.right * 圓位置.x + transform.up * 圓位置.y, 圓大小);
        Gizmos.DrawCube(transform.position + transform.right * 方塊位置.x + transform.up * 方塊位置.y, 方塊大小);
        Gizmos.DrawCube(transform.position + transform.right * 牆壁位置.x + transform.up * 牆壁位置.y, 牆壁大小);
        Gizmos.DrawCube(transform.position + transform.right * 底下位置.x + transform.up * 底下位置.y, 底下大小);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "即死陷阱")
        {
            死亡();
        }
    }
}
