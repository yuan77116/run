using UnityEngine;

public class 傳送翻滾bo : MonoBehaviour
{
    player控制 e;
    public float 翻滾時間=4;
    float a = 0;
    private Animator ani;
    void Start()
    {
        e = GetComponent<player控制>();
        ani = GetComponent<Animator>();
        ani.SetBool("傳送翻滾bo", true);
        e.可使用E = false;
    }
    void Update()
    {
        if (a < 翻滾時間)
        {
            a += 1 * Time.deltaTime;
        }
        else
        {
            ani.SetBool("傳送翻滾bo", false);
            e.可使用E = true;
            enabled = false;
        }
    }

}
