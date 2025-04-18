using TMPro;
using UnityEngine;

public enum ResearchesGenre { Support, Bomb }
public class ResearchTreeController : MonoBehaviour
{
    public static ResearchTreeController Instance;
    [SerializeField] bool CompletedResearchReset;
    [SerializeField] bool ResearchReset;
    [SerializeField] NodePanel nodePanel;
    [SerializeField] PlayerStatusSO player;

    [SerializeField] Transform support_Parent;
    [SerializeField] Transform bomb_Parent;
    ResearchNode[] support;            // 共通研究ツリー
    ResearchNode[] bomb;     // 連射型研究ツリー

    [Header("UI")]
    [SerializeField] TextMeshProUGUI insightAmount;
    [SerializeField] TextMeshProUGUI scrapAmount;

    public NodePanel NodePanel { get { return nodePanel; } }

    void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        support = support_Parent.GetComponentsInChildren<ResearchNode>();
        bomb = bomb_Parent.GetComponentsInChildren<ResearchNode>();
        ResearchNode[][] nodes = { support, bomb };


        if(ResearchReset == true)
        {
            foreach(var n in nodes)
            {
                foreach(var node in n)
                {
                    if(!CompletedResearchReset && node.state == ResearchState.Completed) continue;
                    node.state = ResearchState.Locked;
                }
            }
        }

        UnlockResearches(ResearchesGenre.Support);
        UnlockResearches(ResearchesGenre.Bomb);
    }

    void Update()
    {
        UpdateHasAmount();      // 所持数を更新
    }

    /// <summary>
    /// ノードがアンロックできるか確かめる
    /// </summary>
    /// <param name="genre"></param>
    public void UnlockResearches(ResearchesGenre genre)
    {
        var nodes = NodeList(genre);

        foreach (var node in nodes)
        {
            if (node.state == ResearchState.Locked     // ロックされている状態だった場合
            && node.ClearPrerequisites())             // 前提研究を終了しているか確認する
            {
                // 前提条件を満たしている場合、解放
                node.state = ResearchState.Unlocked;
            }
            node.CanClickButton();
        }
    }

    /// <summary>
    /// 研究するタイミングで処理
    /// </summary>
    /// <param name="node">ノード自身</param>
    /// <param name="genre">ノードのジャンル</param>
    public void CompleteResearch(ResearchNode node, ResearchesGenre genre)
    {
        // 解放はされている状態だった場合
        if(node.state == ResearchState.Unlocked)
        {
            Debug.Log("研究が完了しました");
            node.state = ResearchState.Completed; // 研究を完了
            node.AddPlayerStatus();      // プレイヤーのステータスに反映
            UnlockResearches(genre);     // 新たに解放可能な研究をチェック
        }
    }

    public void UpdateHasAmount()
    {
        insightAmount.text = player.InsightPointHaveAmount.ToString("F0");
        scrapAmount.text = player.ScrapHaveAmount.ToString("F0");
    }


    ResearchNode[] NodeList(ResearchesGenre genre)
    {
        switch(genre)
        {
            case ResearchesGenre.Support:
                return support;
            case ResearchesGenre.Bomb:
                return bomb;
        }
        return null;
    }
}