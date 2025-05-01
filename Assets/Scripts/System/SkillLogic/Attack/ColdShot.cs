using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ColdShotLogic", menuName = "Skills/ColdShot")]
public class ColdShot : SkillLogicBase
{
    public override IEnumerator ExecuteFlow()
    {
        // 攻撃処理
        Debug.Log("攻撃スキルを発動しました。");
        yield break;
    }
}
