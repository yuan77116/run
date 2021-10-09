using UnityEngine;

public class 碰到onTr改Layer層 : MonoBehaviour
{
    public Renderer lay;
    public int 改layer層 = 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            lay.sortingOrder = 改layer層;
        }
    }
}
