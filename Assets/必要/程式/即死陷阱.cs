using UnityEngine;

public class 即死陷阱 : MonoBehaviour
{
    public bool 即死bo = true;
    public int 傷害值;
    public float 傷害cd;
    [Header("陷阱傷害冷卻")]
    private float cdtime = 0;
    private bool 碰到陷阱 = false;
    player控制 e;
    void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (即死bo)
            {
                e.受傷(e.最大hp);
            }
            else
            {
                碰到陷阱 = true;
            }
        }
        if (collision.transform.tag == "怪物")
        {
            collision.gameObject.GetComponent<怪物控制>().受傷(9999);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            碰到陷阱 = false;
        }
    }
    private void Update()
    {
        if (碰到陷阱 && !即死bo)
        {
            if (cdtime < 傷害cd)
            {
                cdtime += 1 * Time.deltaTime;
            }
            else
            {
                cdtime = 0;
                e.受傷(傷害值);
            }
        }
    }
}
