using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "SO/ChapterStorySO")]
public class ChapterStorySO : ScriptableObject
{
    // ストーリーをまとめる配列
    [SerializeField] Page[] stories;

    public Page[] Pages { get{ return stories; } }
}

/// <summary>
/// セリフやキャラ、背景画像などの設定
/// </summary>
[System.Serializable]
public class Page
{
    public Sprite stageBackground;
    [Header("キャラクター名前/画像")]
    public CharacterGenre charaName;
    public Sprite[] icon;

    [TextArea(3, 5), Header("文章")]
    public string story;


    /// <summary>
    /// キャラクター名を返す
    /// </summary>
    public string CharactorName()
    {
        switch(charaName)
        {
            case CharacterGenre.MainCharacter:
                return "主人公";
            case CharacterGenre.Father:
                return "父";
            case CharacterGenre.Enemy:
                return "カウントダウンボム";
        }

        return "";
    }
}


public enum CharacterGenre
{
    MainCharacter,
    Father,
    Enemy,
}