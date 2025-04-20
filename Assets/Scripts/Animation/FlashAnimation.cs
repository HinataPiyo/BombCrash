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
}
