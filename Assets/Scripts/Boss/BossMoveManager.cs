using UnityEngine;
using DG.Tweening;

public class BossMoveManager : MonoBehaviour
{
    [Header("移動をまとめたオブジェクト")] public Transform pathContainer; // 軌跡のポイントをまとめたGameObject
    [Header("移動にかかる時間")] public float duration = 5f;      // 移動にかかる時間
    [Header("繰り返し設定")] public LoopType loopType = LoopType.Restart; // 繰り返し設定
    [Header("-1にするとループする")] public int loopCount = -1; // -1で無限ループ

    void Start()
    {
        if (pathContainer == null)
        {
            Debug.LogError("Path Container is not assigned.");
            enabled = false;
            return;
        }

        Transform[] pathPoints = new Transform[pathContainer.childCount];
        for (int i = 0; i < pathContainer.childCount; i++)
        {
            pathPoints[i] = pathContainer.GetChild(i);
        }

        Vector3[] waypoints = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            waypoints[i] = pathPoints[i].position;
        }

        // ここで初期位置を最初のウェイポイントに合わせる
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0];
        }

        transform.DOPath(waypoints, duration, PathType.CatmullRom)
            .SetLoops(loopCount, loopType)
            .SetEase(Ease.Linear);
    }
}

