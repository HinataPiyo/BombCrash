using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;
    [SerializeField] TextMeshProUGUI waveTime;
    [SerializeField] TextMeshProUGUI bombCount;
    [SerializeField] TextMeshProUGUI spawnProbability;
    [SerializeField] TextMeshProUGUI createBombTime;
    public float WaveTime { set { waveTime.text = "WaveTime : " + value.ToString("F2"); } }
    public int BombCount { set { bombCount.text = $"BombCount : {value}"; } }
    public Range SpawnProbability { set { spawnProbability.text = $"SpawnProbability : " + value.min.ToString("F3") + "," + value.max.ToString("F3"); } }
    public float CreateBombTime { set { createBombTime.text = $"CreateBombTime : {value}"; } }
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        WaveTime = 0;
        BombCount = 0;
        SpawnProbability = new Range { min = 0, max = 0 };
        CreateBombTime = 0;
    }
}
