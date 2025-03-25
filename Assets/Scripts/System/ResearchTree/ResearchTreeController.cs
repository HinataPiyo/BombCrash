using UnityEngine;

public enum ResearchesGenre { Common, RapidEngineer }
public class ResearchTreeController : MonoBehaviour
{
    [SerializeField] bool ResearchReset;
    public static ResearchTreeController Instance;
    [SerializeField] PlayerStatusSO player;

    [SerializeField] Transform common_Parent;
    ResearchNode[] common;            // 共通研究ツリー
    ResearchNode[] rapidEngineer;     // 連射型研究ツリー

    void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        common = common_Parent.GetComponentsInChildren<ResearchNode>();
        if(ResearchReset == true)
        {
            foreach(var node in common)
            {
                node.ResearchData.state = ResearchState.Locked;
            }
        }

        UnlockResearches(ResearchesGenre.Common);
    }

    /// <summary>
    /// ノードがアンロックできるか確かめる
    /// </summary>
    /// <param name="genre"></param>
    public void UnlockResearches(ResearchesGenre genre)
    {
        foreach (var node in NodeList(genre))
        {
            if (node.ResearchData.state == ResearchState.Locked     // ロックされている状態だった場合
            && node.ClearParam())             // 前提研究を終了しているか確認する
            {
                node.ResearchData.state = ResearchState.Unlocked; // 前提条件を満たしている場合、解放
                node.CanClickButton(true);      // ボタンを押せるようにする
            }
            else
            {
                node.CanClickButton(false);     // ボタンを押せないようにする
            }
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
        if(node.ResearchData.state == ResearchState.Unlocked)
        {
            Debug.Log("研究が完了しました");
            node.ResearchData.state = ResearchState.Completed; // 研究を完了
            UnlockResearches(genre);     // 新たに解放可能な研究をチェック
        }
    }

    ResearchNode[] NodeList(ResearchesGenre genre)
    {
        switch(genre)
        {
            case ResearchesGenre.Common:
                return common;
            case ResearchesGenre.RapidEngineer:
                return rapidEngineer;
        }
        return null;
    }
}