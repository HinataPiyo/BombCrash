using UnityEngine;

[CreateAssetMenu(fileName = "ColdShot", menuName = "Skill/Skills/ColdShot")]
public class ColdShot : SkillSO
{
    [Header("スキルが発動する前に行う動作"), SerializeField] GameObject trigger_Preafab;    // 爆弾を投げるなど

    public override void Execute()
    {
        // 攻撃処理
        Debug.Log("攻撃スキルを発動しました。");
        Transform clossTo0Enemy = WaveManager.Instance.GetCrossTo0Enemy();
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
    }
}
