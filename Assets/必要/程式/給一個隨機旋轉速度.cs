using UnityEngine;

public class 給一個隨機旋轉速度 : MonoBehaviour
{
    public bool x;
    public bool y=true;
    public bool z;
    public float 速度低,速度高;
    public bool 格時間變化一次;
    public float 時長;
    private float cd=0;
    private void Update()
    {
        給一個隨機旋轉();
    }
    private void 給一個隨機旋轉()
    {
        if (格時間變化一次)
        {
            if (cd < 時長)
            {
                cd += 1 * Time.deltaTime;
                if (x)
                {
                    transform.Rotate(Random.Range(速度低, 速度高), 0, 0);
                }
                if (y)
                {
                    transform.Rotate(0, Random.Range(速度低, 速度高), 0);
                }
                if (z)
                {
                    transform.Rotate(0, 0, Random.Range(速度低, 速度高));
                }
            }
            else
            {
                cd = 0;
            }
        }
        else
        {
            if (x)
            {
                transform.Rotate(Random.Range(速度低, 速度高), 0, 0);
            }
            if (y)
            {
                transform.Rotate(0, Random.Range(速度低, 速度高), 0);
            }
            if (z)
            {
                transform.Rotate(0, 0, Random.Range(速度低, 速度高));
            }
        }
    }
}
