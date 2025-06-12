using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO player;
    [SerializeField] LoadSceneTips loadSceneTips;
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI tipsText;
    public Slider progressBar;

    [Header("Fade設定")]
    [SerializeField] CanvasGroup fadegroup;
    float fadeSpeed = 0.75f;
    bool fadeFlag;

    void Start()
    {
        StartCoroutine(FadeIn());
        LoadSceneSetting();
        StartCoroutine(LoadAsync());
    }

    void LoadSceneSetting()
    {
        int r_background = Random.Range(0, loadSceneTips.Background.Length);
        background.sprite = loadSceneTips.Background[r_background];

        if(loadSceneTips.Tips[0] != null)
        {
            tipsText.text = loadSceneTips.Tips[0];
        }
    }

    IEnumerator LoadAsync()
    {
        // このフレームで UI の更新を確実に反映
        yield return null;

        AsyncOperation async = SceneManager.LoadSceneAsync(SystemDefine.NextSceneName(player.nextScene));
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(3f);

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            progressBar.value = progress;

            // 読み込み完了（progress = 0.9f）したらシーンを切り替える
            if (async.progress >= 0.9f)
            {
                // 少し待機してから切り替える場合（演出のため）
                yield return new WaitForSeconds(2f);

                StartCoroutine(FadeOut());
                yield return new WaitUntil(() => !fadeFlag);

                async.allowSceneActivation = true;
            }

            yield return null;
        }

        yield break;
    }

    IEnumerator FadeIn()
    {
        fadeFlag = true;
        while(fadegroup.alpha > 0)
        {
            fadegroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fadeFlag = false;
        yield break;
    }

    IEnumerator FadeOut()
    {
        fadeFlag = true;
        while(fadegroup.alpha < 1)
        {
            fadegroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fadeFlag = false;
        yield break;
    }
}
