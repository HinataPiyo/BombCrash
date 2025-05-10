using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ColdShotLogic", menuName = "Skills/ColdShot")]
public class ColdShot : SkillLogicBase
{
    [Header("スキルが発動する前に行う動作"), SerializeField] GameObject trigger_Preafab;    // 爆弾を投げるなど

    public override IEnumerator ExecuteFlow()
    {
        // 攻撃処理
        Debug.Log("攻撃スキルを発動しました。");
        Transform clossTo0Enemy = WaveManager.Instance.CrossTo0Enemy();
        if (clossTo0Enemy != null)
        {
            // スキルの発動
            GameObject obj = Instantiate(trigger_Preafab, GameSystem.Instance.Otomo.transform.position, Quaternion.identity);
            obj.GetComponent<ColdShotBomb>().ExplosionPoint = clossTo0Enemy.position;
        }
        else
        {
            Debug.Log("カウントダウンが0になる敵がいません。");
        }

        yield break;
    }
}
