using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ドロップダウンのStatusNameGenreのSlotを管理するクラス
/// </summary>
public class DropdownGenreSelectButtonController : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] AttachmentShopSlotController assCtrl;
    [Header("Slot")]
    [SerializeField] GameObject genreSelectButton_Prefab;
    [SerializeField] Transform slot_Parent;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI selectGenreText;
    [SerializeField] Button dropButton;
    [Header("Animation")]
    [SerializeField] Animator anim;
    List<DropdownGenreSelectButton> slots = new List<DropdownGenreSelectButton>();
    bool isPanelOpen;

    void Awake()
    {
        dropButton.onClick.AddListener(DropOnClick);
        for (int ii = 0; ii < PlayerStatusSO.bombStatusNames.Length; ii++)
        {
            GameObject obj = Instantiate(genreSelectButton_Prefab, slot_Parent);
            DropdownGenreSelectButton slot = obj.GetComponent<DropdownGenreSelectButton>();

            slot.SetInit(PlayerStatusSO.bombStatusNames[ii], this);
            slots.Add(slot);
        }
    }

    void Start()
    {
        SetSelectGenre(StatusName.BombAttackDamageUp);
    }

    /// <summary>
    /// ジャンルを決定する処理
    /// </summary>
    /// <param name="statusName"></param>
    public void SetSelectGenre(StatusName statusName)
    {
        selectGenreText.text = PlayerStatusSO.StatusNameToName(statusName);
        assCtrl.GenreSort(statusName);

        if (!isPanelOpen) return;
        anim.SetTrigger("Close");
        isPanelOpen = false;
    }

    /// <summary>
    /// ドロップボタンが押された時の処理
    /// </summary>
    void DropOnClick()
    {
        if (isPanelOpen)
        {
            anim.SetTrigger("Close");
            isPanelOpen = false;
            return;
        }
        anim.SetTrigger("Open");
        isPanelOpen = true;
    }
}