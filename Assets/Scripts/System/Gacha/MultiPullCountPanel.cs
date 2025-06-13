using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiPullCountPanel : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerSO;
    GachaSystemController gsCtrl;
    [SerializeField] GachaPanelUIController gUICtrl;
    [SerializeField] GameObject multiPullCountObj;
    [SerializeField] TMP_InputField multiCountInputField;
    [SerializeField] Button pullCountDecideButton;
    [SerializeField] Button multiBackButton;

    [Header("Range")]
    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    [SerializeField] Button maxButton;
    [SerializeField] Button minButton;

    void Awake()
    {
        gsCtrl = GetComponent<GachaSystemController>();
        pullCountDecideButton.onClick.AddListener(DisideButtonOnClick);
        multiBackButton.onClick.AddListener(MultiBackOnClick);

        plusButton.onClick.AddListener(PlusButtonOnClick);
        minusButton.onClick.AddListener(MinusButtonOnClick);
        maxButton.onClick.AddListener(MaxButtonOnClick);
        minButton.onClick.AddListener(MinButtonOnClick);

        multiPullCountObj.SetActive(false);
    }

    /// <summary>
    /// 一括パネルを非表示にする
    /// </summary>
    public void MultiBackOnClick()
    {
        multiPullCountObj.SetActive(false);
    }

    /// <summary>
    /// 一括ボタンが押された時のPanelの開閉
    /// </summary>
    public void OnMultiPullCountPanel(int pullCount)
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        multiCountInputField.text = pullCount.ToString();

        multiPullCountObj.SetActive(true);
    }

    /// <summary>
    /// 引き回数を決定するボタンを押下したときの処理
    /// </summary>
    void DisideButtonOnClick()
    {
        // 入力された文字列をint型に変換
        int count = GetCountInputFieldToInt();
        gsCtrl.MultiPullOnClick(count);
        multiPullCountObj.SetActive(false);
    }

    /// <summary>
    /// InputFieldで設定した値が所持リソースを超えていないか確認する
    /// Inspectorで設定
    /// </summary>
    public void InputCountOnDeselect()
    {
        // 回数が上限を超えていたら
        if (!gUICtrl.CanAffordMultiCount(gUICtrl.SinglePullCount * int.Parse(multiCountInputField.text)))
        {
            int maxCount = playerSO.InsightPointHaveAmount / gUICtrl.SinglePullCount;
            multiCountInputField.text = maxCount.ToString();
        }
    }

    /// <summary>
    /// InputFieldの文字列からint型に変換
    /// </summary>
    /// <returns></returns>
    int GetCountInputFieldToInt()
    {
        return int.Parse(multiCountInputField.text);
    }

    /// <summary>
    /// InputFieldに反映
    /// </summary>
    void SetCountInputField(int count)
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        multiCountInputField.text = count.ToString();
    }

    /// <summary>
    /// プラスボタンを押したときの処理
    /// </summary>
    void PlusButtonOnClick()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        int count = GetCountInputFieldToInt();
        if (count >= playerSO.InsightPointHaveAmount / gUICtrl.SinglePullCount) return;
        count++;
        SetCountInputField(count);
    }

    /// <summary>
    /// マイナスボタンを押したときの処理
    /// </summary>
    void MinusButtonOnClick()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        int count = GetCountInputFieldToInt();
        if (count <= 10) return;
        count--;
        SetCountInputField(count);
    }

    /// <summary>
    /// 最大ボタンを押したときの処理
    /// 今引ける最大値を設定する
    /// </summary>
    void MaxButtonOnClick()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        int count = playerSO.InsightPointHaveAmount / gUICtrl.SinglePullCount;
        SetCountInputField(count);
    }

    /// <summary>
    /// 最小ボタンを押したときの処理
    /// 一括で引ける最小値を設定する
    /// </summary>
    void MinButtonOnClick()
    {
        SoundManager.Instance.PlaySE(SoundDefine.SE.BTN_Click);
        int count = 10;
        SetCountInputField(count);
    }
}