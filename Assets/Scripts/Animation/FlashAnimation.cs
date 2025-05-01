using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class FlashAnimation : MonoBehaviour
{
    /*
    public WaveManager waveManager;
    //public ScreenFlash screenFlash;
    public Animator anim;
    public AnimationClip animationClip;
    //public Animator anim;
   // カウントがこの値になったら関数を呼び出す
    public int callFunctionThreshold = 2;

    void Start()
    {
        anim = GetComponent<Animator>();   
    }
    void Update()
    {
        // waveManagerがアサインされているか確認
        if (waveManager != null)
        {
            // waveManagerのcountがcallFunctionThreshold以上になったら関数を呼び出す
            if (waveManager.WaveCount >= callFunctionThreshold)
            {
                MyFunction();
                // 一度呼び出したら、これ以上呼ばないように制御する場合
                //callFunctionThreshold = int.MaxValue;
                callFunctionThreshold++;
            }
        }
        else
        {
            Debug.LogError("waveManagerがアサインされていません！");
        }
    }

    void MyFunction()
    {
        Debug.Log("カウントが " + callFunctionThreshold + " に達したので、MyFunction() が呼び出されました！");
        // ここに呼び出したい処理を書く
        //screenFlash.Flash();
        animationClip;
    }
    */
    /*
    public WaveManager waveManager; // Inspectorからアサインする
    public int previousCount = -1; // 前回のカウント数を保存する変数
    public Animator animator;

    void Update()
    {
        // counterScriptがアサインされているか確認
        if (waveManager != null)
        {
            // CounterScriptのcountが前回と異なる場合にMyFunction()を呼び出す
            if (waveManager.WaveCount > previousCount)
            {
                MyFunction();
                previousCount = waveManager.WaveCount; // 今回のカウント数を保存
            }
        }
        else
        {
            Debug.LogError("CounterScriptがアサインされていません！");
        }
    }

    void MyFunction()
    {
        Debug.Log("カウントが " + waveManager.WaveCount + " になりました！MyFunction() が呼び出されました！");
        // ここに呼び出したい処理を書く
        animator.SetBool("guyarhkjnml", true);
    }
    */
    public WaveManager WaveManager; // Inspectorからアサイン
    public ScreenFlash screenFlash;
    public Animator animator; // Inspectorからアサイン
    //public string animationParameterName = "Count"; // Animatorのパラメータ名（例: "Count"）
    public int previousCount = -1;
    float endlessWaveRuleTime = 0;
    public int Count = 2;
    public GameObject ScreenFlashPoint;
    public GameObject screenFlashImage;
    public EndlessWaveRule endlessWaveRule;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animatorコンポーネントがアサインされていません！");
        }
    }

    void Update()
    {
        if (WaveManager != null && animator != null)
        {
            if (WaveManager.WaveCount < previousCount)
            {
                UpdateAnimator();
                previousCount = WaveManager.WaveCount;
                //previousCount++;
            }
        }
    }

    void UpdateAnimator()
    {
        // Animatorのfloat型のパラメータを更新する例
        //animator.SetFloat(animationParameterName, (float)WaveManager.WaveCount);

        // 他のパラメータも必要に応じて更新できる
        // 例：bool型のパラメータを更新
        // animator.SetBool("IsCounting", true);
        //animator.SetBool("IsCutintypeA",false);
        //if(endlessWaveRule)
        //{
            Instantiate(screenFlashImage,ScreenFlashPoint.transform.position,transform.rotation);
        //}
        Debug.Log("aaaaaaaaaaa");
        if(WaveManager.WaveCount >= Count)
        {
            Debug.Log("ssssss");
            animator.SetBool("IsCutintypeA",true);
            Count += 10;
            screenFlash.Flash();
        }
        

        // 例：trigger型のパラメータをトリガー
        // if (counterScript.count % 5 == 0) // カウントが5の倍数の時
        // {
        //     animator.SetTrigger("OnFiveCount");
        // }

        //Debug.Log("Animatorのパラメータ '" + animationParameterName + "' を " + WaveManager.WaveCount + " に更新しました！");
    }
}
