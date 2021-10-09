using UnityEngine;

public class 望遠鏡 : MonoBehaviour
{
    public Camera 攝影機;
    public float 望遠鏡距離;
    private bool 範圍內;
    private bool 一次;
    player控制 e;
    private void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內 = false;
            一次 = false;
            攝影機.orthographicSize = 7;
        }
    }
    private void Update()
    {
        if (範圍內 && !一次 && Input.GetKeyDown(KeyCode.E) && e.可使用E)
        {
            攝影機.orthographicSize = 望遠鏡距離;
            一次 = true;
        }
    }
}
