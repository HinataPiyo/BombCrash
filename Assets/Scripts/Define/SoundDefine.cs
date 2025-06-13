using UnityEngine;

public class SoundDefine
{
    [System.Serializable]
    public class SEStatus
    {
        public SE type;
        public AudioClip clip;
    }

    [System.Serializable]
    public class BGMStatus
    {
        public BGM type;
        public AudioClip clip;
    }

    public static AudioClip GetSE(SEStatus[] se, SE type)
    {
        foreach (SEStatus s in se)
        {
            if (s.type == type)
            {
                return s.clip;
            }
        }

        return null;
    }

    public enum SE
    {
        BOOM = 1,
        GameOver_BOOM = 2,
        BTN_Click = 3,
        Slot_Click = 4,
    }

    public enum BGM
    {
        HomeScene = 1,
        GameScene = 2,
        GameOver = 3,
    }
}