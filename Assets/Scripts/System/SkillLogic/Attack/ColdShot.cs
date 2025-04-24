using UnityEngine;

[CreateAssetMenu(fileName = "ColdShotLogic", menuName = "Skills/ColdShot")]
public class ColdShot : SkillLogicBase
{
    public override void Execute()
    {
        // 攻撃処理
        Debug.Log("攻撃スキルを発動しました。");
    }


}
