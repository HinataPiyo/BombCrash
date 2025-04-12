using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneMainCanvas : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] PlayerStatusSO player;
    [Header("メインキャンバス")]
    [SerializeField] CanvasGroup canvasGroup;
    float alphaSpeed = 0.3f;
    [Header("爆弾所持数")]
    [SerializeField] GameObject usedBomb_Prefab;
    [SerializeField] Transform bombHaveParent;
    [SerializeField] Image[] bombHaveImage;
    private int previousAmo = 0; // 前回の爆弾の数を記録

    [Header("スキルの表示")]
    [SerializeField] Transform slillParent;
    [SerializeField] Image[] skillImage;

    [Header("スクラップの所持数")]
    [SerializeField] TextMeshProUGUI scrapHaveAmountText;
    [SerializeField] Animator anim;

    [Header("フェード")]
    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] float fadespeed;

    void Awake()
    {
        bombHaveImage = bombHaveParent.GetComponentsInChildren<Image>();
        skillImage = slillParent.GetComponentsInChildren<Image>();
    }


    void Start()
    {
        StartCoroutine(FadeOut());
        scrapHaveAmountText.text = $"{player.ScrapHaveAmount}";       // スクラップの所持数
    }


    /// <summary>
    /// 爆弾のイラストを更新
    /// </summary>
    /// <param name="currentAmo">現在の爆弾の数</param>
    public void BombHaveUpdate(int currentAmo)
    {
        for (int ii = 0; ii < bombHaveImage.Length; ii++)
        {
            bombHaveImage[ii].enabled = ii < currentAmo;

            // 爆弾の数が減少した場合のみInstantiateを実行
            if (ii >= currentAmo && ii < previousAmo)
            {
                Instantiate(usedBomb_Prefab, bombHaveImage[ii].transform.position, Quaternion.identity);
            }
        }

        // 現在の爆弾の数を記録
        previousAmo = currentAmo;
    }
    
    /// <summary>
    /// キャンバスの透明度を調整する
    /// </summary>
    public IEnumerator CanvasGroupAlpha()
    {
        while(canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha -= alphaSpeed;
            yield return null;
        }

        yield break;
    }

    /// <summary>
    /// スクラップを取得する処理
    /// </summary>
    public void ScrapCountUpAnimation()
    {
        player.ScrapHaveAmount = 1;
        scrapHaveAmountText.text = $"{player.ScrapHaveAmount}";       // スクラップの所持数
        anim.SetTrigger("CountUp");
    }

    IEnumerator FadeOut()
    {
        while(fadePanel.alpha > 0)
        {
            fadePanel.alpha -= fadespeed * Time.deltaTime;
            yield return null;
        }
        fadePanel.blocksRaycasts = false;

        yield break;
    }

    IEnumerator FadeInGoHomeScene()
    {
        fadePanel.blocksRaycasts = true;
        player.SceneName = SceneName.HomeScene;
        
        while(fadePanel.alpha < 1)
        {
            fadePanel.alpha += fadespeed * Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("LoadScene");
    }

    public void GoHomeScene() { StartCoroutine(FadeInGoHomeScene()); }
}