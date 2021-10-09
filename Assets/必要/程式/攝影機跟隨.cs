using UnityEngine;
using System.Collections;

public class 攝影機跟隨 : MonoBehaviour
{
    [Header("攝影機跟隨速度"),Range(1,5)]
    public float speed = 3;
    [Header("目標")]
    public GameObject Target;
    //[Header("目標名稱")]
    //public string nameTarget;
    public bool 左右限制;
    public bool 上下限制;
    [Header("左右限制")]
    public Vector2 le_ri_restrictions;
    [Header("上下限制")]
    public Vector2 up_lo_restrictions;
    //private Transform target;
    //public GameObject 我名;

    private void Start()
    {
    //尋找物件名稱(吃效能)
    //target = GameObject.Find(nameTarget).transform;
    //我名 = GameObject.Find("我名");
    }
    private void LateUpdate()
    {
        Track();
    }

    private void Track()
    {
        Vector3 posCamera = transform.position;
        Vector3 posTarget = Target.transform.position;
         //Lerp(a點, b點, 分割中點跟隨速度ex0.5f)
        Vector3 posResult = Vector3.Lerp(posCamera, posTarget, speed *Time.deltaTime);

        posResult.z = -10;
        if(左右限制)
        posResult.x = Mathf.Clamp(posResult.x, le_ri_restrictions.x, le_ri_restrictions.y);
        if(上下限制)
        posResult.y = Mathf.Clamp(posResult.y, up_lo_restrictions.x, up_lo_restrictions.y);

        transform.position = posResult;
    }
    [Header("晃動幅度")]
    public float 晃動幅度 = 0.02f;
    [Header("晃動次數")]
    public int 晃動次數 = 2;
    [Header("晃動間隔")]
    public float 晃動間隔 = 0.03f;
    public IEnumerator 晃動()
    {
        Vector3 原始座標 = transform.position;
        for (int i = 0; i < 晃動次數; i++)
        {
            Vector3 當下座標 = 原始座標;
            if (i % 2 == 0)
            {
                當下座標.x -= 晃動幅度;
                當下座標.y += 晃動幅度;
            }
            else
            {
                當下座標.x += 晃動幅度;
                當下座標.y -= 晃動幅度;
            }
            transform.position = 當下座標;
            yield return new WaitForSeconds(晃動間隔);
        }
        transform.position = 原始座標;
    }
}
