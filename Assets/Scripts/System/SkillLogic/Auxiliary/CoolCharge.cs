    using UnityEngine;

[CreateAssetMenu(fileName = "CoolCharge", menuName = "Skills/CoolCharge")]
public class CoolCharge : SkillSO
{
    public override void Execute()
    {
        // 補助処理
        Debug.Log("補助スキルを発動しました。");
    }
}