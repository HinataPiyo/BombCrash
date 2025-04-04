using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodePanel : MonoBehaviour
{
    DragNode dragNode;
    [SerializeField] ResearchNode currentNode;
    [SerializeField] CanvasGroup canvasGroup;
    [Header("説明"), SerializeField] TextMeshProUGUI explanation;
    [Header("数テキスト")]
    [SerializeField] TextMeshProUGUI wavePointAmount;
    [SerializeField] TextMeshProUGUI scrapAmount;
    [SerializeField] TextMeshProUGUI requiredWaveAmount;
    [Header("研究ボタン"), SerializeField] Button researchButton;
    TextMeshProUGUI researchText;
    Image buttonImage;
    Color32 gray = new Color32(130, 130, 130, 255);

    [Header("支援 / 爆弾")]
    [SerializeField] ChangePanel[] changePanel;
    [System.Serializable]
    struct ChangePanel
    {
        public Button button;
        public GameObject panel;
    }

    void Start()
    {
        dragNode = GetComponent<DragNode>();
        researchText = researchButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = researchButton.GetComponent<Image>();
        researchButton.onClick.AddListener(() => ResearchButtonClick());        // リスナー登録

        // パネル切り替えボタンのリスナー登録
        for(int ii = 0; ii < changePanel.Length; ii++)
        {
            int count = ii;
            changePanel[ii].button.onClick.AddListener(() => ChangePanelButtonClick(count));
        }

        ChangePanelButtonClick(0);
        canvasGroup.alpha = 0f;
        explanation.text = "";
    }


    /// <summary>
    /// ノードをクリックされた時に呼び出される
    /// </summary>
    /// <param name="node"></param>
    public void SetResearchNode(ResearchNode node)
    {
        currentNode = node;
        ResearchData d = node.ResearchData;

        if(node.ResearchData.state == ResearchState.Locked)
        {
            UpdateUI(d);
            buttonImage.gameObject.SetActive(false);
        }
        else if(node.ResearchData.state == ResearchState.Unlocked)
        {
            UpdateUI(d);
            // 研究するボタンを押せるか否か
            researchButton.interactable = node.PlayerHasExceededTheLimit();
            buttonImage.color = researchButton.interactable ? Color.white : gray;
            researchText.text = "研究";
            buttonImage.gameObject.SetActive(true);
        }
        else if(node.ResearchData.state == ResearchState.Completed)
        {
            UpdateUI(d);
            researchText.text = "完了";
            buttonImage.color = Color.white;
            researchButton.interactable = false;
            buttonImage.gameObject.SetActive(true);
        }

        
        canvasGroup.alpha = 1f;
    }

    void UpdateUI(ResearchData d)
    {
        explanation.text = d.explanation;
        wavePointAmount.text = d.WavePointCost.ToString();
        scrapAmount.text = d.ScrapCost.ToString();
        requiredWaveAmount.text = d.RequiredWave.ToString();
    }

    /// <summary>
    /// 研究ボタンをクリックしたときの処理
    /// </summary>
    void ResearchButtonClick()
    {
        researchText.text = "完了";
        researchButton.interactable = false;
        ResearchTreeController r = ResearchTreeController.Instance;
        r.CompleteResearch(currentNode, currentNode.ResearchData.genre);
    }

    /// <summary>
    /// ノードパネルを切り替える
    /// </summary>
    /// <param name="num">ボタンの番号</param>
    void ChangePanelButtonClick(int num)
    {
        for(int ii = 0; ii < changePanel.Length; ii++)
        {
            bool isActive = false;
            if(ii == num) isActive = true;
            changePanel[ii].panel.SetActive(isActive);
        }

        canvasGroup.alpha = 0f;
        explanation.text = "";
        dragNode.PosisionReset();

        ResearchTreeController.Instance.UnlockResearches(ResearchesGenre.Support);
        ResearchTreeController.Instance.UnlockResearches(ResearchesGenre.Bomb);
    }
}
