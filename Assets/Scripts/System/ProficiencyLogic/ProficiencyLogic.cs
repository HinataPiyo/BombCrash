using UnityEngine;

public class ProficiencyLogic : MonoBehaviour
{
    public SkillSO skillSO;
    public int currentProficiency = 1;
    public float requiredProficiency = 10;
    public float proficiencyMultiplier = 1.2f;
    public float maxProficiency = 30;

    private void Awake() 
    {
        ////skillSO.CurrentProficiency = currentProficiency;
    }

    public virtual void LevelUp()
    {
        if (requiredProficiency >= maxProficiency)
        {
            maxProficiency *= proficiencyMultiplier;
            currentProficiency++;
            requiredProficiency = 0;

            // 必要に応じて熟練度の更新をSkillSOに反映
            ////skillSO.CurrentProficiency = currentProficiency;
        }
    }

    public void AddProficiency(float amount)
    {
        requiredProficiency += amount;

        if (requiredProficiency >= maxProficiency)
        {
            LevelUp();
        }
    }
}
