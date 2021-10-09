using UnityEngine;

public class npc動作 : MonoBehaviour
{
    public 存放對話資料 存放date;
    public GameObject 新對話視窗;
    [Header("面向左")]
    public Sprite npc圖;
    [Header("對話物_本身面向依次左.中.右")]
    public Sprite[] sprite_npc方向;
    public GameObject say_3D;
    對話系統 對話系統;
    player控制 e;
    [Header("不動")]
    private bool 完成;
    private bool 範圍內;
    // Start is called before the first frame update
    void Start()
    {
        say_3D.SetActive(false);
        e = GameObject.Find("主角").GetComponent<player控制>();
        對話系統 = 新對話視窗.GetComponent<對話系統>();
    }

    // Update is called once per frame
    void Update()
    {
        npc面向判定();
        if (完成)
        {
            say_3D.SetActive(false);
        }
        else if (!完成 && 範圍內)
        {
            say_3D.SetActive(true);
        }
        if (範圍內 && e.可使用E && Input.GetKeyDown(KeyCode.E))
        {
            對話系統.音效.PlayOneShot(對話系統.對話, 0.8f);
            對話系統.播放旁白();
            完成 = true;
            e.可使用E = false;
        }
    }
    private void npc面向判定()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = npc圖;
        if (範圍內)
        {
            if (!完成)
            {
                if (e.gameObject.transform.position.x > transform.position.x)
                {
                    npc圖 = sprite_npc方向[2];
                }
                else
                {
                    npc圖 = sprite_npc方向[0];
                }
            }
            else if (完成)
            {
                npc圖 = sprite_npc方向[1];
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內 = true;
            對話系統.存放date = 存放date;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            對話系統.CanvasGroup畫布.alpha = 0;
            e.可使用E = true;
            範圍內 = false;
            對話系統.對話輪 = 0;
        }
    }
}
