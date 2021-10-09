using UnityEngine;

public class 觸碰E通關 : MonoBehaviour
{
    轉場控制 轉場控制;
    player控制 e;
    private bool 範圍內 = false;
    private void Start()
    {
        轉場控制 = GameObject.Find("場景管理器").GetComponent<轉場控制>();
        e = GameObject.Find("主角").GetComponent<player控制>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內 = false;
        }
    }
    private void Update()
    {
        if (範圍內 && e.可使用E && Input.GetKeyDown(KeyCode.E))
        {
            轉場控制.直接下一關();
        }
    }
}