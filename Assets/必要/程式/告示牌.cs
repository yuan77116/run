using UnityEngine;
using UnityEngine.UI;

public class 告示牌 : MonoBehaviour
{
    player控制 e;
    public GameObject 告示視窗;
    public Text 告示標題;
    public Text 告示內容;
    public Transform 滾動區域;
    //
    public int 標題size=50, 內容size=35;
    //
    public bool 要按e = true;
    private bool 看過了;
    private bool 達到可觀看距離;
    //
    public string 標題內容;
    public string 告示牌內容;
    //
    [Header("標題顏色")]
    public Vector3 rgb1 = new Vector3(0, 0, 0);
    public int a1 = 255;
    [Header("告示顏色")]
    public Vector3 rgb2 = new Vector3(0, 0, 0);
    public int a2 = 255;
    private AudioSource 告示牌音效;
    //---------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            達到可觀看距離 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            達到可觀看距離 = false;
        }
    }
    //---------------------------------------------------------------------
    void Start()
    {
        告示牌音效 = GetComponent<AudioSource>();
        e = GameObject.Find("主角").GetComponent<player控制>();
        //告示視窗 = GameObject.Find("告示牌視窗");

        //告示標題.color = new Color(rgb1.x, rgb1.y, rgb1.z, a1);
        //告示內容.color = new Color(rgb2.x, rgb2.y, rgb2.z, a2);
        //告示標題.text = 標題內容;
        //告示內容.text = 告示牌內容;
        //告示標題.fontSize = 標題size;
        //告示內容.fontSize = 內容size;
        看過了 = false;
    }
    void Update()
    {
        開啟條件();
        關閉();
    }
    private void 關閉()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(告示視窗!=null)
            告示視窗.SetActive(false);
        }
    }
    //---------------------------------------------------------------------
    private void 開啟條件()
    {
        if (要按e)
        {
            if (達到可觀看距離 && Input.GetKeyDown(KeyCode.E) && e.可使用E)
            {
                告示牌音效.Play();
                告示標題.color = new Color(rgb1.x, rgb1.y, rgb1.z, a1);
                告示內容.color = new Color(rgb2.x, rgb2.y, rgb2.z, a2);
                告示標題.text = 標題內容;
                告示內容.text = 告示牌內容;
                告示標題.fontSize = 標題size;
                告示內容.fontSize = 內容size;
                告示視窗.SetActive(true);
                e.可使用E = false;
                看過了 = true;
            }
        }
        else
        {
            if (達到可觀看距離 && e.可使用E && !看過了)
            {
                告示牌音效.Play();
                告示標題.color = new Color(rgb1.x, rgb1.y, rgb1.z, a1);
                告示內容.color = new Color(rgb2.x, rgb2.y, rgb2.z, a2);
                告示標題.text = 標題內容;
                告示內容.text = 告示牌內容;
                告示標題.fontSize = 標題size;
                告示內容.fontSize = 內容size;
                告示視窗.SetActive(true);
                e.可使用E = false;
                看過了 = true;

            }
        }
        if (看過了)
        {
            要按e = true;
        }
    }
    public void 回復滾動痕跡()
    {
        滾動區域.transform.position =new Vector2(滾動區域.transform.position.x,-90);
    }
}
