using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

//吃字== fn + insert(Delete)

public class player控制 : MonoBehaviour
{
    private float 浮力 = 0.1f;
    public bool 騎乘中 = false;
    重生點管理 重生點管理;
    private bool 可下蹲穿越bo = false;
    public bool 可使用E = true;
    private 攝影機跟隨 攝影機跟隨;

    #region cd
    public int 地刺陷阱傷害 = 10;
    public float 自己受傷冷卻cd = 1;
    private float 自己受傷冷卻time;
    private bool 自己受傷bo;
    public bool 無敵 = false;
    public float 無敵時間 = 0.4f;
    private bool isatk1;
    private bool isatk2;
    private bool isatk3;
    public float isatk1cd = 0.9f;
    public float isatk2cd = 1.9f;
    public float isatk3cd = 0.9f;
    public float isatk1time = 0;
    public float isatk2time = 0;
    public float isatk3time = 0;
    private bool 受傷冷卻1;
    private bool 受傷冷卻2;
    private bool 受傷冷卻3;
    #endregion

    #region public設定
    private GameObject 摔傷;
    [Header("移動速度"), Tooltip("角色移動速度")]
    private float Movespeed = 100;
    [Header("跳躍高度"), Tooltip("角色跳躍高度")]
    private float JumpHeight = 4;
    [Header("生命值")]
    public float hp = 100;
    [Header("攻擊力")]
    public float ATK = 20;
    [Header("魔力")]
    public float mp = 0;
    [Header("剩餘技能點數")]
    public int SkillPoints = 0;
    [Header("重力值")]
    private float gravity = 0.55f;
    [Header("體力值")]
    public float 體力 = 10;
    [Header("攻擊區域")]
    private Vector2 v2攻擊區域 = new Vector2(1.2f, 0);
    private Vector3 v3尺寸 = new Vector3(1.2f, 2, 0.1f);
    private float 碰不到地板a;
    private float 碰不到地板cd = 1f;
    public Rigidbody2D rig;
    private CapsuleCollider2D 碰撞區域;
    //public SpriteRenderer 角色圖;
    private Animator ani;
    [Header("檢查腳底觸碰中心點")]
    private Vector3 foot_touch_area = new Vector3(0, -0.6f, 0.1f);
    [Range(0, 2)]
    private float trouch_radius = 0.8f;
    [Header("檢查繩索")]
    private Vector3 繩索位置 = new Vector3(0, 1, 0.1f);
    [Range(0, 2)]
    private float 繩索大小 = 0.7f;
    [Header("是否碰到地板")]
    private bool step_floor;
    private bool 碰到牆壁;
    public AudioSource 攻擊音效;
    public AudioClip[] 攻擊音效編號;
    public AudioSource 行動音效;
    public AudioClip[] 行動音效編號;
    public AudioSource 狀態音效;
    public AudioClip[] 狀態音效編號;
    public AudioClip 墜落音效;
    private bool 墜落音效one;
    private bool 爬牆狀態;
    [Header("ui文字")]
    public Text 血量hp;
    [Header("ui血量")]
    public Image 圖片hp;
    public float 最大hp;
    private bool 墜落扣血;
    private bool 碰牆壁;
    private float 扣血值 = 0;
    #endregion
    //------------------------------------------------
    #region private設定
    private float LeftRight;
    private bool 有武器bo;
    private bool 水中;
    private bool 繩索上;
    #endregion
    //------------------------------------------------
    #region st/up/fixup設定
    private void Start()
    {
        攝影機跟隨 = GameObject.Find("Main Camera").GetComponent<攝影機跟隨>();
        跳躍一次 = true;
        摔傷 = GameObject.Find("摔傷");
        if (摔傷 != null)
        {
            摔傷.SetActive(false);
        }
        重生點管理 = GameObject.Find("重生點管理").GetComponent<重生點管理>();
        碰撞區域 = GetComponent<CapsuleCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        ani.SetBool("有武器bo", false);
        攻擊音效 = 攻擊音效.gameObject.GetComponent<AudioSource>();
        行動音效 = 行動音效.gameObject.GetComponent<AudioSource>();
        狀態音效 = 狀態音效.gameObject.GetComponent<AudioSource>();
        有武器bo = false;
        碰撞區域.size = new Vector2(1, 1.85f);
        碰撞區域.offset = new Vector2(0, -0.3f);
        最大hp = hp;
    }
    private void Update()
    {
        jump();
        攻擊();
        拿出武器();
        print("無敵" + 無敵);
        print("可使用E" + 可使用E);
    }
    private void FixedUpdate()
    {
        move();
        爬牆();
        衝刺();
        flip();
        墜落狀態();
        下蹲穿越();
        走跑音效判斷();
        走跑音效();
    }
    #endregion
    //------------------------------------------------
    #region 事件設定
    /// <summary>
    /// 移動
    /// </summary>
    private void move()                  //角色移動 + rig負重力
    {
        //Vector2 playermove = transform.position + new Vector3(LeftRight, -gravity, 0)*Movespeed*Time.fixedDeltaTime;
        //rig.MovePosition(playermove);
        if (可使用E)
        {
            if (水中)
            {
                rig.velocity = new Vector2(LeftRight * Movespeed / 2 * Time.fixedDeltaTime, rig.velocity.y + 浮力);
                if (Input.GetKey(KeyCode.W))
                {
                    浮力 = 0.4f;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    浮力 = -0.1f;
                }
                else
                {
                    浮力 = 0.1f;
                }
                碰撞區域.size = new Vector2(1, 0.8f);
                碰撞區域.offset = new Vector2(0, -0.3f);
            }
            else
            {
                rig.velocity = new Vector2(LeftRight * Movespeed * Time.fixedDeltaTime, rig.velocity.y - gravity);
                ani.SetBool("跑步", LeftRight != 0);
            }
        }
    }
    /// <summary>
    /// 跳躍
    /// </summary>
    private void jump()                  //角色跳躍
    {
        Collider2D[] 地板範圍 = Physics2D.OverlapCircleAll(transform.position + foot_touch_area, trouch_radius, 1 << 3);
        Collider2D[] 地板限制 = 地板範圍.Where(x => x.tag == "地板" || x.tag == "可穿越跳台" || x.tag == "即死陷阱" || x.tag == "不可跳躍的地板").ToArray();
        if (地板限制.Length > 0)
        {
            ani.SetBool("跳躍", false);
            ani.SetBool("此方塊可跳躍", true);
            ani.SetBool("碰到地板", true);
            step_floor = true;
        }
        else if (騎乘中)
        {
            step_floor = true;
        }
        else
        {
            ani.SetBool("此方塊可跳躍", false);
            step_floor = false;
            ani.SetBool("碰到地板", false);
        }
        Collider2D[] 牆壁 = 地板範圍.Where(x => x.tag == "牆壁").ToArray();
        if (牆壁.Length > 0)
        {
            碰牆壁 = true;
        }
        else
        {
            碰牆壁 = false;
        }
        if (Input.GetKeyDown(KeyCode.K) && step_floor && 可使用E && !爬牆狀態 && !Input.GetKey(KeyCode.S))
        {
            rig.AddForce(new Vector2(0, JumpHeight * 10000));
            ani.SetBool("跳躍", true);
        }
        Collider2D[] 水中判斷 = 地板範圍.Where(x => x.tag == "水中").ToArray();
        if (水中判斷.Length > 0)
        {
            水中 = true;
            ani.SetBool("在水裡", true);
        }
        else
        {
            水中 = false;
            ani.SetBool("在水裡", false);
        }
        Collider2D[] 陷阱 = 地板範圍.Where(x => x.tag == "陷阱").ToArray();
        if (陷阱.Length > 0)
        {
            if (!自己受傷bo)
            {
                受傷(地刺陷阱傷害);
                自己受傷bo = true;
            }
            if (自己受傷bo)
            {
                if (自己受傷冷卻time < 自己受傷冷卻cd)
                {
                    自己受傷冷卻time += 1 * Time.deltaTime;
                }
                else
                {
                    自己受傷冷卻time = 0;
                    自己受傷bo = false;
                }
            }
        }
        Collider2D[] 繩索範圍 = Physics2D.OverlapCircleAll(transform.position + 繩索位置, 繩索大小, 1 << 3);
        Collider2D[] 繩索 = 繩索範圍.Where(x => x.tag == "繩索").ToArray();
        if (繩索.Length > 0)
        {
            ani.SetBool("繩索", true);
            繩索上 = true;
        }
        else
        {
            ani.SetBool("繩索", false);
            繩索上 = false;
        }
        //Vector3 方塊大小 = new Vector3(1, 0.4f, 0.2f);
        //Collider2D 碰怪物身上 = Physics2D.OverlapBox(transform.position + transform.right * 0 + transform.up * -1.3f, 方塊大小, 0, 1 << 8);
        //if (碰怪物身上)
        //{
        //    step_floor = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.K) && 碰到牆壁 && 可使用E)
        //{
        //    rig.AddForce(new Vector2(0, JumpHeight * 10000));
        //    ani.SetBool("跳躍", true);
        //}
    }
    private void 衝刺()
    {
        if (Input.GetKey(KeyCode.L) && 可使用E && step_floor && !水中)
        {
            ani.SetBool("L", true);
            if (有武器bo)
            {
                Movespeed = 500;
            }
            else
            {
                Movespeed = 600;
            }
        }
        else
        {
            ani.SetBool("L", false);
        }
        if (!step_floor)
        {
            ani.SetBool("L", false);
        }
        if (step_floor && !Input.GetKey(KeyCode.L))
        {
            Movespeed = 400;
        }
        //
        if (Input.GetKey(KeyCode.S))
        {
            ani.SetBool("下蹲", true);
        }
        else
        {
            ani.SetBool("下蹲", false);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.L) && !水中)
        {
            碰撞區域.size = new Vector2(0.7f, 0.7f);
            碰撞區域.offset = new Vector2(0, -0.5f);
        }
        else if (!水中)
        {
            碰撞區域.size = new Vector2(1, 1.85f);
            碰撞區域.offset = new Vector2(0, -0.3f);
        }
    }
    private void 攻擊()
    {
        if (Input.GetKeyDown(KeyCode.J) && 可使用E && 有武器bo && !Input.GetKey(KeyCode.W) && !isatk1 && !Input.GetKey(KeyCode.L))
        {
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                ani.SetTrigger("下砍");
                isatk1 = true;
                攻擊音效.clip = 攻擊音效編號[0];
                攻擊音效.Play();
            }
            Collider2D hit = Physics2D.OverlapBox(transform.position + transform.right * v2攻擊區域.x + transform.up * v2攻擊區域.y, v3尺寸, 0, 1 << 8);
            if (hit)
            {
                if (hit.tag == "怪物" && !受傷冷卻1)
                {
                    hit.GetComponent<怪物控制>().受傷(ATK);
                    受傷冷卻1 = true;
                }
                //print(hit.name);
                //StartCoroutine(cameraa.晃動());
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.W) && 可使用E && 有武器bo && !isatk2 && !Input.GetKey(KeyCode.L))
        {
            isatk2 = true;
            攻擊音效.clip = 攻擊音效編號[1];
            攻擊音效.Play();
            ani.SetTrigger("上砍");
            無敵時間 = 0.4f;
            StartCoroutine(無敵狀態ie());
            Collider2D hit = Physics2D.OverlapBox(transform.position + transform.right * v2攻擊區域.x + transform.up * v2攻擊區域.y, v3尺寸, 0, 1 << 8);
            if (hit)
            {
                if (hit.tag == "怪物" && !受傷冷卻2)
                {
                    hit.GetComponent<怪物控制>().受傷(ATK);
                    受傷冷卻2 = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.A) && 可使用E && 有武器bo && !isatk3 && !Input.GetKey(KeyCode.L))
        {
            isatk3 = true;
            攻擊音效.clip = 攻擊音效編號[0];
            攻擊音效.Play();
            ani.SetTrigger("轉身砍");
            Collider2D hit = Physics2D.OverlapBox(transform.position + transform.right * v2攻擊區域.x + transform.up * v2攻擊區域.y, v3尺寸, 0, 1 << 8);
            if (hit)
            {
                if (hit.tag == "怪物" && !受傷冷卻3)
                {
                    hit.GetComponent<怪物控制>().受傷(ATK);
                    受傷冷卻3 = true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.D) && 可使用E && 有武器bo && !isatk3 && !Input.GetKey(KeyCode.L))
        {
            isatk3 = true;
            攻擊音效.clip = 攻擊音效編號[0];
            攻擊音效.Play();
            ani.SetTrigger("轉身砍");
            Collider2D hit = Physics2D.OverlapBox(transform.position + transform.right * v2攻擊區域.x + transform.up * v2攻擊區域.y, v3尺寸, 0, 1 << 8);
            if (hit)
            {
                if (hit.tag == "怪物" && !受傷冷卻3)
                {
                    hit.GetComponent<怪物控制>().受傷(ATK);
                    受傷冷卻3 = true;
                }
            }
        }
        if (isatk1)
        {
            if (isatk1time < isatk1cd)
            {
                isatk1time += 1 * Time.deltaTime;
            }
            else
            {
                isatk1time = 0;
                isatk1 = false;
                受傷冷卻1 = false;
            }
        }
        if (isatk2)
        {
            if (isatk2time < isatk2cd)
            {
                isatk2time += 1 * Time.deltaTime;
            }
            else
            {
                isatk2time = 0;
                isatk2 = false;
                受傷冷卻2 = false;
            }
        }
        if (isatk3)
        {
            if (isatk3time < isatk3cd)
            {
                isatk3time += 1 * Time.deltaTime;
            }
            else
            {
                isatk3time = 0;
                isatk3 = false;
                受傷冷卻3 = false;
            }
        }
    }
    private void 拿出武器()
    {
        if (有武器bo && Input.GetKeyDown(KeyCode.Q) && 可使用E)
        {
            ani.SetTrigger("收拿武器");
            有武器bo = false;
            攻擊音效.clip = 攻擊音效編號[3];
            攻擊音效.Play();
        }
        else if (!有武器bo && Input.GetKeyDown(KeyCode.Q) && 可使用E)
        {
            ani.SetTrigger("收拿武器");
            有武器bo = true;
            攻擊音效.clip = 攻擊音效編號[2];
            攻擊音效.Play();
        }
        if (有武器bo)
        {
            ani.SetBool("有武器bo", true);
        }
        else
        {
            ani.SetBool("有武器bo", false);
        }
    }
    private void 墜落狀態()
    {
        if (!step_floor && !碰牆壁 && !水中 && !繩索上)
        {
            碰不到地板a += 1 * Time.fixedDeltaTime;
            if (!墜落一次)
            {
                墜落一次 = true;
            }
            //print("碰不到地板a" + 碰不到地板a);
        }
        else if (水中||繩索上)
        {
            扣血值 = 0;
            碰不到地板a = 0;
        }
        else
        {
            扣血值 += 碰不到地板a * 5;
            ani.SetBool("墜落", false);
            //print("碰不到地板a" + 碰不到地板a);
        }
        if (step_floor)
        {
            碰不到地板a = 0;
            墜落一次 = false;
        }
        if (碰不到地板a > 碰不到地板cd)
        {
            ani.SetBool("墜落", true);
            墜落扣血 = true;
            if (!墜落音效one)
            {
                墜落音效one = true;
                狀態音效.PlayOneShot(墜落音效, 0.8f);
            }
        }
        if (墜落扣血 && step_floor)
        {
            if (hp != 0)
                受傷(Mathf.Ceil(扣血值));
            摔傷.SetActive(true);
            Invoke("摔傷計時", 1);
            墜落扣血 = false;
            扣血值 = 0;
            墜落音效one = false;
        }
    }
    private void 摔傷計時()
    {
        摔傷.SetActive(false);
    }
    private void 爬牆()
    {
        if (Input.GetKey(KeyCode.W) && 可使用E)
        {
            ani.SetBool("W", true);
        }
        else
        {
            ani.SetBool("W", false);
        }
        if (碰到牆壁)
        {
            ani.SetBool("碰到牆面", true);
        }
        else
        {
            ani.SetBool("碰到牆面", false);
        }
        if (Input.GetKey(KeyCode.W) && 碰到牆壁 && 可使用E)
        {
            爬牆狀態 = true;
            rig.AddForce(new Vector2(0, 1700));
        }
        else
        {
            爬牆狀態 = false;
        }
    }
    private void 下蹲穿越()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.K) && 可下蹲穿越bo && 可使用E)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("下蹲穿越結束", 0.24f);
            rig.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
    private void 下蹲穿越結束()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
        rig.constraints = RigidbodyConstraints2D.FreezeAll;
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void 受傷(float 傷害)
    {
        if (!無敵)
        {
            StartCoroutine(攝影機跟隨.晃動());
            狀態音效.clip = 狀態音效編號[1];
            狀態音效.loop = false;
            狀態音效.Play();
            hp -= 傷害;
            ani.SetTrigger("受傷");
            if (hp <= 0)
            {
                死亡();
            }
            血量hp.text = "血量 " + hp;
            圖片hp.fillAmount = (hp / 最大hp);
        }
        else hp -= 0;
    }
    private void 死亡()
    {
        狀態音效.clip = 狀態音效編號[2];
        狀態音效.loop = false;
        狀態音效.Play();
        hp = 0;
        ani.SetBool("死亡bo", true);
        GetComponent<CapsuleCollider2D>().enabled = false;
        rig.velocity = Vector2.zero;
        rig.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        Invoke("復活", 2f);
    }
    private void 復活()
    {
        enabled = true;
        ani.SetBool("死亡bo", false);
        GetComponent<CapsuleCollider2D>().enabled = true;
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        hp = 最大hp;
        重生點管理.復活();
        血量hp.text = "血量 " + hp;
        圖片hp.fillAmount = (hp / 最大hp);
    }
    public void E回復()
    {
        可使用E = true;
    }
    IEnumerator 無敵狀態ie()
    {
        無敵 = true;
        rig.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(無敵時間);
        GetComponent<CapsuleCollider2D>().enabled = true;
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        無敵 = false;
    }
    public IEnumerator 復活無敵time()
    {
        無敵 = true;
        yield return new WaitForSeconds(無敵時間);
        無敵 = false;
    }
    private void flip()                  //角色翻轉
    {
        LeftRight = Input.GetAxis("Horizontal");
        if (LeftRight > 0)//看右
        {
            //角色圖.flipX = false;
            if(!繩索上)
            transform.eulerAngles = Vector2.up * 0;
        }
        if (LeftRight < 0)  //看左
        {
            //角色圖.flipX = true;
            if (!繩索上)
            transform.eulerAngles = Vector2.up * 180;
        }
    }
    //繪製圖示事件
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        //Gizmos.DrawSphere(transform.position,2); //(中心點，半徑)
        Gizmos.DrawSphere(transform.position + foot_touch_area, trouch_radius);
        Gizmos.DrawSphere(transform.position + 繩索位置, 繩索大小);
        Gizmos.color = new Color(1, 0.3f, 0.1f, 0.3f);
        Gizmos.DrawCube(transform.position + transform.right * v2攻擊區域.x + transform.up * v2攻擊區域.y, v3尺寸);
    }
    //碰 到 牆面
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "牆壁")
        {
            碰到牆壁 = true;
        }
        else if (collision.transform.tag == "可穿越跳台")
        {
            可下蹲穿越bo = true;
        }
        if (collision.transform.tag == "怪物")
        {
            ani.SetBool("碰到地板", true);
            step_floor = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "牆壁")
        {
            碰到牆壁 = false;
        }
        else if (collision.transform.tag == "可穿越跳台")
        {
            可下蹲穿越bo = false;
        }
    }
    #endregion
    //------------------------------------------------
    #region 音效控制
    private bool 跑步判斷;
    private bool 衝刺判斷;
    private bool 跑步一次;
    private bool 衝刺一次;
    private bool 跳躍判斷;
    private bool 跳躍一次;
    private bool 爬牆判斷;
    private bool 爬牆一次;
    private bool 跳台上;
    private bool x, y;
    private void 走跑音效判斷()
    {
        if (LeftRight != 0 && 可使用E && step_floor)
        {
            y = false;
            跑步判斷 = true;
        }
        else
        {
            跑步判斷 = false;
            跑步一次 = false;
            if (!跳躍判斷 && !爬牆判斷 || hp == 0)
            {
                行動音效.Stop();
            }
            if (!y)
            {
                y = true;
                衝刺一次 = false;
            }
        }

        if (跑步判斷 && Input.GetKey(KeyCode.L))
        {
            衝刺判斷 = true;
            x = false;
        }
        else if (!Input.GetKey(KeyCode.L))
        {
            衝刺判斷 = false;
            衝刺一次 = false;
            if (!x)
            {
                跑步一次 = false;
                x = true;
            }
        }
        if (!step_floor) 跳躍判斷 = true;
        else
        {
            跳躍判斷 = false;
            跳躍一次 = false;
        }
        if (爬牆狀態) 爬牆判斷 = true;
        else
        {
            爬牆判斷 = false;
            爬牆一次 = false;
        }
        //---
    }
    private Collider2D[] 碰跳台範圍;
    private Collider2D[] 碰跳台;
    private void 走跑音效()
    {
        碰跳台範圍 = Physics2D.OverlapCircleAll(transform.position + foot_touch_area, trouch_radius, 1 << 3);
        碰跳台 = 碰跳台範圍.Where(x => x.tag == "可穿越跳台").ToArray();
        if (碰跳台.Length > 0)
        {
            跳台上 = true;
        }
        else
        {
            跳台上 = false;
        }
        if (跑步判斷 && !跑步一次 && !衝刺判斷 && 跳台上)   //台上走
        {
            行動音效.clip = 行動音效編號[4];
            行動音效.loop = true;
            行動音效.Play();
            跑步一次 = true;
        }
        if (跑步判斷 && !跑步一次 && !衝刺判斷 && !跳台上)  //地上走
        {
            行動音效.clip = 行動音效編號[0];
            行動音效.loop = true;
            行動音效.Play();
            跑步一次 = true;
        }
        if (衝刺判斷 && !衝刺一次 && 跳台上) //台上跑
        {
            行動音效.clip = 行動音效編號[5];
            行動音效.loop = true;
            行動音效.Play();
            衝刺一次 = true;
        }
        if (衝刺判斷 && !衝刺一次 && !跳台上)//地上跑
        {
            行動音效.clip = 行動音效編號[1];
            行動音效.loop = true;
            行動音效.Play();
            衝刺一次 = true;
        }
        if (跳躍判斷 && !跳躍一次)
        {
            行動音效.clip = 行動音效編號[2];
            行動音效.loop = false;
            行動音效.Play();
            跳躍一次 = true;
        }
        if (爬牆判斷 && !爬牆一次)
        {
            行動音效.clip = 行動音效編號[3];
            行動音效.loop = true;
            行動音效.Play();
            爬牆一次 = true;
        }
    }
    //.
    private bool 墜落一次;
    #endregion
}

