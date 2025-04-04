using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }
    [Header("シネマシン"), SerializeField] CinemachineCamera cinemachine;
    [Header("メインキャンバス"), SerializeField] Transform mainCanvas;
    CameraShake cameraShake;
    float playTime = 1f;
    bool isGameOver = false;
    Transform endObjectPos;

    public bool IsGameOver { get { return isGameOver; } }
    public Transform MainCanvas { get { return mainCanvas; } }
    public CameraShake CameraShake { get { return cameraShake; } }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cameraShake = cinemachine.GetComponent<CameraShake>();
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    /// <param name="endObj"></param>
    public void GameOver(GameObject endObj)
    {
        endObjectPos = endObj.transform;        // 爆発した敵の位置情報を取得する
        SoundManager.Instance.StopBgm();        // BGMを停止させる
        isGameOver = true;                      // GameOverになったことを知らせる
        StartCoroutine(GameOverFlow());         // コルーチンの再生
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    IEnumerator GameOverFlow()
    {
        Debug.Log("GameOver");
        Time.timeScale = 0;


        // 一定時間後にゲームオーバー処理を実行
        float time = playTime;
        while(time > 0f)
        {
            time -= Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;     // ゲーム再開
        playTime = 1f;          // ゲームオーバー処理の時間をリセット

        StartCoroutine(mainCanvas.GetComponent<MainCanvas>().CanvasGroupAlpha());

        yield break;
    }

}

/// <summary>
/// 範囲を決める
/// </summary>
public struct Range
{
    public float min, max;
}
