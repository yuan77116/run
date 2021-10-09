using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class _呼叫故事程式 : MonoBehaviour
{
    public AudioSource 暫停場景背景音樂;
    public GameObject 故事視窗;
    public Slider 故事視窗slider;
    public Text 故事標題text;
    public Text 故事內容text;
    public Text 故事歌詞text;
    public Text 播放時間text;

    public bool 要按e=true;
    public int 歌曲編號 = 0;
    public float 故事針數微調 = 2;
    public int[] 故事針數int;
    public string[] 故事內容;
    public float 歌詞針數微調 = 2;
    public int[] 歌詞針數int;
    public string[] 歌詞內容;
    public string 此歌曲總長st = "00：00 ";

    player控制 e;
    public string 歌題;
    private bool 看過了;
    private bool 可觀看故事;
    private bool 呼叫一次別動 = false;
    private bool 一次;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            可觀看故事 = true;
            enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            可觀看故事 = false;
            enabled = false;
        }
    }
    private void Start()
    {
        if (故事視窗 != null)
        {
            故事視窗.SetActive(false);
        }
        e = GameObject.Find("主角").GetComponent<player控制>();
        歌曲 = GetComponent<AudioSource>();
        故事int = 0;
        歌詞int = 0;
    }
    private void Update()
    {
        if (故事視窗 == null)
        {
            歌曲.Stop();
        }
        開啟();
        暫停控制();
    }
    private void FixedUpdate()
    {
        計時();
        上歌詞();
        上故事();
    }
    //-----------
    private int 故事int = 0;
    private int 歌詞int = 0;
    private float 播放等待fl = 2;
    private float 計時器 = 0;
    private AudioSource 歌曲;
    public AudioClip[] 歌單;
    private int 故事針數i = 0;
    private int 歌詞針數i = 0;
    private bool 執行;
    private float 時間;
    private bool space暫停;
    private void 開啟()
    {
        if (要按e)
        {
            if (可觀看故事 && Input.GetKeyDown(KeyCode.E) && e.可使用E && !呼叫一次別動)
            {
                e.可使用E = false;
                看過了 = true;
                呼叫一次別動 = true;
                啟動();
            }
        }
        else
        {
            if (可觀看故事 && e.可使用E && !看過了 && !呼叫一次別動)
            {
                e.可使用E = false;
                看過了 = true;
                呼叫一次別動 = true;
                啟動();
            }
        }
        if (可觀看故事 && 看過了 && e.可使用E && Input.GetKeyDown(KeyCode.E))
        {
            e.可使用E = false;
            呼叫一次別動 = true;
            啟動();
        }
    }
    private void 暫停控制()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !space暫停)
        {
            故事暫停();
            space暫停 = true;
            故事內容text.text = "空白建暫停中";
            故事歌詞text.text = "";
        }
        else if (Input.GetKeyDown(KeyCode.Space) && space暫停)
        {
            故事繼續();
            space暫停 = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            關閉故事();
        }
    }
    public void 啟動()
    {
        故事視窗.SetActive(true);
        暫停場景背景音樂.Pause();
        space暫停 = false;
        歌曲.clip = 歌單[歌曲編號];
        Invoke("故事繼續", 播放等待fl);
        故事標題text.text = 歌題;
        故事視窗slider.value = 歌曲.volume;
    }
    IEnumerator 故事結束等待()
    {
        yield return new WaitForSeconds(2);
        故事內容text.text = "故事回憶結束";
        故事歌詞text.text = "";
        yield return new WaitForSeconds(3);
        關閉故事();
    }
    private void 故事暫停()
    {
        執行 = false;
        歌曲.Pause();
    }
    private void 故事繼續()
    {
        執行 = true;
        故事內容text.text = 故事內容[故事int];
        故事歌詞text.text = 歌詞內容[歌詞int];
        歌曲.Play();
    }
    private void 關閉故事()
    {
        執行 = false;
        故事視窗.SetActive(false);
        時間 = 0;
        計時器 = 0;
        暫停場景背景音樂.Play();
        e.可使用E = true;
        歌詞針數i = 0;
        故事針數i = 0;
        故事int = 0;
        歌詞int = 0;
        space暫停 = false;
        故事內容text.text = " ";
        故事歌詞text.text = " ";
        呼叫一次別動 = false;
        歌曲.Stop();
        一次 = false;
    }
    private void 計時()
    {
        if (執行)
        {
            計時器 += 1 * Time.fixedDeltaTime;
            時間 = 計時器;
        }
        else
        {
            if (!一次)
            {
                計時器 = 時間;
                一次 = true;
            }
        }
        int 分 = (int)計時器 / 60;
        float 秒 = (int)計時器 % 60;
        播放時間text.text = (分 + "：" + 秒 + " | " + 此歌曲總長st);
    }
    public void 上歌詞()
    {
        if (Mathf.Ceil(時間) == (歌詞針數int[歌詞針數i] + 歌詞針數微調))
        {
            故事歌詞text.text = 歌詞內容[歌詞int];
            if (歌詞針數i < 歌詞針數int.Length - 1)
            {
                歌詞針數i++;
                歌詞int++;
            }
            else if (歌詞針數i == 歌詞針數int.Length - 1)
            {
                StartCoroutine(故事結束等待());
            }
        }
    }
    public void 上故事()
    {
        if (Mathf.Ceil(時間) == (故事針數int[故事針數i] + 故事針數微調))
        {
            故事內容text.text = 故事內容[故事int];
            if (故事針數i < 故事針數int.Length - 1)
            {
                故事針數i++;
                故事int++;
            }
            else if (歌詞針數i == 歌詞針數int.Length - 1)
            {
                故事內容text.text = "(=.=> <=.=)";
            }
        }
    }
}
