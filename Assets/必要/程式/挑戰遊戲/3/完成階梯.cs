using UnityEngine;

public class 完成階梯 : MonoBehaviour
{
    階梯遊戲 階梯遊戲;
    public GameObject 完成的階梯程式物;
    void Start()
    {
        階梯遊戲 = 完成的階梯程式物.GetComponent<階梯遊戲>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            階梯遊戲.完成 = true;
        }
    }
}
