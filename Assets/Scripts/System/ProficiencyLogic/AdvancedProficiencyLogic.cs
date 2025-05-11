using UnityEngine;

public class AdvancedProficiencyLogic : ProficiencyLogic
{
    public override void LevelUp()
    {
        base.LevelUp(); // 基本のレベルアップ処理を実行
        Debug.Log("Advanced Level Up! Additional effects can be added here.");
    }
}
