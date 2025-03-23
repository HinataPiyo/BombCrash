using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] AudioSource seSouse;
    [SerializeField] AudioSource bgmSouse;
    [SerializeField] AudioClip[] seClips;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    public void PlaySE(SE sound)
    {
        seSouse.PlayOneShot(seClips[(int)sound]);
    }

    public void StopBgm()
    {
        bgmSouse.Stop();
    }
}

public enum SE
{
    // ThrowBomb,
    Explosion,
    GameOver,
    GameClear,
    CountDown
}
