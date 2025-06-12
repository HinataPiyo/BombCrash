using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ドロップダウンのStatusNameGenreのSlot自身を管理するクラス
/// </summary>
public class DropdownGenreSelectButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI genreText;
    [SerializeField] Button button;
    StatusName statusName;

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="_statusName"></param>
    public void SetInit(StatusName _statusName, DropdownGenreSelectButtonController ctrl)
    {
        button.onClick.AddListener(() => ctrl.SetSelectGenre(statusName));
        statusName = _statusName;
        genreText.text = SystemDefine.StatusNameToName(_statusName);
    }
}