using UnityEngine;

public class 碰到setA某物 : MonoBehaviour
{
    public bool 持續碰=true;
    public GameObject 某物;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            某物.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (持續碰)
            {
                某物.SetActive(false);
            }
        }
    }
}
