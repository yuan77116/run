using UnityEngine;

public class 跟隨方向火球 : MonoBehaviour
{
    [Header("隨機生成角度")]
    public int v1=0, v2=360;
    player控制 e;
    Rigidbody2D rig;
    public int 傷害量 = 10;
    public float 速度 = 100;
    void Start()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(v1, v2));
        e = GameObject.Find("主角").GetComponent<player控制>();
        rig = GetComponent<Rigidbody2D>();
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void Update()
    {
        rig.velocity = transform.up * -Time.deltaTime * 速度;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            e.受傷(傷害量);
            Destroy(gameObject);
        }
        else if (collision.transform.tag == "地板" || collision.transform.tag == "牆壁" || collision.transform.tag == "不可跳躍的地板" || collision.transform.tag == "即死陷阱")
        {
            Destroy(gameObject);
        }
    }
}
