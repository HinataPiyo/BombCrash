using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    OtomoSkillManager otomo_SkillManager;
    [SerializeField] GameObject skillSlot_Prefab;
    [SerializeField] Transform skillSlot_Parent;
    [SerializeField] Button equipmentChange_BackButton;
    void Start()
    {
        otomo_SkillManager = OtomoSkillManager.Instance;
        equipmentChange_BackButton.onClick.AddListener(BackButtonOnClick);
        for(int ii = 0; ii < otomo_SkillManager.SkillSoTabel.Length; ii++)
        {
            // 時間的に厳しいしスキルも一旦少な目だから一緒のインベントリに格納する。
            GameObject obj = Instantiate(skillSlot_Prefab, skillSlot_Parent);
            OtomoSkillInventorySlot slot = obj.GetComponent<OtomoSkillInventorySlot>();
            slot.SetSkill(otomo_SkillManager.SkillSoTabel[ii]);     // 生成したスロットにスキルを格納する
        }
    }

    /// <summary>
    /// スキル変更中に表示する「終了」ボタン
    /// </summary>
    void BackButtonOnClick()
    {
        // スキル変更中フラグを折る
        OtomoSkillManager.Instance.isEquipmentChangeNow = false;
        // 終了時のアニメーションを再生
        OtomoPanelChange.Instance.SkillEquipment_EndBack();
    }
}