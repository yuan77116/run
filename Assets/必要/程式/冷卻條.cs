using UnityEngine;
using UnityEngine.UI;

public class 冷卻條 : MonoBehaviour
{
    player控制 e;
    public Image 圖片冷卻1;
    public Image 圖片冷卻2;
    public Image 圖片冷卻3;
    // Start is called before the first frame update
    void Start()
    {
        e = GameObject.Find("主角").GetComponent<player控制>();
    }

    // Update is called once per frame
    void Update()
    {
        圖片冷卻1.fillAmount = (e.isatk1time / e.isatk1cd);
        圖片冷卻2.fillAmount = (e.isatk2time / e.isatk2cd);
        圖片冷卻3.fillAmount = (e.isatk3time / e.isatk3cd);
    }
}
