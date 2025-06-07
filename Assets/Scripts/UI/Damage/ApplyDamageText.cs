using TMPro;
using UnityEngine;

public class ApplyDamageText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;

    /// <summary>
    /// 攻撃力をテキストに反映する処理
    /// </summary>
    /// <param name="damage"></param>
    public void SetApplyDamageText(Vector2 pos, float damage)
    {
        damageText.text = damage.ToString("F1");
        transform.position = pos;
    }

    /// <summary>
    /// アニメーション終了時自身を破棄する
    /// </summary>
    public void EndAnimationDestroy()
    {
        Destroy(gameObject);
    }

}
