public class SystemDefine
{
    // ステータスごとに対応するStatusNameを用意
    public static StatusName[] bombStatusNames = new StatusName[]
    {
        StatusName.BombAttackDamageUp,
        StatusName.CriticalDamageUp,
        StatusName.CriticalChanceUp,
        StatusName.BombStockAmountUp,
        StatusName.ExplosionRadiusUp,
        StatusName.BombCreateSpeedUp,
    };

    public static StatusName[] supportStatusNames = new StatusName[]
    {
        StatusName.DropScrapUp,
        StatusName.GetInsightPointUp,
    };

    public static Rarity[] rarities = new Rarity[]
    {
        Rarity.N,
        Rarity.R,
        Rarity.SR,
        Rarity.SSR,
        Rarity.UR,
    };

    /// <summary>
    /// レアリティから文字列に変換
    /// </summary>
    public static string RarityToName(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.N:
                return "N";
            case Rarity.R:
                return "R";
            case Rarity.SR:
                return "SR";
            case Rarity.SSR:
                return "SSR";
            case Rarity.UR:
                return "UR";
        }

        return "";
    }

    /// <summary>
    /// StatusNameから日本語に変換した文字列を返す
    /// </summary>
    public static string StatusNameToName(StatusName statusName)
    {
        switch (statusName)
        {
            case StatusName.BombAttackDamageUp:
                return "冷却ダメージ";
            case StatusName.BombCreateSpeedUp:
                return "爆弾生成速度";
            case StatusName.BombStockAmountUp:
                return "最大爆弾所持数";
            case StatusName.CriticalDamageUp:
                return "クリティカルダメージ";
            case StatusName.CriticalChanceUp:
                return "クリティカル率";
            case StatusName.ExplosionRadiusUp:
                return "爆発範囲";
        }

        return "";
    }

    /// <summary>
    /// シーン移動の際にenumを文字列に変換する
    /// </summary>
    public static string NextSceneName(SceneName nextScene)
    {
        switch (nextScene)
        {
            case SceneName.GameScene:
                return "GameScene";
            case SceneName.HomeScene:
                return "HomeScene";
        }

        return null;
    }

    public static string GetConvertColorText(ConvertColor color, string text)
    {
        switch (color)
        {
            case ConvertColor.Red:
                return $"<color=#CD2E3C>{text}</color>";
            case ConvertColor.Blue:
                return $"<color=#2E46CD>{text}</color>";
            default:
                return $"<color=FFFFFF>{text}</color>";
        }
    }
}

public enum ConvertColor { Red, Blue }
public enum Rarity { NON = -1, N, R, SR, SSR, UR }

public enum SceneName
{
    GameScene,
    HomeScene,
}

public enum StatusName
{
    BombAttackDamageUp,
    CriticalDamageUp,
    CriticalChanceUp,
    BombStockAmountUp,
    BombCreateSpeedUp,
    ExplosionRadiusUp,
    ThrowAmountUp,

    DropScrapUp,
    GetInsightPointUp,
}

// カテゴリ
public enum Category
{
    Attack,
    Auxiliary,
    Disruption,
}
