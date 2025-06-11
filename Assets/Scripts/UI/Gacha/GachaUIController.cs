using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ガチャ画面のUIを管理するクラス
/// </summary>
public class GachaUIController : MonoBehaviour
{
    [System.Serializable]
    public class ArrowStat
    {
        public Button arrow;
        public bool isReverse;      // 矢が反転しているか否か
    }

    [SerializeField] Animator anim;
    [SerializeField] ArrowStat[] arrowStat;

    void Awake()
    {
        // 矢の初期化
        for (int ii = 0; ii < arrowStat.Length; ii++)
        {
            int index = ii;
            arrowStat[ii].arrow.onClick.AddListener(() => ArrowOnClick(index));
            arrowStat[ii].isReverse = false;
        }
    }

    /// <summary>
    /// 矢のボタンが押された時の処理
    /// </summary>
    /// <param name="changeIndex">矢の順番</param>
    void ArrowOnClick(int changeIndex)
    {
        arrowStat[changeIndex].isReverse = !arrowStat[changeIndex].isReverse;       // 現在の反転状態を切り替える

        // もし矢が反転状態だったら
        if (arrowStat[changeIndex].isReverse)
        {
            arrowStat[changeIndex].arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
            anim.SetInteger("ChangeIndex", changeIndex);        // アニメーション再生
        }
        else
        {
            arrowStat[changeIndex].arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetInteger("ChangeIndex", changeIndex + 1);        // アニメーション再生
        }
    }

    
}