using UnityEngine;

public class 呼叫怪物滿血 : MonoBehaviour
{
    怪物控制 怪物控制;
    private AudioSource aud;
    public GameObject 要復活的怪物;
    private bool 範圍內;
    public bool 復活所有清單怪物=false;
    public GameObject[] 要復活的所有怪物;
    player控制 e;
    private void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
        怪物控制 = 要復活的怪物.gameObject.GetComponent<怪物控制>();
        aud = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            範圍內 = false;
        }
    }
    void Update()
    {
        if (!復活所有清單怪物)
        {
            if (範圍內 && Input.GetKeyDown(KeyCode.E) && e.可使用E)
            {
                aud.Play();
                怪物控制.復活怪物();
            }
        }
        else
        {
            if (範圍內 && Input.GetKeyDown(KeyCode.E) && e.可使用E)
            {
                aud.Play();
                for (int i = 0; i < 要復活的所有怪物.Length; i++)
                {
                    GameObject a = 要復活的所有怪物[i - 1];
                    怪物控制 = a.gameObject.GetComponent<怪物控制>();
                    怪物控制.復活怪物();
                }
            }
        }
    }
}
