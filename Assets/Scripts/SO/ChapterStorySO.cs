using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "SO/ChapterStorySO")]
public class ChapterStorySO : ScriptableObject
{
    // ストーリーをまとめる配列
    [SerializeField] Page[] stories;

    public Page[] Pages { get { return stories; } }
    
    public static Dictionary<CharacterGenre, string> GetCharacterName = new ()
    {
        {CharacterGenre.MainCharacter, "主人公"},
        {CharacterGenre.LittleMainCharacter, "主人公(幼少期)"},
        {CharacterGenre.Father, "父"},
        {CharacterGenre.Mother, "母"},
        {CharacterGenre.Enemy, "カウントボム"},
    };
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
    [Header("ハイライトするImage")]
    [Tooltip("true : 右, false : 左")] public bool isHighlight = false;
    public Sprite[] icon = new Sprite[2];

    [TextArea(3, 5), Header("文章")]
    public string story;

    [Header("SE")]
    public AudioClip seClip;

    [Header("カメラシェイク")]
    public float time = 0;
    [Tooltip("振幅")] public float amplitudeGain = 0;
    [Tooltip("周波数")] public float frequencyGain = 0;
}


public enum CharacterGenre
{
    MainCharacter,
    LittleMainCharacter,
    Father,
    Mother,
    Enemy,
}