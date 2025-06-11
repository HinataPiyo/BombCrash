using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class AttachmentPanelChange : MonoBehaviour
{
    public static AttachmentPanelChange Instance;
    [SerializeField] HomeSceneController homeSceneCont;
    [SerializeField] PlayableDirector director;
    [SerializeField] PlayableAsset statusToAttachment;
    [SerializeField] PlayableAsset attachmentToStatus;

    [SerializeField] Button[] changePanel;
    [Header("非アクティブにしたいパネル"), SerializeField] GameObject[] isActivePanel;
    [Header("アクティブにしたいパネル"), SerializeField] GameObject isActiveHomePanel;

    public PlayableDirector Director => director;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        director.playableAsset = null;      // 最初に再生されるのを防ぐ
        for(int ii = 0; ii < changePanel.Length; ii++)
        {
            switch(ii)
            {
                case 0:     // ステータス画面から強化画面
                    changePanel[ii].onClick.AddListener(StatusToAttachment);
                    break;
                case 1:     // 強化画面からステータス画面
                    changePanel[ii].onClick.AddListener(AttachmentToStatus);
                    break;
            }
        }
    }

    void StartBackHome()
    {
        SoundManager.Instance.PlaySE(1);
        StartCoroutine(BackHome());
    }

    IEnumerator BackHome()
    {
        // フェードイン処理
        homeSceneCont.StartPanelChangeFade();
        yield return new WaitUntil(() => homeSceneCont.FadeInEnd);
        foreach(var panel in isActivePanel)
        {
            panel.SetActive(false);
        }

        isActiveHomePanel.SetActive(true);
        homeSceneCont.PlayHomeDirector();

        // フェードアウト処理
        homeSceneCont.StartFadeOut();
        yield break;
    }

    void StatusToAttachment()
    {
        // Directorが再生中だった場合、処理を行わない
        if(director.state == PlayState.Playing) return;
        director.playableAsset = statusToAttachment;
        director.RebindPlayableGraphOutputs();
        director.Play();
    }

    void AttachmentToStatus()
    {
        // Directorが再生中だった場合、処理を行わない
        if(director.state == PlayState.Playing) return;
        director.playableAsset = attachmentToStatus;
        director.RebindPlayableGraphOutputs();
        director.Play();
    }
}
