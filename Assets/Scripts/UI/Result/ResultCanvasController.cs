using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultCanvasController : MonoBehaviour
{
    public static ResultCanvasController Instance;

    [SerializeField] WaveManager waveManager;
    [SerializeField] PlayerStatusSO playerSO;
    [SerializeField] InsightPointCalculation insightPointCalculation;
    [Header("到達WAVE数")]
    [SerializeField] TextMeshProUGUI waveText;
    [Header("スクラップ")]
    [SerializeField] TextMeshProUGUI scrapText;
    [SerializeField] TextMeshProUGUI scrapBonusText;
    [SerializeField] TextMeshProUGUI scrapTotalText;
    [Header("知見ポイント")]
    [SerializeField] TextMeshProUGUI insightText;
    [SerializeField] TextMeshProUGUI insightBonusText;
    [SerializeField] TextMeshProUGUI insightTotalText;

    [Header("帰還ボタン")]
    [SerializeField] Button returnButton;

    [SerializeField] ResultData result;
    int beforScrap;
    int beforInsight;

    public ResultData Data { get { return result; } }

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        returnButton.onClick.AddListener(ReturnButtonOnClick);
        result = new ResultData();
        beforScrap = playerSO.ScrapHaveAmount;
        beforInsight = playerSO.InsightPointHaveAmount;
    }

    public void SetResultTextValue()
    {
        result.waveCount = waveManager.WaveCount;
        result.scrap = playerSO.ScrapHaveAmount - beforScrap;
        result.scrapBonus = (int)Mathf.Round(result.scrap * playerSO.CheckAttachmentStatusName(StatusName.DropScrapUp));
        result.scrapTotal = result.scrap + result.scrapBonus;

        int insight = insightPointCalculation.GetDefaultInsight();              // 知見ポイントの計算
        int bonusInsight = insightPointCalculation.GetInsightBonus();           // ボーナス知見ポイントの計算
        result.insight = insight;
        result.insightBonus = bonusInsight;
        result.insightTotal = result.insight + result.insightBonus;

        waveText.text = result.waveCount.ToString("F0");
        scrapText.text = result.scrap.ToString("F0");
        scrapBonusText.text = result.scrapBonus.ToString("F0");
        scrapTotalText.text = result.scrapTotal.ToString("F0");
        insightText.text = result.insight.ToString("F0");
        insightBonusText.text = result.insightBonus.ToString("F0");
        insightTotalText.text = result.insightTotal.ToString("F0");

        // 敵を倒した数の設定
        EnemyKillCountController.Instance.ResultSetKillCountUI();
    }

    void ReturnButtonOnClick()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        playerSO.SceneName = SceneName.HomeScene;
        SceneManager.LoadScene("LoadScene");
    }

    [System.Serializable]
    public class ResultData
    {
        public int waveCount;
        public int scrap;
        public int scrapBonus;
        public int scrapTotal;
        public int insight;
        public int insightBonus;
        public int insightTotal;
        public KillEnemy[] killEnemies;

        [System.Serializable]
        public class KillEnemy
        {
            public EnemySO enemySO;
            public int killCount;
        }
    }
}
