using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public enum ResearchesGenre { Common, RapidEngineer }
public class ResearchTreeController : MonoBehaviour
{
    public static ResearchTreeController Instance;
    [SerializeField] PlayerStatusSO player;

    // CompletedResearches = CR
    List<ResearchData> common_CR = new List<ResearchData>();            // 共通研究ツリー
    List<ResearchData> rapidEngineer_CR = new List<ResearchData>();     // 連射型研究ツリー

    void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    /// <summary>
    /// 研究できるかどうかは判別する
    /// </summary>
    /// <param name="data">研究データ</param>
    /// <param name="genre">研究ジャンル</param>
    /// <returns></returns>
    public bool CanResearch(ResearchData data, ResearchesGenre genre)
    {
        return player.ArrivalWave >= data.requiredWave
            && player.ScrapHaveAmount >= data.scrapCost
            && player.WavePointHaveAmount >= data.wavePointCost
            && data.requiredResearchIds.All(id => Completed(genre).Contains(id));
    }

    List<ResearchData> Completed(ResearchesGenre genre)
    {
        switch(genre)
        {
            case ResearchesGenre.Common:
                return common_CR;
            case ResearchesGenre.RapidEngineer:
                return rapidEngineer_CR;
        }
        return null;
    }
}