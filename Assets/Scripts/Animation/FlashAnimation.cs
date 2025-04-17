using UnityEngine;

public class FlashAnimation : MonoBehaviour
{
    public WaveManager waveManager;
    public ScreenFlash screenFlash;
    //public Animator anim;
   // カウントがこの値になったら関数を呼び出す
    public int callFunctionThreshold = 2;

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
                callFunctionThreshold = int.MaxValue;
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
        screenFlash.Flash();
    }
}
