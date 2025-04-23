using UnityEngine;

public class InventoryController : MonoBehaviour
{
    OtomoSkillManager otomo_SkillManager;
    [SerializeField] GameObject skillSlot_Prefab;
    [SerializeField] Transform skillSlot_Parent;
    void Start()
    {
        otomo_SkillManager = OtomoSkillManager.Instance;
        for(int ii = 0; ii < otomo_SkillManager.SkillSoTabel.Length; ii++)
        {
            // 時間的に厳しいしスキルも一旦少な目だから一緒のインベントリに格納する。
            GameObject obj = Instantiate(skillSlot_Prefab, skillSlot_Parent);
            OtomoSkillInventorySlot slot = obj.GetComponent<OtomoSkillInventorySlot>();
            slot.SetSkill(otomo_SkillManager.SkillSoTabel[ii]);     // 生成したスロットにスキルを格納する
        }
    }
}