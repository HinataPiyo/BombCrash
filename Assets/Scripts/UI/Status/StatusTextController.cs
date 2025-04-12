using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusTextController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerSO;
    [SerializeField] BombSO bombSO;
    [SerializeField] Transform statusElementParent;
    StatusTextElement[] statusElements;

    private Dictionary<int, Func<string>> statusValueMapping;

    void Start()
    {
        statusElements = statusElementParent.GetComponentsInChildren<StatusTextElement>();

        // マッピングの初期化
        InitializeStatusValueMapping();

        // ステータス値を設定
        SetStatusValue();
    }

    void InitializeStatusValueMapping()
    {
        statusValueMapping = new Dictionary<int, Func<string>>
        {
            { 0, () => bombSO.AttackDamage.ToString("F1") },
            { 1, () => playerSO.CriticalDamage.ToString("F1") },
            { 2, () => playerSO.CriticalChance.ToString("F1") },
            { 3, () => (playerSO.MaxHaveBomb + 1).ToString("F0") },
            { 4, () => bombSO.ExplosionRadius.ToString("F2") },
            { 5, () => playerSO.CreateBombTime.ToString("F2") }
        };
    }

    public void SetStatusValue()
    {
        for (int ii = 0; ii < statusElements.Length; ii++)
        {
            if (statusValueMapping.TryGetValue(ii, out var getValue))
            {
                statusElements[ii].ValueText.text = getValue();
            }
        }

        Debug.Log("ステータステキストを更新しました");
    }
}