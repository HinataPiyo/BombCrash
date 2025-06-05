using UnityEngine;

[CreateAssetMenu(fileName = "AttachmentDataSO", menuName = "SO/AttachmentDataSO")]
public class AttachmentDataSO : ScriptableObject
{
    [SerializeField] protected PlayerStatusSO psSO;
    [SerializeField] new string name;           // 名前
    [SerializeField] string discription;        // 説明
    [SerializeField] Sprite sprite;             // 画像
    [SerializeField] StatusName statusName;     // 強化したいステータスネーム
    [SerializeField] float upgradeValue;       // 強化率

    public StatusName StatusName => statusName;
    public float UpgreadeValue => upgradeValue;

    public float GetUpgradeValue(StatusName upgradName)
    {
        // 強化内容が同じだったら
        if (statusName == upgradName) return upgradeValue;
        return 0;
    }
}