using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] AudioSource seSouse;
    [SerializeField] AudioSource bgmSouse;
    [SerializeField] AudioClip playerDieBgm;
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

    public void PlaySE(int soundNum)
    {
        seSouse.PlayOneShot(seClips[soundNum]);
    }

    public void StopBgm()
    {
        bgmSouse.Stop();
    }

    public void PlayerDeiBGM()
    {
        bgmSouse.clip = playerDieBgm;
        bgmSouse.Play();
    }
}
