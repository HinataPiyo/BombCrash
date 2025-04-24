using UnityEngine;
using UnityEngine.Playables;

public class SkillSlotController : MonoBehaviour
{
    [SerializeField] Transform equipmentSkillSlot_Parent;
    [SerializeField] OtomoSkillStatusSlot[] equipmentSkillSlot;

    OtomoSkillStatusSlot currentSelectSlot;
    public OtomoSkillStatusSlot CurrentEquipmentSkillSlot => currentSelectSlot;
    void Start()
    {
        equipmentSkillSlot = equipmentSkillSlot_Parent.GetComponentsInChildren<OtomoSkillStatusSlot>();
        
        for(int ii = 0; ii < equipmentSkillSlot.Length; ii++)
        {
            int index = ii;
            // リスナー登録
            equipmentSkillSlot[ii].SkillChangeButton.onClick.AddListener(
                () => SkillChangeEquipment(index)
            );

            // スロットがロックされているか確認
            equipmentSkillSlot[ii].LockedSlot();
            // スロットの中身が空だった場合テキストの表示を簡素化する
            equipmentSkillSlot[ii].SkillNullSlot();
        }
    }

    /// <summary>
    /// スキルの切り替え開始
    /// </summary>
    void SkillChangeEquipment(int slotNo)
    {
        // ディレクターが再生中だった場合処理を終了
        if(OtomoPanelChange.Instance.Director.state == PlayState.Playing) return;
        // スロットがロック状態か確認する、ロック状態だったら処理を終了
        if(equipmentSkillSlot[slotNo].SlotState == SkillEquipmentState.Locked) return;
        // 装備変更中に別の装備スロットをクリックしたときに備えて
        if(OtomoSkillManager.Instance.isEquipmentChangeNow == false)
        {
            OtomoPanelChange.Instance.SkillEquipmentChanges();
        }

        // スキル変更中フラグを立てる
        OtomoSkillManager.Instance.isEquipmentChangeNow = true;
        
        // 現在選択されている装備スロットを格納する
        currentSelectSlot = equipmentSkillSlot[slotNo];

        // アニメーション再生
        currentSelectSlot.ClickAnimation();
    }

    /// <summary>
    /// スキル一覧から選ばれたスキルをセットする
    /// </summary>
    public void SkillChange_SetSkill(SkillSO skillSO)
    {
        for(int ii = 0; ii < equipmentSkillSlot.Length; ii++)
        {
            // 選択したスキルが既に装備されているか確認
            if(equipmentSkillSlot[ii].SkillSO == skillSO)
            {
                // 同じスキルがあったら装備せず処理を終了
                return;
            }
        }

        // スキル一覧から選ばれたスキルをセットする
        currentSelectSlot.SetSkill(skillSO);
        OtomoSkillManager.Instance.EquippedSkill.Add(skillSO);

        // アニメーション再生
        currentSelectSlot.ClickAnimation();
    }
}