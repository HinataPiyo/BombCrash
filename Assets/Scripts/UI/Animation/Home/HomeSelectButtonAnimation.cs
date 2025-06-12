using UnityEngine;
using DG.Tweening;

/// <summary>
/// ホーム画面のセレクトボタンの移動モーションを再生
/// </summary>
public class HomeSelectButtonAnimation : MonoBehaviour
{
    float moveDuration = 1f;
    float fadeDuration = 1f;

    [SerializeField] RectTransform[] buttons;
    [SerializeField] RectTransform[] parents;
    [SerializeField] CanvasGroup canvasGroup;

    void Start()
    {
        for (int ii = 0; ii < buttons.Length; ii++)
        {
            // 開始位置にセット
            buttons[ii].anchoredPosition = new Vector3(0, (ii + 1) * -45);

            // 元の位置にアニメーション
            buttons[ii].DOAnchorPos(Vector2.zero, moveDuration)
                .SetEase(Ease.OutQuad);
        }

        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, fadeDuration)
                   .SetEase(Ease.Linear);
    }
}
