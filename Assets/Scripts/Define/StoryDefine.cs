using System.Collections.Generic;

public class StoryDefine
{
    public static Dictionary<CharacterGenre, string> GetCharacterName = new ()
    {
        {CharacterGenre.MainCharacter, "主人公"},
        {CharacterGenre.LittleMainCharacter, "主人公(幼少期)"},
        {CharacterGenre.Father, "父"},
        {CharacterGenre.Mother, "母"},
        {CharacterGenre.Enemy, "カウントボム"},
    };
}