using UnityEngine;
using DG.Tweening;

public class HomeSelectButtonAnimation : MonoBehaviour
{
    Vector3 startPos = new Vector3(5, 0, 0);
    float moveDuration = 1f;
    float fadeDuration = 1f;

    [SerializeField] GameObject[] buttons;
    [SerializeField] CanvasGroup canvasGroup;

    void Start()
    {
        for (int ii = 0; ii < buttons.Length; ii++)
        {
            buttons[ii].transform.position = new Vector3(buttons[ii].transform.position.x, startPos.y + ii * -5, 0);

            // 最初に速く、だんだんと遅くなる Ease.OutQuad を適用
            buttons[ii].transform.DOMove(buttons[ii].transform.parent.position, moveDuration)
                 .SetEase(Ease.OutQuad);
        }

        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, fadeDuration)
                   .SetEase(Ease.Linear);
    }
}
