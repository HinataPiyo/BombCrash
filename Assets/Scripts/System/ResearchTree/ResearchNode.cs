using UnityEngine;
using UnityEngine.UI;

public class ResearchNode : MonoBehaviour
{
    [Header("Param")]
    [SerializeField] ResearchesGenre genre;
    [SerializeField] ResearchData default_Data;
    [SerializeField] PlayerStatusSO player;

    [Header("Status")]
    [SerializeField] Data m_data;
    [System.Serializable]
    public class Data
    {
        public int scrapCost;
        public int wavePointCost;
        public int requiredWave;
    }

    [Header("UI")]
    [SerializeField] Image icon;
    [SerializeField] Button button;
    public ResearchData ResearchData { get { return default_Data; } }
    public Data M_Data { get { return m_data; } }

    void Start()
    {
        if (default_Data == null) return;

        ResearchTreeController r = ResearchTreeController.Instance;
        // リスナー登録
        button.onClick.AddListener(() => r.CompleteResearch(this, genre));

        icon.sprite = default_Data.icon;    // アイコンの設定
        SetParam();
    }

    /// <summary>
    /// 階層ごとにパラメータを変動させる
    /// </summary>
    void SetParam()
    {
        m_data.requiredWave = default_Data.GetDynamicWaveCost();
        m_data.scrapCost = default_Data.GetDynamicScrapCost();
        m_data.wavePointCost = default_Data.GetDynamicWavePointCost();
    }

    // 研究条件がそろっていたらボタンが押せるようになる。
    public void CanClickButton(bool interactable)
    {
        button.interactable = interactable;

        switch(default_Data.state)
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
        return player.ArrivalWave >= m_data.requiredWave
        && player.ScrapHaveAmount >= m_data.scrapCost
        && player.WavePointHaveAmount >= m_data.wavePointCost
        && default_Data.ClearPrerequisites();
    }
}