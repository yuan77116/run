using UnityEngine;

public class 重生點 : MonoBehaviour
{
    public int 此重生點位;
    重生點管理 重生點管理;
    紀錄 紀錄0;
    private bool a = false;
    Renderer color;
    void Start()
    {
        重生點管理 = GameObject.Find("重生點管理").GetComponent<重生點管理>();
        color = GetComponent<Renderer>();
        紀錄0= GameObject.Find("重生點管理").GetComponent<紀錄>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !a)
        {
            color.material.color= new Color(0, 255, 0, 255);
            重生點管理.音效.Play();
            a = true;
            紀錄();
        }
    }
    private void 紀錄()
    {
        紀錄0.data.重生點紀錄 = 此重生點位;
        重生點管理.復活點int = 此重生點位;
        PlayerPrefs.SetInt("重生點紀錄", 紀錄0.data.重生點紀錄);
        PlayerPrefs.SetInt("關卡", 紀錄0.data.關卡);
    }

}
