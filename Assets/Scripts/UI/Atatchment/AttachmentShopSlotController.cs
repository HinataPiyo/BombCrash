using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AttachmentShopSlot.csを管理す津クラス
/// </summary>
[RequireComponent(typeof(AttachmentEquipSlotController))]
public class AttachmentShopSlotController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO player;
    [Header("SlotのPrefab : AttachmentShopSlot"), SerializeField] GameObject attachmentShopSlot_Prefab;
    [SerializeField] Transform createSlot_Paent;
    [Header("データベース"), SerializeField] AttachmentDatabase attachmentDB;
    List<AttachmentShopSlot> slots = new List<AttachmentShopSlot>();
    AttachmentEquipSlotController aesCtrl;

    void Awake()
    {
        aesCtrl = GetComponent<AttachmentEquipSlotController>();
        foreach (AttachmentDataSO data in attachmentDB.AttachmentDB)
        {
            GameObject obj = Instantiate(attachmentShopSlot_Prefab, createSlot_Paent);
            AttachmentShopSlot slot = obj.GetComponent<AttachmentShopSlot>();
            slot.SetInit(data, player, this);     // 装備の情報を渡す
            slots.Add(slot);        // リストに格納
        }
    }

    /// <summary>
    /// クリックしたパネル以外が開いていたら閉じる処理
    /// </summary>
    public void OtherPanelOnClick()
    {
        foreach (AttachmentShopSlot slot in slots)
        {
            // パネルが開いていたら
            if (slot.IsPanelOpen)
            {
                // 閉じる
                slot.ClosePanel();
            }
        }
    }

    /// <summary>
    /// attachmentを外したときの処理
    /// ボタンテキストの更新
    /// </summary>
    /// <param name="removeData">外すattachment</param>
    public void RemoveAttachment(AttachmentDataSO removeData)
    {
        foreach (AttachmentShopSlot slot in slots)
        {
            if (slot.AttachmentDataSO == removeData)
            {
                slot.CheckCanStat();       // テキストの更新
                break;
            }
        }
        aesCtrl.RemoveAttachment(removeData);
    }

    /// <summary>
    /// 装備する処理
    /// </summary>
    public void EquipOnClick(AttachmentDataSO data)
    {
        aesCtrl.EquipAttachment(data);
    }

    /// <summary>
    /// ステータス名に応じてソートする
    /// </summary>
    /// <param name="statusName"></param>
    public void GenreSort(StatusName statusName)
    {
        foreach (AttachmentShopSlot slot in slots)
        {
            if (slot.AttachmentDataSO.StatusName != statusName)
            {
                slot.gameObject.SetActive(false);       // 指定していないステータス名は非表示
                continue;
            }

            slot.ResetAnim();
            // 指定したステータス名は表示
            slot.gameObject.SetActive(true);
        }
    }
}