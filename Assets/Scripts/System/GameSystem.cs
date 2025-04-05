using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }
    [SerializeField] WaveManager waveManager;
    [Header("シネマシン"), SerializeField] CinemachineCamera cinemachine;
    [Header("メインキャンバス"), SerializeField] Transform mainCanvas;
    [SerializeField] PlayableDirector gameOverDirector;

    [Header("プレイヤー")]
    [SerializeField] PlayerStatusSO player;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject player_Prefab;
    GameObject currentPlayer;
    [SerializeField] GameObject diePlayer;
    CameraShake cameraShake;
    float playTime = 1f;
    bool isGameOver = false;
    bool isAllDirection;        // 全てのゲームオーバー演出が終了したかどうか
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
        currentPlayer = Instantiate(player_Prefab, spawnPoint.position, Quaternion.identity);
    }

    void Update()
    {
        // ゲームオーバー演出が全て終了したら
        if(isAllDirection)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                mainCanvas.GetComponent<MainCanvas>().GoHomeScene();
            }
        }
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    /// <param name="endObj"></param>
    public void GameOver()
    {
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
        // Time.timeScale = 0;


        // // 一定時間後にゲームオーバー処理を実行
        // float time = playTime;
        // while(time > 0f)
        // {
        //     time -= Time.unscaledDeltaTime;
        //     yield return null;
        // }

        // Time.timeScale = 1;     // ゲーム再開
        // playTime = 1f;          // ゲームオーバー処理の時間をリセット

        // UIを非表示にする
        StartCoroutine(mainCanvas.GetComponent<MainCanvas>().CanvasGroupAlpha());

        // フィールドにいる敵をすべて破棄
        foreach(var obj in waveManager.EnemyList) Destroy(obj);

        yield return new WaitWhile(() => currentPlayer != null);
        gameOverDirector.Play();
        yield return new WaitForSeconds(1f);
        Instantiate(diePlayer);                 // ゲームオーバー時のプレイヤーの画像を生成
        cameraShake.Shake(0.3f, 0.5f);          // カメラ振動
        SoundManager.Instance.PlayerDeiBGM();   // BGMを再生

        isAllDirection = true;
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
