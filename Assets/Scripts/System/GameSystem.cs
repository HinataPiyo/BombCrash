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
    float zoomSpeed = 2f;
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

        // カメラを滑らかにendObjectPosに移動
        float moveDuration = 1.5f; // 移動にかける時間
        Vector3 startPosition = cinemachine.transform.position;     // カメラの現在位置
        Vector3 targetPosition = new Vector3(endObjectPos.position.x, endObjectPos.position.y, -10);    // カメラの移動先

        StartCoroutine(mainCanvas.GetComponent<MainCanvas>().CanvasGroupAlpha());
        // カメラ移動とズーム処理を同時に実行
        yield return StartCoroutine(MoveCamera(startPosition, targetPosition, moveDuration));
        yield return StartCoroutine(ZoomCamera(3f, zoomSpeed));

        yield break;
    }

    /// <summary>
    /// カメラを移動させる
    /// </summary>
    IEnumerator MoveCamera(Vector3 startPosition, Vector3 targetPosition, float moveDuration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // 線形補間でカメラを移動
            cinemachine.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }
    }

    /// <summary>
    /// カメラをズームさせる
    /// </summary>
    IEnumerator ZoomCamera(float targetSize, float zoomSpeed)
    {
        while (cinemachine.Lens.OrthographicSize > targetSize)
        {
            cinemachine.Lens.OrthographicSize -= zoomSpeed * Time.deltaTime;
            yield return null;
        }
    }

}

/// <summary>
/// 範囲を決める
/// </summary>
public struct Range
{
    public float min, max;
}
