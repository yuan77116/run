using UnityEngine;
using UnityEngine.UI;
//using System.Collections;

public class 對話by_npc : MonoBehaviour
{
    private AudioSource 村民聲;
    player控制 e;
    [Header("主角位置")]
    private GameObject player_pos;
    [Header("面向左")]
    public Sprite npc圖;
    [Header("對話物_本身面向依次左.中.右")]
    public Sprite[] sprite_npc方向;
    [Header("對話視窗_所有")]
    public GameObject windows;
    [Header("對話內容")]
    public Text 內容text;
    private int 第幾句對話完成int;
    //[Header("判定可對話範圍")]
    //private bool 對話範圍內 = false;
    [Header("對話框提示")]
    public GameObject say_3D;
    public string[] 說些;
    private int nameber = 0;
    private bool 碰=false;

    private bool say3D_setbo;
    private bool 對話視窗開啟bool;
    private void Start()
    {
        村民聲 = GetComponent<AudioSource>();
        第幾句對話完成int = 說些.Length+1;
        player_pos = GameObject.Find("主角");
        e = GameObject.Find("主角").GetComponent<player控制>();
        windows.SetActive(false);
        say_3D.SetActive(false);
        say3D_setbo = false;
        對話視窗開啟bool = false;
        碰 = false;
        //content.material.color = new Color(75, 0, 160);
        //content = gameObject.GetComponent<Text>();
        //content.color = new Color(75, 0, 160);
    }
    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = npc圖;
        npc面向判定();
        說話();
        if (say3D_setbo)
        {
            say_3D.SetActive(false);
        }
    }
    //**************************************
    private void 說話()
    {
        if (對話視窗開啟bool && Input.GetKeyDown(KeyCode.K) && !e.可使用E)
        {
            關閉對話視窗();
        }
        else if (對話視窗開啟bool && Input.GetKeyDown(KeyCode.J) && !e.可使用E)
        {
            next();
        }
    }
    private void npc面向判定()
    {
        if (碰)
        {
            if ((!say3D_setbo) || (say3D_setbo && !e.可使用E))
            {
                if (player_pos.transform.position.x > transform.position.x)
                {
                    npc圖 = sprite_npc方向[2];
                }
                else
                {
                    npc圖 = sprite_npc方向[0];
                }
            }
            else
            {
                npc圖 = sprite_npc方向[1];
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            碰 = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (!say3D_setbo)
        {
            say_3D.SetActive(true);
        }
        else
        {
            村民聲.loop = false;
            if (!say3D_setbo)
            {
                村民聲.Play();
            }
            say_3D.SetActive(false);
        }
        if (collision.transform.tag == "Player" && Input.GetKeyDown(KeyCode.E) && e.可使用E)
        {
            開起對話視窗();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        say_3D.SetActive(false);
        if (collision.transform.tag == "Player")
        {
            關閉對話視窗();
        }
    }
    //****************************************
    private void 開起對話視窗()
    {
        村民聲.Play();
        windows.SetActive(true);
        對話視窗開啟bool = true;
        e.可使用E = false;
        內容text.text = "";
    }
    private void 關閉對話視窗()
    {
        windows.SetActive(false);
        對話視窗開啟bool = false;
        nameber = 0;
        e.可使用E = true;
    }
    public void next()
    {
        nameber++;
        if((nameber < 第幾句對話完成int))
        {
            內容text.text = 說些[nameber - 1];
        }
        else if (nameber== 第幾句對話完成int)
        {
            內容text.text = "對話完成";
            say3D_setbo = true;
        }
        else if(nameber > 第幾句對話完成int)
        {
            關閉對話視窗();
        }
    }
}
