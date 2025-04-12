using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin perlin;
    void Start()
    {
        perlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    /// <summary>
    /// カメラ振動
    /// </summary>
    /// <param name="time">振動する時間</param>
    /// <param name="strength">振動する力加減</param>
    public void Shake(float time, float strength)
    {
        StartCoroutine(PlayShake(time, strength));
    }

    IEnumerator PlayShake(float time, float strength)
    {
        float _time = time;
        while(_time > 0)
        {
            SetGain(strength);
            _time -= Time.deltaTime;
            yield return null;
        }

        SetGain(0);
        yield break;
    }

    void SetGain(float strength)
    {
        perlin.AmplitudeGain = strength * 1.5f;
        perlin.FrequencyGain = strength;
    }
}
