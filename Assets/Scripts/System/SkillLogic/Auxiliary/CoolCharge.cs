using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CoolCharge", menuName = "Skill/Skills/CoolCharge")]
public class CoolCharge : SkillSO
{
    // 効果量(五段階に分けて)
    static readonly float[] effectValue = new float[] { 0.2f, 0.4f, 0.6f, 0.8f };   // %
    static readonly float[] applyTime = new float[] { 2f, 3f, 4f, 5f, 6f };     // 秒

    ThrowBomb throwBomb;

    public override void Execute()
    {
        // 補助処理
        Debug.Log("補助スキルを発動しました。");

        throwBomb = GameSystem.Instance.Player.GetComponent<ThrowBomb>();
        throwBomb.CreateBombTimeUp(true, effectValue[AwakeningCount]);       // 効果適用
        GameSystem.Instance.StartCoroutine(ApplyEffectTime());
    }

    /// <summary>
    /// 効果適用時間
    /// </summary>
    IEnumerator ApplyEffectTime()
    {
        yield return new WaitForSeconds(applyTime[AwakeningCount]);
        throwBomb.CreateBombTimeUp(false, 0);      // デフォルト値に戻す
    }

    /// <summary>
    /// スキルの説明を返す
    /// </summary>
    public override string GetEffectDiscription(int awakeningCount)
    {
        string applay = $"{applyTime[awakeningCount]}秒間";
        string effect = $"{effectValue[awakeningCount]}%";
        return $"凍結爆弾の生成速度が{SystemDefine.GetConvertColorText(ConvertColor.Red, applay)}、{SystemDefine.GetConvertColorText(ConvertColor.Red, effect)}上昇する。(CT: {GetDecCoolTime(awakeningCount)})";
    }

    /// <summary>
    /// 段階的にクールタイムの減少値を確定
    /// </summary>
    public override float GetDecCoolTime(int awakening)
    {
        float[] decCT = { 0, 0, 0.5f, 1, 2.5f };
        return coolTime - decCT[awakening];
    }
}