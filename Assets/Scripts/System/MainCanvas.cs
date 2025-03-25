using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] PlayerStatusSO statusSO;
    [Header("メインキャンバス")]
    [SerializeField] CanvasGroup canvasGroup;
    float alphaSpeed = 0.3f;
    [Header("爆弾所持数")]
    [SerializeField] Transform bombHaveParent;
    [SerializeField] Image[] bombHaveImage;

    [Header("スキルの表示")]
    [SerializeField] Transform slillParent;
    [SerializeField] Image[] skillImage;

    [Header("スクラップの所持数")]
    [SerializeField] TextMeshProUGUI scrapHaveAmountText;
    [SerializeField] Animator anim;

    void Awake()
    {
        bombHaveImage = bombHaveParent.GetComponentsInChildren<Image>();
        skillImage = slillParent.GetComponentsInChildren<Image>();
    }


    void Start()
    {
        scrapHaveAmountText.text = $"{statusSO.ScrapHaveAmount}";       // スクラップの所持数
    }


    /// <summary>
    /// 爆弾のイラストを更新
    /// </summary>
    /// <param name="currentAmo">現在の爆弾の数</param>
    public void BombHaveUpdate(int currentAmo)
    {
        for(int ii = 0; ii < bombHaveImage.Length; ii++)
        {
            bombHaveImage[ii].enabled = ii < currentAmo ? true : false;
        }
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
        statusSO.ScrapHaveAmount = 1;
        scrapHaveAmountText.text = $"{statusSO.ScrapHaveAmount}";       // スクラップの所持数
        anim.SetTrigger("CountUp");
    }



}