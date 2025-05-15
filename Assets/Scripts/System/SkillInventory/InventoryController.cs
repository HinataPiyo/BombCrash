using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    OtomoSkillManager otomo_SkillManager;
    [SerializeField] GameObject skillSlot_Prefab;
    [SerializeField] Transform skillSlot_Parent;
    [SerializeField] Button equipmentChange_BackButton;

    List<OtomoSkillInventorySlot> skillSlots_Array = new List<OtomoSkillInventorySlot>();     // スキルのインベントリの配列

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else Destroy(gameObject);
    }
    void Start()
    {
        otomo_SkillManager = OtomoSkillManager.Instance;
        equipmentChange_BackButton.onClick.AddListener(BackButtonOnClick);
        // スキルのインベントリを生成する
        CreateSkillInventory();
    }

    /// <summary>
    /// スキルのインベントリを生成する
    /// </summary>
    void CreateSkillInventory()
    {
        // スキルのインベントリを生成する
        for (int ii = 0; ii < otomo_SkillManager.SkillSoTabel.Length; ii++)
        {
            // スキルのインベントリを生成する
            GameObject skillSlot = Instantiate(skillSlot_Prefab, skillSlot_Parent);
            // スキルのインベントリにスキルをセットする
            OtomoSkillInventorySlot skillSlotComponent = skillSlot.GetComponent<OtomoSkillInventorySlot>();
            skillSlotComponent.SetSkill(otomo_SkillManager.SkillSoTabel[ii]);
            skillSlots_Array.Add(skillSlotComponent);
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


    /// <summary>
    /// スキルのインベントリを更新する
    /// </summary>
    /// <param name="skill"></param>
    public void ProficiencyUpSlotUpdate(SkillSO skill)
    {
        foreach (var slot in skillSlots_Array)
        {
            // スキルのインベントリを更新する
            if (slot.SkillSO == skill)
            {
                slot.SetSkill(skill);       // スロットを更新する
            }
        }
    }
}