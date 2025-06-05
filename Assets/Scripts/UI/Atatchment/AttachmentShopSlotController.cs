using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AttachmentShopSlot.csを管理す津クラス
/// </summary>
public class AttachmentShopSlotController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO player;
    [Header("SlotのPrefab : AttachmentShopSlot"), SerializeField] GameObject attachmentShopSlot_Prefab;
    [SerializeField] Transform createSlot_Paent;
    [Header("データベース"), SerializeField] AttachmentDatabase attachmentDB;
    List<AttachmentShopSlot> slots = new List<AttachmentShopSlot>();

    void Awake()
    {
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
}