using UnityEngine;

public class 觸發開啟區域門程式 : MonoBehaviour
{
    開啟區域門 區域門控制;

    public bool 需要按下E;
    public bool 需要開關bo=true;
    public bool 一次=true;
    private bool 一次bo=false;

    private bool 範圍內bo;
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            範圍內bo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            範圍內bo = false;
        }
    }
    //
    private void Start()
    {
        區域門控制 = GameObject.Find("地圖區域門").GetComponent<開啟區域門>();
    }
    private void Update()
    {
        //print("範圍內bo"+範圍內bo+ "區域門控制.開關bo"+ 區域門控制.開關bo);
        if (一次)
        {
            if(!一次bo)
            判斷達成開門條件();
        }
        else
        {
            判斷達成開門條件();
        }
    }
    private void 判斷達成開門條件()
    {
        if (需要開關bo)
        {
            if (!需要按下E && 範圍內bo)
            {
                區域門控制.呼叫開門();
                一次bo = true;
            }
            else if (需要按下E && 範圍內bo && Input.GetKeyDown(KeyCode.E))
            {
                區域門控制.呼叫開門();
                一次bo = true;
            }
        }
        else
        {
            if (!需要按下E && 範圍內bo)
            {
                區域門控制.呼叫關門();
                一次bo = true;
            }
            else if (需要按下E && 範圍內bo && Input.GetKeyDown(KeyCode.E))
            {
                區域門控制.呼叫關門();
                一次bo = true;
            }
        }

    }
}
