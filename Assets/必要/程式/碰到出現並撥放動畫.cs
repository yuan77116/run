using UnityEngine;

public class 碰到出現並撥放動畫 : MonoBehaviour
{
    public GameObject 物;
    private Animator ani;
    private void Start()
    {
        ani = 物.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            物.SetActive(true);
            ani.enabled = true;
            Destroy(gameObject);
        }
    }
}
