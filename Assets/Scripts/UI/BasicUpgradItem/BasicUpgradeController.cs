using System.Linq;
using UnityEngine;

public class BasicUpgradeController : MonoBehaviour
{
    [SerializeField] BasicUpgradeData b_UpDataSO;
    [SerializeField] Transform b_UpItemBombParent;
    [SerializeField] Transform b_UpItemSupportParent;
    BasicUpgradeItem[] b_UpItemBomb;
    BasicUpgradeItem[] b_UpItemSupport;

    void Awake()
    {
        b_UpDataSO.InitSetData();       // リストの初期k（レベルに追うおじて必要消費量を更新）
    }


    void Start()
    {
        b_UpItemBomb = b_UpItemBombParent.GetComponentsInChildren<BasicUpgradeItem>();
        b_UpItemSupport = b_UpItemSupportParent.GetComponentsInChildren<BasicUpgradeItem>();
        
        SetTextsValue();                // 上記の処理が終わったら、テキストを更新する
    }

    /// <summary>
    /// UpgradeItem のテキストを設定する
    /// </summary>
    public void SetTextsValue()
    {
        // データを逆順にする
        var reversedData_bomb = b_UpDataSO.PlayerDatas.Reverse().ToArray();
        var reversedData_support = b_UpDataSO.SupportDatas.Reverse().ToArray();

        for (int ii = 0; ii < reversedData_bomb.Length; ii++)
        {
            b_UpItemBomb[ii].SetData(reversedData_bomb[ii]);
        }

        
        for (int ii = 0; ii < reversedData_support.Length; ii++)
        {
            b_UpItemSupport[ii].SetData(reversedData_support[ii]);
        }
    }

}
