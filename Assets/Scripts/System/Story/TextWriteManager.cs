using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 場面に応じて効果音やBGMを再生できる様にしたい。
/// </summary>
public class TextWriteManager : MonoBehaviour
{
    public static TextWriteManager instance;
    public StoryController storyCtrl;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] CanvasGroup fadeCanvas;
    [SerializeField] PlayerStatusSO player;
    [Header("表示するストーリーSOを入れる"), SerializeField]
    ChapterStorySO chaptStory;
    [Header("表示する画像")]
    [SerializeField] Image stageBackground;
    [SerializeField] Image leftIcon;
    [SerializeField] Image rightIcon;

    [Header("テキストの設定")]
    [SerializeField] TextMeshProUGUI name_text;
    [SerializeField] TextMeshProUGUI write_text;
    [SerializeField] float writeTime = 0.05f;
    [SerializeField] GameObject downArrow;

    int CurrentStory;       // 現在のストーリーの番号
    float fadeSpeed = 1;

    bool end_TextWeite;
    bool clickKey;

    bool endFade;



    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        if(chaptStory == null) Debug.LogError("ストーリーが設定されていません。");
        downArrow.SetActive(false);     // 文章をすべて表示し終わったら表示される矢印を非アクティブにする
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        // １ページ分のテキストが表示し終わったら
        if(end_TextWeite == true)
        {
            downArrow.SetActive(true);
            
            if(Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
            {
                clickKey = true;
                end_TextWeite = false;
                downArrow.SetActive(false);
            }
        }
    }

    IEnumerator StoryUpdate()
    {
        // 最初のストーリーから始まるように初期化
        CurrentStory = 0;

        // 設定されたストーリーの最大ページ数分ループ
        for(int ii = 0; ii < chaptStory.Pages.Length; ii++)
        {
            StartCoroutine(ArticleWrite());     // テキストを一文字ずつ表示する
            yield return new WaitUntil(() => clickKey);        // 文章がすべて表示され、クリックされるまで待機
            clickKey = false;
            CurrentStory++;     // 次のページに行けるようにインクリメント
        }

        // 全てのテキストが表示し終わったら
        StartCoroutine(FadeOut());
    }

    /// <summary>
    /// ストーリーの表示が全て終わったら
    /// </summary>
    void EndStory()
    {
        player.SceneName = SceneName.HomeScene;
        SceneManager.LoadScene("LoadScene");
    }

    IEnumerator ArticleWrite()
    {
        write_text.text = "";
        Page currentPage = chaptStory.Pages[CurrentStory];
        name_text.text = ChapterStorySO.GetCharacterName[currentPage.charaName];       // しゃべっている人の名前に設定
        stageBackground.sprite = currentPage.stageBackground;

        // カメラシェイク
        cameraShake.Shake(currentPage.time, currentPage.amplitudeGain, currentPage.frequencyGain);

        for (int ii = 0; ii < currentPage.icon.Length; ii++)
        {
            leftIcon.enabled = currentPage.icon[0] != null;
            rightIcon.enabled = currentPage.icon[1] != null;
            leftIcon.sprite = currentPage.icon[0];
            rightIcon.sprite = currentPage.icon[1];

            rightIcon.color = currentPage.isHighlight ? Color.white : new Color32(130, 130, 130, 255);
            leftIcon.color = currentPage.isHighlight ? new Color32(130, 130, 130, 255) : Color.white;
        }

        yield return new WaitUntil(() => endFade);
        // 一文字ずつ表示
        foreach(var talk in currentPage.story)
        {
            write_text.text += talk;
            yield return new WaitForSeconds(writeTime);
        }

        // テキストの表示が終わったことを知らせる
        end_TextWeite = true;
        yield return null;
    }

    IEnumerator FadeIn()
    {
        fadeCanvas.alpha = 1;
        fadeCanvas.blocksRaycasts = true;
        StartCoroutine(StoryUpdate());      // ストーリーを再生
        while (fadeCanvas.alpha > 0)
        {
            fadeCanvas.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fadeCanvas.blocksRaycasts = false;
        endFade = true;
        yield break;
    }


    IEnumerator FadeOut()
    {
        fadeCanvas.blocksRaycasts = true;
        while(fadeCanvas.alpha < 1)
        {
            fadeCanvas.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        EndStory();
        yield break;
    }
}
