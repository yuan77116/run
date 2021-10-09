using UnityEngine;

public class 紀錄 : MonoBehaviour
{
    player控制 e;
    重生點管理 重生點管理;
    private AudioSource aud;
    void Start()
    {
        aud = GameObject.Find("背景音樂"). GetComponent<AudioSource>();
        重生點管理 = GameObject.Find("重生點管理").GetComponent<重生點管理>();
        e = GameObject.Find("主角").GetComponent<player控制>();

        if (PlayerPrefs.GetInt("關卡", data.關卡) != 重生點管理.此關卡為第幾關)
        {
            data.關卡 = 重生點管理.此關卡為第幾關;
            PlayerPrefs.SetInt("關卡", data.關卡);
            data.重生點紀錄 = 0;
            PlayerPrefs.SetInt("重生點紀錄", data.重生點紀錄);
            data.音樂時間 = 0;
            PlayerPrefs.SetFloat("音樂時間", data.音樂時間);
            if(!aud.isPlaying)
            aud.Play();
        }
        else
        {
            if (PlayerPrefs.GetInt("重生點紀錄", data.重生點紀錄) != 0)
            {
                重生點管理.復活點int = PlayerPrefs.GetInt("重生點紀錄", data.重生點紀錄);
                e.transform.position = 重生點管理.所有點位[重生點管理.復活點int].position;
            }
            if (PlayerPrefs.GetFloat("音樂時間", data.音樂時間) != 0)
            {
                aud.time=PlayerPrefs.GetFloat("音樂時間", data.音樂時間);
                aud.Play();
            }
        }
    }
    private void FixedUpdate()
    {
        data.音樂時間 = aud.time;
    }
    [SerializeField]
    public playerdata data;
    [System.Serializable]
    public class playerdata
    {
        public int 重生點紀錄=0;
        public int 關卡=0;
        public float 音樂時間 = 0;
    }
}
