using UnityEngine;
using System;
using System.Collections;


public class AnimationFlowController : MonoBehaviour
{
   public Animator animator; // Inspectorからアサイン
    public string animationToPlay; // Inspectorから設定するアニメーション名
    public event Action OnAnimationFinished; // アニメーション終了時に発行するイベント

    private bool isPlaying = false;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animatorコンポーネントがアサインされていません！");
            enabled = false;
        }
    }

    public void PlayAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(animationToPlay) && !isPlaying)
        {
            animator.Play(animationToPlay);
            isPlaying = true;
            StartCoroutine(CheckAnimationState());
        }
    }

    IEnumerator CheckAnimationState()
    {
        while (isPlaying)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // 現在再生中のアニメーションが指定したアニメーションでないか、再生が終了したら
            if (!animator.IsInTransition(0) && stateInfo.IsName(animationToPlay) && stateInfo.normalizedTime >= 1)
            {
                isPlaying = false;
                OnAnimationFinished?.Invoke(); // アニメーション終了イベントを発行
                yield break;
            }
            yield return null;
        }
    }

    // 外部からアニメーションが再生中かどうかを確認できるようにする (必要に応じて)
    public bool IsAnimationPlaying()
    {
        return isPlaying;
    }
}
