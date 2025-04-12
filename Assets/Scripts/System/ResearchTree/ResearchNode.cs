using UnityEngine;
using UnityEngine.UI;

public class ResearchNode : MonoBehaviour
{
    [Header("Param")]
    [SerializeField] ResearchData data;
    [SerializeField] PlayerStatusSO player;


    [Header("UI")]
    [SerializeField] Image icon;
    [SerializeField] Button button;
    public ResearchData ResearchData { get { return data; } }

    void Start()
    {
        if (data == null) return;
        ResearchTreeController r = ResearchTreeController.Instance;
        // クリックしたらパネルに自身のノード情報を渡す
        button.onClick.AddListener(() => r.NodePanel.SetResearchNode(this));

        icon.sprite = data.icon;    // アイコンの設定
    }

    /// <summary>
    /// プレイヤーのステータスに反映する
    /// </summary>
    public void AddPlayerStatus()
    {
        // Playerの所持数を減少させる
        player.ScrapHaveAmount = - data.ScrapCost;
        player.InsightPointHaveAmount = - data.InsightPointCost;

        switch(data.genre)
        {
            case ResearchesGenre.Support:
                SupportCountUp();
                break;
            case ResearchesGenre.Bomb:
                BombCountUp();
                break;
        }
    }

    /// <summary>
    /// プレイヤーの所持数が超えているか調べる
    /// </summary>
    public bool PlayerHasExceededTheLimit()
    {
        return player.ScrapHaveAmount >= data.ScrapCost && 
        player.InsightPointHaveAmount >= data.InsightPointCost;
    }

    /// <summary>
    /// ボタンのステートによって色が変わる
    /// </summary>
    /// <param name="interactable"></param>
    public void CanClickButton()
    {
        switch(data.state)
        {
            case ResearchState.Locked:
                icon.color = new Color32(130, 130, 130, 255);
                break;
            case ResearchState.Unlocked:
            case ResearchState.Completed:
                icon.color = Color.white;
                break;
        }
    }

    /// <summary>
    /// Playerの所持数が超えているか確認する
    /// </summary>
    /// <returns></returns>
    public bool ClearParam()
    {
        return player.ArrivalWave >= data.RequiredWave
        && player.ScrapHaveAmount >= data.ScrapCost
        && player.InsightPointHaveAmount >= data.InsightPointCost
        && data.ClearPrerequisites();
    }

    void BombCountUp()
    {
        // 自身に設定されている研究内容に当てはめる
        switch(data.statusName)
        {
            case StatusName.ExplosionRadiusUp:
                player.Bomb_RC.ExplosionRadiusUp = 1;
                break;
            case StatusName.BombCreateSpeedUp:
                player.Bomb_RC.BombCreateSpeedUp = 1;
                break;
            case StatusName.BombStockAmountUp:
                player.Bomb_RC.BombStockAmountUp = 1;
                break;
            case StatusName.BombAttackDamageUp:
                player.Bomb_RC.AttackDamageUp = 1;
                break;
            case StatusName.ThrowAmountUp:
                player.Bomb_RC.ThrowAmountUp = 1;
                break;
        }
    }

    void SupportCountUp()
    {
        // 自身に設定されている研究内容に当てはめる
        switch(data.statusName)
        {
            case StatusName.DropScrapUp:
                player.Support_RC.ScrapBonusUp = 1;
                break;
            case StatusName.GetInsightPointUp:
                player.Support_RC.InsightBonusUp = 1;
                break;
        }
    }
}