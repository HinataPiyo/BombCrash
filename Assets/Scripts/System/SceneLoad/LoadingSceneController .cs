using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO player;
    public Slider progressBar;

    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        // このフレームで UI の更新を確実に反映
        yield return null;

        AsyncOperation async = SceneManager.LoadSceneAsync(player.NextSceneName());
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            progressBar.value = progress;

            // 読み込み完了（progress = 0.9f）したらシーンを切り替える
            if (async.progress >= 0.9f)
            {
                // 少し待機してから切り替える場合（演出のため）
                yield return new WaitForSeconds(1f);
                async.allowSceneActivation = true;
            }

            yield return null;
        }

        yield break;
    }
}
