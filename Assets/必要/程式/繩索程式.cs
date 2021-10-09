using UnityEngine;

public class 繩索程式 : MonoBehaviour
{
    private bool 範圍內;
    private float 位置微調y = -1;
    private bool 一次家人;
    private bool 一次回復;
    private Vector2 原位置;
    private bool 開始;
    private Animator 滑索ani;
    player控制 e;
    private bool 回復位置一次;
    private AudioSource aud;
    private void Start()
    {
        原位置 = new Vector2(transform.position.x, transform.position.y);
        e = GameObject.Find("主角").GetComponent<player控制>();
        aud = GetComponent<AudioSource>();
        滑索ani = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        繩上();
        if(滑索ani.enabled && !範圍內&& !回復位置一次)
        {
            滑索ani.enabled = false;
            滑索ani.SetTrigger("重至");
            aud.Stop();
            gameObject.transform.position=原位置;
        }
    }
    private void 繩上()
    {
        if (範圍內 && Input.GetKeyDown(KeyCode.E) && e.可使用E)
        {
            開始 = true;
        }
        if (開始)
        {
            if (!一次家人)
            {
                if (!滑索ani.enabled)
                {
                    e.transform.position = transform.position + new Vector3(0, 位置微調y, 0);
                }
                e.transform.parent = transform;
                e.rig.constraints = RigidbodyConstraints2D.FreezeAll;
                滑索ani.enabled = true;
                一次家人 = true;
                一次回復 = false;
                aud.Play();
            }
            if (Input.GetKeyUp(KeyCode.E) && e.可使用E)
            {
                開始 = false;
                一次家人 = false;
                回復位置一次 = false;
            }
        }
        else
        {
            if (!一次回復)
            {
                e.transform.parent = null;
                e.rig.constraints = RigidbodyConstraints2D.FreezeRotation;
                一次回復 = true;
            }
        }
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
        }
    }
}
