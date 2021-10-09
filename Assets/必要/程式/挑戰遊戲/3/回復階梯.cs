using UnityEngine;

public class 回復階梯 : MonoBehaviour
{
    階梯遊戲 階梯遊戲;
    public GameObject 回復的階梯程式物;
    private void Start()
    {
        階梯遊戲 = 回復的階梯程式物.GetComponent<階梯遊戲>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            階梯遊戲.回復();
        }
    }
}
