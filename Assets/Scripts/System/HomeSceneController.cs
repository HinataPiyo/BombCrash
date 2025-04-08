using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class HomeSceneController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] PlayerStatusSO player;
    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] float fadespeed;
    [SerializeField] ChangePanel[] changePanel;
    [SerializeField] PlayableDirector homeDirector;
    [Header("マウスクリックエフェクト")]
    [SerializeField] GameObject mouseClick_Prefab;
    const int playNumber = 0;
    bool fadeEnd;
    public bool FadeEnd { get { return fadeEnd; } }
    public void PlayHomeDirector() { homeDirector.Play(); }

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
            changePanel[ii].button.onClick.AddListener(() => ChangePanelProc(count));
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(mouseClick_Prefab, mousePos, Quaternion.identity);
        }
    }

    void ChangePanelProc(int num)
    {
        StartCoroutine(ChangePanelButtonClick(num));
    }
    /// <summary>
    /// パネルを切り替える
    /// </summary>
    /// <param name="num">ボタンの番号</param>
    IEnumerator ChangePanelButtonClick(int num)
    {
        StartCoroutine(PanelChangeFade());
        yield return new WaitUntil(() => fadeEnd);

        HashSet<GameObject> processedPanels = new HashSet<GameObject>(); // 処理済みのパネルを記録

        for (int ii = 0; ii < changePanel.Length; ii++)
        {
            if (ii == num)
            {
                changePanel[ii].panel.SetActive(true);
                processedPanels.Add(changePanel[ii].panel);
            }
            else
            {
                if (processedPanels.Contains(changePanel[ii].panel)) continue;
                changePanel[ii].panel.SetActive(false);
                continue;
            }

            if (num == playNumber)
            {
                StartCoroutine(GoGameScene());
                yield break;
            }

            Debug.Log("Panel" + changePanel[ii].panel);
        }

        StartCoroutine(FadeOut());
        yield break;
    }

    /// <summary>
    /// ゲームシーンに遷移
    /// </summary>
    IEnumerator GoGameScene()
    {
        player.SceneName = SceneName.GameScene;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LoadScene");
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        fadeEnd = false;
        fadePanel.blocksRaycasts = true;
        while(fadePanel.alpha > 0)
        {
            fadePanel.alpha -= fadespeed * Time.deltaTime;
            yield return null;
        }
        
        fadePanel.blocksRaycasts = false;
        yield break;
    }

    IEnumerator PanelChangeFade()
    {
        
        fadePanel.blocksRaycasts = true;
        while(fadePanel.alpha < 1)
        {
            fadePanel.alpha += fadespeed * Time.deltaTime;
            yield return null;
        }

        fadeEnd = true;
        yield break;
    }
    
    public void StartFadeOut() { StartCoroutine(FadeOut()); }
    public void StartPanelChangeFade() { StartCoroutine(PanelChangeFade()); }
}
