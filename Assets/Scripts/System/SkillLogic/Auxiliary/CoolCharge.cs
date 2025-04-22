using UnityEngine;

[CreateAssetMenu(fileName = "CoolChargeLogic", menuName = "Skills/CoolCharge")]
public class CoolCharge : SkillLogicBase
{
    public override void Execute()
    {
        // 補助処理
        Debug.Log("補助スキルを発動しました。");
    }
}