using UnityEngine;
using System;
using System.Collections;


public class AnimationFlowController : MonoBehaviour
{
    /*
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
    */
    public WaveManager waveManager;
    public Animator animator; // Inspectorからアサイン
    public string animationToPlay; // Inspectorから設定するアニメーション名
    public int repeatCount = 0; // 繰り返し再生回数（0で無限ループ）
    public event Action OnAnimationFinished; // 1回の再生終了時に発行するイベント
    public event Action OnRepeatFinished; // 全ての繰り返しが終了した時に発行するイベント

    private bool isPlaying = false;
    private int currentRepeat = 0;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animatorコンポーネントがアタッチされていません！");
            enabled = false;
        }
    }

    public void PlayAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(animationToPlay) && !isPlaying)
        {
            currentRepeat = 0;
            isPlaying = true;
            StartCoroutine(RepeatAnimation());
        }
    }

    IEnumerator RepeatAnimation()
    {
        while (isPlaying && (repeatCount <= 0 || currentRepeat < repeatCount))
        {
            animator.Play(animationToPlay);

            while (isPlaying)
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (!animator.IsInTransition(0) && stateInfo.IsName(animationToPlay) && stateInfo.normalizedTime >= 1)
                {
                    OnAnimationFinished?.Invoke(); // 1回のアニメーション終了イベントを発行
                    break; // 1回の再生終了
                }
                yield return null;
            }

            currentRepeat++;

            // 無限ループでない場合、繰り返し回数に達したらループを抜ける
            if (repeatCount > 0 && currentRepeat >= repeatCount)
            {
                isPlaying = false;
                OnRepeatFinished?.Invoke(); // 全ての繰り返し終了イベントを発行
            }
            else if (isPlaying)
            {
                // 次の繰り返しまでの短い待ち時間（必要に応じて）
                yield return null;
            }
        }
    }

    public void StopAnimation()
    {
        isPlaying = false;
        currentRepeat = 0;
        animator.StopPlayback(); // アニメーションの再生を停止
    }

    public bool IsAnimationPlaying()
    {
        return isPlaying;
    }
    //OnRepeatFinished 
}
