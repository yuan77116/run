using UnityEngine;

public class 妳是誰消失 : MonoBehaviour
{
    private Animator ani;
    private bool 一;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player"&& !一)
        {
            ani = GetComponent<Animator>();
            ani.enabled=true;
            一 = true;
        }
    }
}
