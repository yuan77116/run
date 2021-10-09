using UnityEngine;

public class g撿起生成 : MonoBehaviour
{
    player控制 e;
    public GameObject 生成的物件;
    public Transform 生成位置;
    public bool 撿起bo = false;
    private void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && Input.GetKeyDown(KeyCode.G) && e.可使用E && !撿起bo)
        {
            Instantiate(生成的物件, 生成位置);
            撿起bo = true;
            Destroy(gameObject);
        }
    }
}
