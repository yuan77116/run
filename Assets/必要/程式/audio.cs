using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audio : MonoBehaviour
{
    public AudioMixer 背景AudioMixer;
    public AudioMixer 效果AudioMixer;
    public Slider 背景音樂slider;
    public Slider 音效音樂slider;
    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.GetFloat("背景音量紀錄", data.背景音量紀錄)!=0)
        {
            背景AudioMixer.SetFloat("背景音量", PlayerPrefs.GetFloat("背景音量紀錄", data.背景音量紀錄));
            背景音樂slider.value = PlayerPrefs.GetFloat("背景音量紀錄", data.背景音量紀錄);
        }
        if (PlayerPrefs.GetFloat("背效音量紀錄", data.背效音量紀錄) != 0)
        {
            效果AudioMixer.SetFloat("背景音量", PlayerPrefs.GetFloat("背效音量紀錄", data.背效音量紀錄));
            音效音樂slider.value = PlayerPrefs.GetFloat("背效音量紀錄", data.背效音量紀錄);
        }
    }
    public void 遊戲音量(float 數值)
    {
        背景AudioMixer.SetFloat("背景音量", 數值);
        data.背景音量紀錄 = 數值;
        PlayerPrefs.SetFloat("背景音量紀錄", data.背景音量紀錄);
    }
    public void 效果音量(float 數值)
    {
        效果AudioMixer.SetFloat("效果音量", 數值);
        data.背效音量紀錄 = 數值;
        PlayerPrefs.SetFloat("背效音量紀錄", data.背效音量紀錄);
    }
    [SerializeField]
    public playerdata data;
    [System.Serializable]
    public class playerdata
    {
        public float 背景音量紀錄 = 0;
        public float 背效音量紀錄 = 0;
    }
}
