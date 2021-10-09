using UnityEngine;

public class 有重力火球 : MonoBehaviour
{
    player控制 e;
    Rigidbody2D rig;
    public int 傷害量 = 10;
    public float 速度 = 100;
    private int 方向;
    [Header("固定的")]
    public bool 上, 下, 左, 右;

    [Header("指定的")]
    public bool 指定的 = false;
    public Vector2 最終位置;
    private void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (指定的)
        {
            指定前進();
        }
        else
        {
            固定前進();
        }
    }
    private void 指定前進()
    {
        transform.position = Vector2.MoveTowards(transform.position, 最終位置, 速度 * Time.deltaTime);
    }
    private void 固定前進()
    {
        if (上)
        {
            方向 = 180;
            rig.velocity = transform.up * Time.deltaTime * 速度;
        }
        else if (下)
        {
            方向 = 0;
            rig.velocity = transform.up * -Time.deltaTime * 速度;
        }
        else if (左)
        {
            方向 = -90;
            rig.velocity = transform.right * -Time.deltaTime * 速度;
        }
        else if (右)
        {
            方向 = 90;
            rig.velocity = transform.right * Time.deltaTime * 速度;
        }
        else
        {
            方向 = 0;
            rig.velocity = transform.up *- Time.deltaTime * 速度;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 方向);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            e.受傷(傷害量);
            Destroy(gameObject);
        }
        else if(collision.transform.tag=="地板"|| collision.transform.tag == "牆壁" || collision.transform.tag == "不可跳躍的地板" || collision.transform.tag == "即死陷阱")
        {
            Destroy(gameObject);
        }
    }
}
