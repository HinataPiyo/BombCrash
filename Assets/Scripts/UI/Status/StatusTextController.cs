using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusTextController : MonoBehaviour
{
    [Header("値が格納されているやつ")]
    [SerializeField] PlayerStatusSO playerSO;
    [SerializeField] BombSO bombSO;
    [SerializeField] BasicUpgradeData b_UpDataSO;

    [Header("ステータステキストの親オブジェクト")]
    [SerializeField] Transform bomb_StatusElementParent;
    [SerializeField] Transform support_StatusElementParent;
    StatusTextElement[] bomb_StatusElements;
    StatusTextElement[] support_StatusElements;

    private Dictionary<int, Func<string>> bomb_StatusValueMapping;
    private Dictionary<int, Func<string>> support_StatusValueMapping;

    void Start()
    {
        bomb_StatusElements = bomb_StatusElementParent.GetComponentsInChildren<StatusTextElement>();
        support_StatusElements = support_StatusElementParent.GetComponentsInChildren<StatusTextElement>();

        // マッピングの初期化
        InitializeStatusValueMapping();

        // ステータス値を設定
        SetStatusValue();
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
            { 0, () => (b_UpDataSO.GetSupportData(StatusName.DropScrapUp).increaseValue * 100).ToString("F1") + "%" },
            { 1, () => (b_UpDataSO.GetSupportData(StatusName.GetInsightPointUp).increaseValue * 100).ToString("F1") + "%" },
        };
    }

    public void SetStatusValue()
    {
        for (int ii = 0; ii < bomb_StatusElements.Length; ii++)
        {
            if (bomb_StatusValueMapping.TryGetValue(ii, out var getValue))
            {
                bomb_StatusElements[ii].ValueText.text = getValue();
            }
        }

        for (int ii = 0; ii < support_StatusElements.Length; ii++)
        {
            if (support_StatusValueMapping.TryGetValue(ii, out var getValue))
            {
                support_StatusElements[ii].ValueText.text = getValue();
            }
        }
    }
}