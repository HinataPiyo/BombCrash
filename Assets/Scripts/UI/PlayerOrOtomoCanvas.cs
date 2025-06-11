using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOrOtomoCanvas : MonoBehaviour
{
    [SerializeField] HomeSceneController homeSceneCont;
    [SerializeField] ChangePanel[] changePanel;

    [System.Serializable]
    struct ChangePanel
    {
        public Button button;
        public GameObject panel;
        public bool isPlayHomeDirector;
    }

    bool isFadeIn;

    void Start()
    {
        for (int ii = 0; ii < changePanel.Length; ii++)
        {
            int count = ii;
            changePanel[ii].button?.onClick.AddListener(() => StartChangePanel(count));
        }
    }

    /// <summary>
    /// パネル切り替え処理を開始
    /// </summary>
    /// <param name="num">ボタンの番号</param>
    void StartChangePanel(int num)
    {
        if (isFadeIn) return;
        SoundManager.Instance.PlaySE(1);
        StartCoroutine(ChangePanelProc(num));   // 画面切り替え（パネル）
    }

    /// <summary>
    /// パネルを切り替える
    /// </summary>
    /// <param name="panels">パネル配列</param>
    /// <param name="num">ボタンの番号</param>
    /// <param name="playDirector">ホームディレクターを再生するかどうか</param>
    IEnumerator ChangePanelProc(int num)
    {
        isFadeIn = true;
        // フェードイン処理
        homeSceneCont.StartPanelChangeFade();
        yield return new WaitUntil(() => homeSceneCont.FadeEnd);

        // パネルの切り替え
        HashSet<GameObject> processedPanels = new HashSet<GameObject>();
        for (int ii = 0; ii < changePanel.Length; ii++)
        {
            if (ii == num)
            {
                changePanel[ii].panel.SetActive(true);
                processedPanels.Add(changePanel[ii].panel);

                // ホームディレクターを再生
                if (changePanel[ii].isPlayHomeDirector)
                {
                    homeSceneCont.PlayHomeDirector();
                }
            }
            else
            {
                if (!processedPanels.Contains(changePanel[ii].panel))
                {
                    changePanel[ii].panel.SetActive(false);
                    processedPanels.Add(changePanel[ii].panel);
                    Debug.Log("SSSSSSSSSSSSSSS");
                }
            }
            
        }

        // フェードアウト処理
        homeSceneCont.StartFadeOut();
        isFadeIn = false;
        yield break;
    }
}
