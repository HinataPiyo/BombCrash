using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class OtomoPanelChange : MonoBehaviour
{
    [SerializeField] HomeSceneController homeSceneCont;
    [SerializeField] PlayableDirector director;
    [SerializeField] PlayableAsset statusToUpgrade;
    [SerializeField] PlayableAsset upgradeToStatus;

    [SerializeField] Button[] changePanel;
    [Header("Homeに戻るボタン"), SerializeField] Button backHomeButton;
    [Header("非アクティブにしたいパネル"), SerializeField] GameObject isActivePanel;
    [Header("アクティブにしたいパネル"), SerializeField] GameObject isActiveHomePanel;

    void Start()
    {
        director.playableAsset = null;      // 最初に再生されるのを防ぐ
        backHomeButton.onClick.AddListener(StartBackHome);
        for(int ii = 0; ii < changePanel.Length; ii++)
        {
            switch(ii)
            {
                case 0:     // ステータス画面から強化画面
                    changePanel[ii].onClick.AddListener(StatusToUpgrade);
                    break;
                case 1:     // 強化画面からステータス画面
                    changePanel[ii].onClick.AddListener(UpgradeToStatus);
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
        yield return new WaitUntil(() => homeSceneCont.FadeEnd);
        isActivePanel.SetActive(false);

        isActiveHomePanel.SetActive(true);
        homeSceneCont.PlayHomeDirector();

        // フェードアウト処理
        homeSceneCont.StartFadeOut();
        yield break;
    }

    void StatusToUpgrade()
    {
        // Directorが再生中だった場合、処理を行わない
        if(director.state == PlayState.Playing) return;
        director.playableAsset = statusToUpgrade;
        director.RebindPlayableGraphOutputs();
        director.Play();
    }

    void UpgradeToStatus()
    {
        // Directorが再生中だった場合、処理を行わない
        if(director.state == PlayState.Playing) return;
        director.playableAsset = upgradeToStatus;
        director.RebindPlayableGraphOutputs();
        director.Play();
    }
}
