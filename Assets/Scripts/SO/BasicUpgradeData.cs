using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicUpgradeData", menuName = "SO/BasicUpgradeData")]
public class BasicUpgradeData : ScriptableObject
{
    // Basicは基礎値の上昇を主に担ったいる(constで定義した定数は固定値で上昇する)
    const float DropScrapUp = 0.25f;        // スクラップの上昇率（25%）
    const float GetInsightPointUp = 0.1f;   // 知見ポイントの獲得基礎値の上昇（暫定10%）

    const float AttackDamageUp = 1.24f;     // 攻撃力 (124%)
    const float CriticalDamageUp = 0.05f;   // クリティカルダメージ（5%）
    const float CriticalChanceUp = 0.03f;   // クリティカル率(3%)
    const int BombStockUp = 1;        // 爆弾最大所持数(1個)
    const float BombCreateSpeedUp = 0.03f;  // 爆弾生成速度(3%)
    const float ExplosionRadiusUp = 0.10f;  // 爆発範囲(暫定10%)
    
    [SerializeField] bool LevelReset = false;
    [Header("プレイヤーベースデータ"), SerializeField] Data[] palyerDatas;
    [Header("サポートベースデータ"), SerializeField] Data[] supportDatas;
    public Data[] PlayerDatas { get { return palyerDatas; } }
    public Data[] SupportDatas { get { return supportDatas; } }

    /// <summary>
    /// レベルを初期化する
    /// </summary>
    void LevelResetProc()
    {
        if(LevelReset == false) return; 
        foreach(var data in palyerDatas)
        {
            data.level = 1;
        }

        foreach(var data in supportDatas)
        {
            data.level = 1;
        }

        LevelReset = false;
    }

    public void InitSetData()
    {
        // レベルを初期化するか確認する
        LevelResetProc();

        foreach(var data in palyerDatas)
        {
            data.increaseValue = data.UpgradeStatusValue(-1);
            data.NeedScrap();
            data.NeedInsight();
        }

        foreach(var data in supportDatas)
        {
            data.increaseValue = data.UpgradeStatusValue(-1);
            data.NeedScrap();
            data.NeedInsight();
        }
    }

    public Data GetPlayerData(StatusName name) { return palyerDatas.FirstOrDefault(data => data.statusName == name); }
    public Data GetSupportData(StatusName name) { return supportDatas.FirstOrDefault(data => data.statusName == name); }


    [System.Serializable]
    public class Data
    {
        public StatusName statusName;
        public bool isPercentage;       // %表記するか否か（trueする）
        public bool useDecimalPlaces;   // 小数点第二位まで表示するか否か
        public string name;
        public int level = 1;
        public float increaseValue;
        public void LevelUpProc()
        {
            level++;
            increaseValue = UpgradeStatusValue(-1);
            NeedScrap();
            NeedInsight();
        }

        /// <summary>
        /// 増加量を返す
        /// </summary>
        /// <param name="nowOrNext">0(次) or -1 (現在)現在のレベルか次のレベルか</param>
        public float UpgradeStatusValue(int nowOrNext)
        {
            switch(statusName)
            {
                case StatusName.BombAttackDamageUp:
                    return AttackDamageUp * (level + nowOrNext);
                case StatusName.CriticalDamageUp:
                    return CriticalDamageUp * (level + nowOrNext);
                case StatusName.CriticalChanceUp:
                    return CriticalChanceUp * (level + nowOrNext);
                case StatusName.BombStockAmountUp:
                    return BombStockUp * (level + nowOrNext);
                case StatusName.BombCreateSpeedUp:
                    return BombCreateSpeedUp * (level + nowOrNext);
                case StatusName.ExplosionRadiusUp:
                    return ExplosionRadiusUp * (level + nowOrNext);
                case StatusName.DropScrapUp:
                    return DropScrapUp * (level + nowOrNext);
                case StatusName.GetInsightPointUp:
                    return GetInsightPointUp * (level + nowOrNext);
            }

            return 0;
        }
        
        [Header("scrap")]
        public int currentScrap;
        public int baseScrap;
        public float scrapPitch;
        public int NeedScrap() { return currentScrap = (int)Mathf.Floor(baseScrap * scrapPitch * level); }
        [Header("insight")]
        public int currentInsight;
        public int baseInsight;
        public float insightPitch;
        public int NeedInsight() { return currentInsight = (int)Mathf.Floor(baseInsight * insightPitch * level); }
    }
}
