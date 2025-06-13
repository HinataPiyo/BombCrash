using UnityEngine;

public class SoundManager : MonoBehaviour
{

    

    public static SoundManager Instance { get; private set; }
    [SerializeField] AudioSource seSouse;
    [SerializeField] AudioSource bgmSouse;
    [SerializeField] AudioClip playerDieBgm;
    [SerializeField] SoundDefine.SEStatus[] seClips;

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

    public void PlaySE(SoundDefine.SE type)
    {
        seSouse.PlayOneShot(SoundDefine.GetSE(seClips, type));
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
