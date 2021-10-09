using UnityEngine;
using System.Collections;

public class 近戰 : 怪物控制
{
    [Header("攻擊區域")]
    public Vector2 v2攻擊區域 = new Vector2(1.8f, 0);
    public Vector3 v3尺寸 = new Vector3(1, 2, 0.2f);
    protected override void OnDrawGizmos()
    {
        //副類別原本的程式內容
        base.OnDrawGizmos();
        Gizmos.color = new Color(1, 0.3f, 0.1f, 0.3f);
        Gizmos.DrawCube(transform.position + transform.right * v2攻擊區域.x + transform.up * v2攻擊區域.y, v3尺寸);
    }
    protected override void Update()
    {
        base.Update();
        atkk();
    }

    private void atkk()
    {
        hit = Physics2D.OverlapBox(transform.position + transform.right * v2攻擊區域.x + transform.up * v2攻擊區域.y, v3尺寸, 0, 1 << 0);
        //Collider2D hit = Physics2D.OverlapBox(transform.position + transform.right * v2攻擊區域.x + transform.up * v2攻擊區域.y, v3尺寸, 0, 1 << 0);
        if (hit)
        {
            if (hit.tag == "Player")
            {
                動作區 = StateEnemy.攻擊;
            }
            //print(hit.name);
        }
    }
    protected override void 攻擊()
    {
        base.攻擊();
        StartCoroutine(playerie());
    }
    public IEnumerator playerie()       //ctrl+k+d 格式化   alt+上下 移動所選
    {
        for (int i = 0; i < atkdelay.Length; i++)
        {
            yield return new WaitForSeconds(atkdelay[i]);
            //print("第一次攻擊");
            if (hit)
            {
                playerh.受傷(atk);
            }
        }
        yield return new WaitForSeconds(攻擊回復time);
        if (hit)
        {
            if (hit.tag == "Player")
            {
                動作區 = StateEnemy.攻擊;
            }
        }
        else if (追敵狀態 && !hit)
        {
            動作區 = StateEnemy.追敵;
        }
        else
        {
            動作區 = StateEnemy.走路;
        }
    }
    public void 取消輸出傷害()
    {
        CancelInvoke("playerie");
        動作區 = StateEnemy.追敵;
    }
}
