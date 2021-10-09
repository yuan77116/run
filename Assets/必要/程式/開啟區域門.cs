using UnityEngine;

public class 開啟區域門 : MonoBehaviour
{
    public GameObject 區域門;

    public bool 開關bo = false;

    public float 此門開關高度 = 3.2f;
    public float 上升速度 = 1, 下降速度 = 1.2f;

    private Vector2 初始位置;
    private float 此門上限;
    private AudioSource 開門音效;
    //
    private void Start()
    {
        開門音效 = GetComponent<AudioSource>();
        初始設定位置();
    }
    private void Update()
    {
        開關控制();
    }
    //
    private void 初始設定位置()
    {
        初始位置 = transform.position;
        此門上限 = 初始位置.y + 此門開關高度;
        if (開關bo)
        {
            transform.position = new Vector2(0, 初始位置.y + 此門開關高度);
        }
        else
        {
            transform.position = new Vector2(0, 初始位置.y);
        }
    }
    private void 開關控制()
    {
        if (開關bo)
        {
            if (transform.position.y < 此門上限)
            {
                transform.Translate(0, 上升速度 * Time.deltaTime, 0);
            }
        }
        else
        {
            if (transform.position.y > 初始位置.y)
            {
                transform.Translate(0, -下降速度 * Time.deltaTime, 0);
            }
        }
    }
    //
    public void 呼叫開門()
    {
        開關bo = true;
        開門音效.Play();
    }
    public void 呼叫關門()
    {
        開關bo = false;
        開門音效.Play();
    }
}
