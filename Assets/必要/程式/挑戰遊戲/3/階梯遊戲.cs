using UnityEngine;
using System.Collections;

public class 階梯遊戲 : MonoBehaviour
{
    public GameObject 階梯;
    public float 節奏1;
    public float 節奏2;
    private bool go;
    public bool 完成 = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!go)
            {
                StartCoroutine(開始());
                go = true;
            }
        }
    }
    IEnumerator 開始()
    {
        階梯.SetActive(true);
        yield return new WaitForSeconds(節奏1);
        階梯.SetActive(false);
        yield return new WaitForSeconds(節奏2);
        if (!完成)
        {
            StartCoroutine(開始());
        }
        else
        {
            階梯.SetActive(true);
        }
    }
    public void 回復()
    {
        go = false;
        階梯.SetActive(false);
        完成 = false;
    }
}
