using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DelayPulseLogic", menuName = "SkillLogic/DelayPulse")]
public class DelayPulse : SkillLogicBase
{
    [SerializeField] float delayTime = 1f;
    [Header("スキルが発動する前に行う動作"), SerializeField] GameObject trigger_Preafab;    // 爆弾を投げるなど
    [Header("発火したときのエフェクト"), SerializeField] GameObject fire_Prefab;
    [Header("敵に当たったエフェクト"), SerializeField] GameObject hitEnemy_Prefab;

    const float throwForce = 4f;

    /// <summary>
    /// 移動処理(爆弾が爆発点に向かう)
    /// </summary>
    /// <param name="pos">生成された爆弾</param>
    public override IEnumerator ExecuteFlow()
    {
        Debug.Log("妨害スキルを発動しました。");
        Transform center = GameSystem.Instance.Center;
        Transform pos = Instantiate(trigger_Preafab, GameSystem.Instance.Otomo.transform.position, Quaternion.identity).transform;
        
        while(Vector3.Distance(center.position, pos.position) > 0.05f)
        {
            Vector3 dir = center.position - pos.position;
            pos.position += dir * throwForce * Time.deltaTime;
            yield return null;
        }

        Destroy(pos.gameObject);
        Instantiate(fire_Prefab, center.position, Quaternion.identity);

        foreach(var enemy in WaveManager.Instance.EnemyList)
        {
            GameObject effect_obj = Instantiate(hitEnemy_Prefab, enemy.transform);
            enemy.GetComponent<EnemyBombController>().DelayTime = delayTime;
            effect_obj.GetComponent<DelayDestroy>().WaitTime = delayTime;
        }
    }
}