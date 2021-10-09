using UnityEngine;

public class 觸碰傳送 : MonoBehaviour
{
    public bool 要按E = true;
    public Transform[] 傳送管理;
    public int 傳送點號碼 = 0;
    private bool 距離bo;
    private GameObject 玩家;
    private AudioSource 傳送音效;
    private void Start()
    {
        玩家 = GameObject.Find("主角");
        傳送音效 = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            距離bo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            距離bo = false;
        }
    }
    private void Update()
    {
        if (要按E)
        {
            if (距離bo && Input.GetKeyDown(KeyCode.E))
            {
                玩家.transform.position = 傳送管理[傳送點號碼].position;
                if (傳送音效 != null)
                {
                    傳送音效.Play();
                }
            }
        }
        else
        {
            if (距離bo)
            {
                玩家.transform.position = 傳送管理[傳送點號碼].position;
                if (傳送音效 != null)
                {
                    傳送音效.Play();
                }
            }
        }
    }
}
