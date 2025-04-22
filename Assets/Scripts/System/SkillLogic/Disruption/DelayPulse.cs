using UnityEngine;

[CreateAssetMenu(fileName = "DelayPulseLogic", menuName = "SkillLogic/DelayPulse")]
public class DelayPulse : SkillLogicBase
{
    public override void Execute()
    {
        // 妨害処理
        Debug.Log("妨害スキルを発動しました。");
    }
}