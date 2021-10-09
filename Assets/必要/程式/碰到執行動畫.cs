using UnityEngine;

public class 碰到執行動畫 : MonoBehaviour
{
    private Animator ani;
    private void Start()
    {
        ani = GetComponent<Animator>();
        ani.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            ani.enabled = true;
        }
    }
}
