using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class 對話系統 : MonoBehaviour
{
    [Range(0, 1)]
    public float 速度 = 0.1f;
    public GameObject 圖示;
    public Text 名稱;
    public Text 內容;
    public CanvasGroup CanvasGroup畫布;
    //public KeyCode 按鍵 = KeyCode.J;
    [Range(0, 1)]
    public float 音效音量 = 1;
    public AudioClip aud;
    public AudioClip 對話;
    [Header("不動")]
    public AudioSource 音效;
    public int 對話輪 = 0;
    public 存放對話資料 存放date;

    player控制 e;
    private void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
        CanvasGroup畫布 = transform.GetChild(0).GetComponent<CanvasGroup>();
        音效 = GetComponent<AudioSource>();
        對話輪 = 0;
    }
    public void 播放旁白()
    {
        StartCoroutine(修改內容());
    }
    private IEnumerator 修改內容()
    {
        CanvasGroup畫布.alpha = 1;     //透明度0>1
        內容.text = "";     //清空
        if (存放date.a[對話輪])
        {
            名稱.text = 存放date.對話者名稱1;
        }
        else if (存放date.b[對話輪])
        {
            名稱.text = 存放date.對話者名稱2;
        }
        else
        {
            名稱.text = "神秘人";
        }
        /////////////////////
        for (int i = 0; i < 存放date.對話內容.Length; i++)   //每個段落i
        {
            for (int j = 0; j < 存放date.對話內容[i].Length; j++)   //每個段落中的文字j
            {
                //print(存放date.對話內容[i][j]);
                內容.text += 存放date.對話內容[i][j];   //i段落中的每個文字j
                音效.PlayOneShot(aud, 音效音量);
                yield return new WaitForSeconds(速度);  //出字速度
            }

            圖示.SetActive(true);

            //等待按下按鍵，null每一針時間
            while (!Input.GetKeyDown(KeyCode.J))    //while達成一直執行  等待玩家完成按鈕，使用null為每針的時間
            {
                yield return null;
            }
            /////////////////////
            if (存放date.a.Length-1 > 對話輪|| 存放date.b.Length-1 > 對話輪)
            {
                對話輪++;
            }
            if (存放date.a[對話輪])
            {
                名稱.text = 存放date.對話者名稱1;
            }
            else if (存放date.b[對話輪])
            {
                名稱.text = 存放date.對話者名稱2;
            }
            else
            {
                名稱.text = "神秘人";
            }

            內容.text = "";   //清空
            圖示.SetActive(false);

            if (i == 存放date.對話內容.Length - 1)  //結束關閉介面
            {
                CanvasGroup畫布.alpha = 0;
                e.可使用E = true;
                對話輪 = 0;
            }
        }
    }
}
