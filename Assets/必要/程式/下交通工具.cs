using UnityEngine;

public class 下交通工具 : MonoBehaviour
{
    player控制 e;
    交通工具 交通工具;
    public string 交通工具名="馬";
    private void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
        交通工具=GameObject.Find(交通工具名).GetComponent<交通工具>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && 交通工具!=null)
        {
            交通工具.到達();
        }
    }
}
