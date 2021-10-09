using UnityEngine;

public class 重生點管理 : MonoBehaviour
{
    public int 此關卡為第幾關;
    public Transform[] 所有點位;
    public int 復活點int=0;
    public AudioSource 音效;
    player控制 e;
    private void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
        音效 = GetComponent<AudioSource>();
    }
   
    public void 復活()
    {
        e.transform.position = 所有點位[復活點int].position;
        StartCoroutine(e.復活無敵time());
        e.無敵時間 = 3f;
    }
}
