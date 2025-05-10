using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyBombController : MonoBehaviour 
{
    EnemySO enemySO;
    [SerializeField] TextMeshPro countDownText;
    float countdownTime;

    float delayTime;
    public float DelayTime { set { delayTime = value; } }
    public float CountDownTime { get { return countdownTime; } }
    private void Start()
    {
        enemySO = GetComponent<EnemyStatus>().EnemySO;
        countdownTime = enemySO.CountDown;
        countDownText.text = countdownTime.ToString("F0");
        StartCoroutine(CountDown());    
    }

    /// <summary>
    /// カウントダウン処理
    /// </summary>
    IEnumerator CountDown()
    {
        while (countdownTime > 0)
        {
            if(GameSystem.Instance.IsGameOver) yield break;

            countDownText.text = countdownTime.ToString("F0");
            CheckCountColor();
            float time = 1f;
            while(time > 0)
            {
                yield return new WaitUntil(() => Delay());
                time -= Time.deltaTime;
            }

            countdownTime--;
        }
        
        BOOM();
    }

    /// <summary>
    /// 爆発処理
    /// </summary>
    void BOOM()
    {
        countDownText.text = "0";
        Instantiate(enemySO.Countzero_Prefab, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySE(1);
        GameSystem.Instance.CameraShake.Shake(0.5f, 1f);
        GameSystem.Instance.GameOver();
        Debug.Log("敵ボムが爆発 【GameOver】");
    }

    void CheckCountColor()
    {
        float safeLine = enemySO.CountDown;
        float dangerLine = enemySO.CountDown / 2;
        float criticalLine = enemySO.CountDown / 3;

        if (countdownTime <= criticalLine)
        {
            countDownText.color = Color.red;
        }
        else if (countdownTime <= dangerLine)
        {
            countDownText.color = Color.yellow;
        }
        else if (countdownTime <= safeLine)
        {
            countDownText.color = Color.green;
        }
    }

    bool Delay()
    {
        if(delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            return false;
        }

        return true;
    }
}