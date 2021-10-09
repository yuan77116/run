using UnityEngine;
using System.Collections;

public class 呼叫火球 : MonoBehaviour
{
    public GameObject 火球欲置物;
    public Transform 生成位置;
    public float 生成cd;
    private float time;
    private bool 可bo;
    private bool 開始;
    private void Start()
    {
        StartCoroutine(一次());
        開始 = false;
    }
    IEnumerator 一次()
    {
        yield return new WaitForSeconds(Random.Range(0, 4));
        呼火球();
        開始 = true;
    }
    private void Update()
    {
        if (開始)
        {
            if (可bo)
            {
                呼火球();
                可bo = false;
            }
            else
            {
                if (time < 生成cd)
                {
                    time += 1 * Time.deltaTime;
                }
                else
                {
                    time = 0;
                    可bo = true;
                }
            }
        }
    }
    private void 呼火球()
    {
        Instantiate(火球欲置物, 生成位置);
    }
}
