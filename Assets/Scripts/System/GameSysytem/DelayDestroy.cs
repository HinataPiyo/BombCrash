using System.Collections;
using UnityEngine;

/// <summary>
/// パーティクルなどに遅延して破棄してほしい場合に使用
/// </summary>
public class DelayDestroy : MonoBehaviour
{
    [SerializeField] float waitTime;
    public float WaitTime { set { waitTime = value; } }
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
