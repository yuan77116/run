using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esc : MonoBehaviour
{
    player控制 e;
    紀錄 紀錄;
    private AudioSource aud;
    //
    public GameObject 角色血量ui介面;
    public GameObject esc介面;
    public GameObject npc對話視窗;
    public GameObject 故事視窗;
    public GameObject 告示牌視窗;
    //
    private bool 開啟UI;
    //
    private void Start()
    {
        aud = GameObject.Find("背景音樂").GetComponent<AudioSource>();
        紀錄 = GameObject.Find("重生點管理").GetComponent<紀錄>();
        Time.timeScale = 1;
        e = GameObject.Find("主角").GetComponent<player控制>();
        //esc介面 = GameObject.Find("遊戲中設定");
        //告示牌視窗 = GameObject.Find("告示牌視窗");
        //故事視窗 = GameObject.Find("故事視窗");
        //npc對話視窗 = GameObject.Find("對話視窗");
        //角色血量ui介面 = GameObject.Find("血量Ui");
        //
        if (esc介面 != null)
            esc介面.SetActive(false);
        if (告示牌視窗 != null)
            告示牌視窗.SetActive(false);
        if (故事視窗 != null)
            故事視窗.SetActive(false);
        if (npc對話視窗 != null)
            npc對話視窗.SetActive(false);
        if (角色血量ui介面 != null)
            角色血量ui介面.SetActive(true);
        //
        開啟UI = false;
    }
    private void Update()
    {
        按下esc();
    }
    //
    private void 按下esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !開啟UI)
        {
            esc介面.SetActive(true);
            角色血量ui介面.SetActive(false);
            開啟UI = true;
            e.可使用E = false;
            Time.timeScale=0;
            PlayerPrefs.SetFloat("音樂時間", 紀錄.data.音樂時間);
            aud.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && 開啟UI)
        {
            esc介面.SetActive(false);
            告示牌視窗.SetActive(false);
            故事視窗.SetActive(false);
            if(npc對話視窗!=null)
            npc對話視窗.SetActive(false);
            角色血量ui介面.SetActive(true);
            開啟UI = false;
            e.可使用E = true;
            Time.timeScale = 1;
            aud.Play();
        }
    }
}
