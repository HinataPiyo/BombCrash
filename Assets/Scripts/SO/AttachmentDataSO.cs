using UnityEngine;

[CreateAssetMenu(fileName = "AttachmentDataSO", menuName = "Attachment/AttachmentDataSO")]
public class AttachmentDataSO : ScriptableObject
{
    [SerializeField] new string name;           // 名前
    [SerializeField] string discription;        // 説明
    [SerializeField] Sprite icon;             // 画像
    [SerializeField] StatusName statusName;     // 強化したいステータスネーム
    [SerializeField] float upgradeValue;       // 強化率
    [SerializeField] int resourceValue;

    [Header("開発済みか否か"), SerializeField] bool isDeveloped;
    [Header("装備中か否か"), SerializeField] bool isEquiped;

    public StatusName StatusName => statusName;
    public Sprite Icon => icon;
    public string Name => name;
    public string Discription => discription;
    public StatusName UseSutatusName => statusName;
    public float UpgreadeValue => upgradeValue;
    public int ResourceValue => resourceValue;
    public bool IsDeveloped { get { return isDeveloped; } set { isDeveloped = value; } }
    public bool IsEquiped { get { return isEquiped; } set { isEquiped = value; } }

    /// <summary>
    /// 装備一覧で使用
    /// </summary>
    public float GetUpgradeValue(StatusName upgradName)
    {
        // 強化内容が同じだったら
        if (statusName == upgradName) return upgradeValue;
        return 0;
    }
}