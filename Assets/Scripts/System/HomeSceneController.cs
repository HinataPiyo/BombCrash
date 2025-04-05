using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeSceneController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO player;
    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] float fadespeed;
    [SerializeField] ChangePanel[] changePanel;
    [SerializeField] PlayableDirector homeDirector;
    const int playNumber = 0;
    const int backNumber = 2;

    [System.Serializable]
    struct ChangePanel
    {
        public Button button;
        public GameObject panel;
    }

    void Start()
    {
        fadePanel.alpha = 1;
        StartCoroutine(FadeOut());
        for (int ii = 0; ii < changePanel.Length; ii++)
        {
            int count = ii;
            changePanel[ii].button.onClick.AddListener(() => ChangePanelButtonClick(count));
        }
    }


    /// <summary>
    /// パネルを切り替える
    /// </summary>
    /// <param name="num">ボタンの番号</param>
    void ChangePanelButtonClick(int num)
    {
        for (int ii = 0; ii < changePanel.Length; ii++)
        {
            bool isActive = false;
            if (ii == num) isActive = true;
            if (num == playNumber) StartCoroutine(GoGameScene());
            else if (num == backNumber) homeDirector.Play();

            if(changePanel[ii].panel != null)
            {
                changePanel[ii].panel.SetActive(isActive);
            }
        }
    }

    /// <summary>
    /// ゲームシーンに遷移
    /// </summary>
    IEnumerator GoGameScene()
    {
        player.SceneName = SceneName.GameScene;
        fadePanel.blocksRaycasts = true;
        while(fadePanel.alpha < 1)
        {
            fadePanel.alpha += fadespeed * Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("LoadScene");
        yield break;
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        fadePanel.blocksRaycasts = true;
        while(fadePanel.alpha > 0)
        {
            fadePanel.alpha -= fadespeed * Time.deltaTime;
            yield return null;
        }
        
        fadePanel.blocksRaycasts = false;
        yield break;
    }
    
}
