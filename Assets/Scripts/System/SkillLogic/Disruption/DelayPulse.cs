using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DelayPulse", menuName = "Skill/Skills/DelayPulse")]
public class DelayPulse : SkillSO
{
    static readonly float[] delayTime = new float[] { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f };
    [Header("スキルが発動する前に行う動作"), SerializeField] GameObject trigger_Preafab;    // 爆弾を投げるなど
    [Header("発火したときのエフェクト"), SerializeField] GameObject fire_Prefab;
    [Header("敵に当たったエフェクト"), SerializeField] GameObject hitEnemy_Prefab;

    const float throwForce = 4f;

    /// <summary>
    /// 移動処理(爆弾が爆発点に向かう)
    /// </summary>
    /// <param name="pos">生成された爆弾</param>
    public override void Execute()
    {
        GameSystem.Instance.StartCoroutine(Flow());
    }

    IEnumerator Flow()
    {
        Debug.Log("妨害スキルを発動しました。");
        Transform center = GameSystem.Instance.Center;
        Transform pos = Instantiate(trigger_Preafab, GameSystem.Instance.Otomo.transform.position, Quaternion.identity).transform;

        while (Vector3.Distance(center.position, pos.position) > 0.05f)
        {
            Vector3 dir = center.position - pos.position;
            pos.position += dir * throwForce * Time.deltaTime;
            yield return null;
        }

        Destroy(pos.gameObject);
        Instantiate(fire_Prefab, center.position, Quaternion.identity);

        foreach (var enemy in WaveManager.Instance.GetEnemyList())
        {
            GameObject effect_obj = Instantiate(hitEnemy_Prefab, enemy.transform);
            enemy.GetComponent<EnemyBombController>().DelayTime = delayTime[AwakeningCount];
            effect_obj.GetComponent<DelayDestroy>().WaitTime = delayTime[AwakeningCount];
        }
    }

    /// <summary>
    /// スキルの説明を返す
    /// </summary>
    public override string GetEffectDiscription(int awakeningCount)
    {
        if (awakeningCount > MaxAwakeningCount) awakeningCount = MaxAwakeningCount + 1;
        string delay = $"{delayTime[awakeningCount]}秒";
        return $"敵のカウントダウンを{SystemDefine.GetConvertColorText(ConvertColor.Red, delay)}の間止める。(CT: {GetDecCoolTime(awakeningCount)})";
    }

    /// <summary>
    /// 段階的にクールタイムの減少値を確定
    /// </summary>
    public override float GetDecCoolTime(int awakeningCount)
    {
        if (awakeningCount > MaxAwakeningCount) awakeningCount = MaxAwakeningCount + 1;
        float[] decCT = { 0, 0, 0.5f, 1, 1.5f, 2f };
        return coolTime - decCT[awakeningCount];
    }
}