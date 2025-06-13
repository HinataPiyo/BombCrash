using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ColdShot", menuName = "Skill/Skills/ColdShot")]
public class ColdShot : SkillSO
{
    static readonly int[] throwCount = new int[] { 1, 2, 3, 3, 3, 3 };
    static readonly float nextThrowInterval = 0.5f;
    [Header("スキルが発動する前に行う動作"), SerializeField] GameObject trigger_Preafab;    // 爆弾を投げるなど

    public override void Execute()
    {
        // 攻撃処理
        Debug.Log("攻撃スキルを発動しました。");
        GameSystem.Instance.StartCoroutine(Throw());
    }

    IEnumerator Throw()
    {
        for (int ii = 0; ii < throwCount[AwakeningCount]; ii++)
        {
            Transform clossTo0Enemy = WaveManager.Instance.GetCrossTo0Enemy();
            if (clossTo0Enemy != null)
            {
                // スキルの発動
                GameObject obj = Instantiate(trigger_Preafab, GameSystem.Instance.Otomo.transform.position, Quaternion.identity);
                obj.GetComponent<ColdShotBomb>().ExplosionPoint = clossTo0Enemy.position;

                yield return new WaitForSeconds(nextThrowInterval);
            }
            else
            {
                Debug.Log("カウントダウンが0になる敵がいません。");
            }
        }
    }

    /// <summary>
    /// スキルの説明を返す
    /// </summary>
    public override string GetEffectDiscription(int awakeningCount)
    {
        string count = $"{throwCount[awakeningCount]}個";
        return $"カウントダウンが最も0に近い敵に{SystemDefine.GetConvertColorText(ConvertColor.Red, count)}の凍結爆弾を投げる。(CT: {GetDecCoolTime(awakeningCount)})";
    }

    /// <summary>
    /// 段階的にクールタイムの減少値を確定
    /// </summary>
    public override float GetDecCoolTime(int awakeningCount)
    {
        float[] decCT = { 0, 0, 0.5f, 1, 1.5f, 2f };
        return coolTime - decCT[awakeningCount];
    }
}
