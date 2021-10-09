using UnityEngine;
using System.Linq;

public class 電梯 : MonoBehaviour
{
    [Header("單一")]
    public bool 自動 = false;
    public Vector2 要移動方向=new Vector2(0,0);
    public float 移動speed=3;
    [Header("連續")]
    public bool 連續 = false;
    public Vector2[] 連續的移動;
    public int 目前的點編號 = 0;

    private Vector2 原本位置;
    private bool 範圍內;
    private Vector2 最終位置;
    private bool 自動開關 = false;
    private Vector2 目前位置;
    private GameObject 主角;
    private AudioSource 電梯音效;
    private bool 音效一次;
    private bool 等待bo;
    //
    private void 回復()
    {
        if (等待bo)
        {
            範圍內 = false;
            主角.transform.parent = null;
            等待bo = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內 = true;
            等待bo = false;
            主角.transform.parent = transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內 = true;
            等待bo = false;
            主角.transform.parent = transform;
        }
        else if (collision.transform.tag == "怪物")
        {
            collision.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            等待bo = true;
            Invoke("回復", 3);
            音效一次 = false;
        }
        else if (collision.transform.tag == "怪物")
        {
            collision.transform.parent = null;
        }
    }
    //
    // Start is called before the first frame update
    void Start()
    {
        電梯音效 = GetComponent<AudioSource>();
        主角 = GameObject.Find("主角");
        原本位置 = transform.position;
        最終位置 = 原本位置 + 要移動方向;
        目前的點編號 = 0;
        範圍內 = false;
    }

    // Update is called once per frame
    void Update()
    {
        目前位置 = transform.position;
        if (!連續)
        {
            觸碰電梯();
        }
        else
        {
            連續電梯();
        }
        音效();
    }
    private void 觸碰電梯()
    {
        if (!自動)
        {
            if (範圍內)
            {
                if (目前位置 != 最終位置)
                {
                    transform.position = Vector2.MoveTowards(transform.position, 最終位置, 移動speed * Time.deltaTime);
                }
            }
            else
            {
                if (目前位置 != 原本位置)
                {
                    transform.position = Vector2.MoveTowards(transform.position, 原本位置, 移動speed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (!自動開關)
            {
                if (目前位置 != 最終位置)
                {
                    transform.position = Vector2.MoveTowards(transform.position, 最終位置, 移動speed * Time.deltaTime);
                }
                else
                {
                    自動開關 = true;
                }
            }
            else
            {
                if (目前位置 != 原本位置)
                {
                    transform.position = Vector2.MoveTowards(transform.position, 原本位置, 移動speed * Time.deltaTime);
                }
                else
                {
                    自動開關 = false;
                }
            }
        }
    }
    private void 連續電梯()
    {
        if (連續)
        {
            最終位置 = 原本位置 + 連續的移動[目前的點編號];
            if (範圍內)
            {
                if (目前位置 != 最終位置)
                {
                    print("目前位置"+ 目前位置 + "最終位置" + 最終位置);
                    transform.position = Vector2.MoveTowards(transform.position, 最終位置, 移動speed * Time.deltaTime);
                }
                else
                {
                    if (目前的點編號 < 連續的移動.Length - 1)
                    {
                        目前的點編號 += 1;
                    }
                }
            }
            else
            {
                最終位置 = 原本位置;
                目前的點編號 = 0;
                if (目前位置 != 最終位置)
                {
                    transform.position = Vector2.MoveTowards(transform.position, 最終位置, 移動speed * Time.deltaTime);
                }
            }
        }
    }
    private void 音效()
    {
        if (範圍內)
        {
            if (!音效一次)
            {
                電梯音效.loop = true;
                電梯音效.Play();
                音效一次 = true;
            }
        }
        else
        {
            電梯音效.Stop();
        }
    }
}
