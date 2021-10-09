using UnityEngine;
using System.Collections;

public class 交通工具 : MonoBehaviour
{
    public Camera cameraa;
    public float 攝影機距離=10;
    player控制 e;
    public Transform 騎乘位;
    public float Movespeed=800;
    public float gravity = 0.5f;
    private Rigidbody2D rig;
    private float LeftRight;
    private bool 下馬=false;
    public GameObject 無敵死亡線=null;
    private AudioSource loop音樂;
    private bool 音樂一次=false;
    private void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
        rig = GetComponent<Rigidbody2D>();
        loop音樂 = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!下馬)
        {
            e.騎乘中 = true;
            e.transform.parent = transform;
        }
    }
    private void Update()
    {
        if (e.騎乘中)
        {
            cameraa.orthographicSize = 攝影機距離;
            e.transform.position = 騎乘位.position;
            行動();
            移動翻轉();
            e.可使用E = false;
        }
    }
    private void 行動()
    {
        rig.velocity = new Vector2(LeftRight * Movespeed * Time.fixedDeltaTime, rig.velocity.y - gravity);
        if (!音樂一次)
        {
            loop音樂.loop = true;
            loop音樂.Play();
            音樂一次 = true;
        }
        if (LeftRight == 0)
        {
            loop音樂.Stop();
            音樂一次 = false;
        }
        //loop音樂.PlayDelayed(0.2f);
    }
    public void 到達()
    {
        音樂一次 = false;
        cameraa.orthographicSize = 7;
        loop音樂.Stop();
        e.騎乘中 = false;
        e.transform.parent = null;
        e.可使用E = true;
        下馬 = true ;
        Invoke("刪除", 1.5f);
        if (無敵死亡線 != null)
        {
            無敵死亡線.SetActive(false);
        }
    }
    private void 刪除()
    {
        Destroy(gameObject);
    }
    private void 移動翻轉()
    {
        LeftRight = Input.GetAxis("Horizontal");
        if (LeftRight > 0)//看右
        {
            //角色圖.flipX = false;
            transform.eulerAngles = Vector2.up * 180;
        }
        if (LeftRight < 0)  //看左
        {
            //角色圖.flipX = true;
            transform.eulerAngles = Vector2.up * 0;
        }
    }
}
