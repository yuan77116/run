using UnityEngine;

public class 碰到消失某物 : MonoBehaviour
{
    public GameObject 某物;
    public bool 購買時持續碰 = false;
    public bool 刪除=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (刪除)
            {
                Destroy(某物);
            }
            else
            {
                某物.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (購買時持續碰)
            {
                某物.SetActive(true);
            }
        }
    }
}
