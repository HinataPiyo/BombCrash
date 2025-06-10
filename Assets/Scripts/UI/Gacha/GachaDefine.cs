using System.Collections.Generic;

public class GachaDefine
{
    public static class GachaProbabilityTable
    {
        /// <summary>
        /// 基本確率
        /// </summary>
        public static Dictionary<Rarity, float> _baseRates = new()
        {
            { Rarity.N,   70f },
            { Rarity.R,   22f },
            { Rarity.SR,  6f },
            { Rarity.SSR, 1.5f },
            { Rarity.UR,  0.1f }
        };

        /// <summary>
        /// ガチャレベルに応じて確率を変更した値を取得
        /// </summary>
        /// <param name="level">現在のガチャレベル</param>
        public static Dictionary<Rarity, float> GetRatesByLevel(int level)
        {
            float boostRate = 0.35f; // レベル1上昇ごとにUR～SRに+0.5%
            float urBoost = boostRate * level;
            float ssrBoost = boostRate * level * 1.2f;
            float srBoost = boostRate * level * 1.5f;

            float ur = _baseRates[Rarity.UR] + urBoost;
            float ssr = _baseRates[Rarity.SSR] + ssrBoost;
            float sr = _baseRates[Rarity.SR] + srBoost;

            // 残り確率を N と R に再配分
            float totalHigh = ur + ssr + sr;
            float remaining = 100f - totalHigh;

            float nRatio = _baseRates[Rarity.N] / (_baseRates[Rarity.N] + _baseRates[Rarity.R]);
            float rRatio = _baseRates[Rarity.R] / (_baseRates[Rarity.N] + _baseRates[Rarity.R]);

            float n = remaining * nRatio;
            float r = remaining * rRatio;

            return new Dictionary<Rarity, float>
            {
                { Rarity.N,  n },
                { Rarity.R,  r },
                { Rarity.SR, sr },
                { Rarity.SSR,ssr },
                { Rarity.UR, ur }
            };
        }
    }

    public static class GachaLevelProgression
    {
        public static int _baseRequiredPulls = 100;
        public static int GetRequiredPullsForNextLevel(int level)
        {
            return _baseRequiredPulls + (level * 20);
        }
    }
}