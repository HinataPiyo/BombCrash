using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusTextController : MonoBehaviour
{
    [Header("値が格納されているやつ")]
    [SerializeField] PlayerStatusSO playerSO;
    [SerializeField] BombSO bombSO;

    [Header("ステータステキストの親オブジェクト")]
    [SerializeField] Transform bomb_StatusElementParent;
    [SerializeField] Transform support_StatusElementParent;
    StatusTextElement[] bomb_StatusElements;
    StatusTextElement[] support_StatusElements;

    Dictionary<int, Func<string>> bomb_StatusValueMapping;
    Dictionary<int, Func<string>> support_StatusValueMapping = new Dictionary<int, Func<string>>();

    void Awake()
    {
        bomb_StatusElements = bomb_StatusElementParent.GetComponentsInChildren<StatusTextElement>();
        support_StatusElements = support_StatusElementParent.GetComponentsInChildren<StatusTextElement>();
        // マッピングの初期化
        InitializeStatusValueMapping();
    }

    void Start()
    {
        // ステータス値を設定
        SetStatusValue();
        EquipUpdateText();
    }

    void InitializeStatusValueMapping()
    {
        bomb_StatusValueMapping = new Dictionary<int, Func<string>>
        {
            { 0, () => bombSO.AttackDamage.ToString("F1") },
            { 1, () => (playerSO.CriticalDamage * 100).ToString("F1") + "%"},
            { 2, () => (playerSO.CriticalChance * 100).ToString("F1") + "%"},
            { 3, () => (playerSO.MaxHaveBomb + 1).ToString("F0") },
            { 4, () => bombSO.ExplosionRadius.ToString("F2") + "m"},
            { 5, () => playerSO.CreateBombTime.ToString("F2") + "s"}
        };

        support_StatusValueMapping = new Dictionary<int, Func<string>>
        {
            { 0, () => "-" },
            { 1, () => "-" },
        };
    }

    public void SetStatusValue()
    {
        for (int ii = 0; ii < bomb_StatusElements.Length; ii++)
        {
            if (bomb_StatusValueMapping.TryGetValue(ii, out var getValue))
            {
                bomb_StatusElements[ii].BasicValueText.text = getValue();
            }
        }

        for (int ii = 0; ii < support_StatusElements.Length; ii++)
        {
            if (ii > support_StatusValueMapping.Count) return;
            if (support_StatusValueMapping.TryGetValue(ii, out var getValue))
            {
                support_StatusElements[ii].BasicValueText.text = getValue();
            }
        }
    }

    /// <summary>
    /// 装備用テキストの更新
    /// </summary>
    public void EquipUpdateText()
    {
        for (int ii = 0; ii < bomb_StatusElements.Length; ii++)
        {
            if (ii < SystemDefine.bombStatusNames.Length)
            {
                // 該当のステータスを合算させた値を取得
                float total = playerSO.CheckAttachmentStatusName(SystemDefine.bombStatusNames[ii]);
                if (total == 0)
                {
                    bomb_StatusElements[ii].EquipValueText.text = "";
                    continue;
                }

                bomb_StatusElements[ii].EquipValueText.text = $"(+{total})";
            }
            else
            {
                bomb_StatusElements[ii].EquipValueText.text = "";
            }
        }

        for (int ii = 0; ii < support_StatusElements.Length; ii++)
        {
            support_StatusElements[ii].EquipValueText.text = "";
        }

        SetStatusValue();
    }
}