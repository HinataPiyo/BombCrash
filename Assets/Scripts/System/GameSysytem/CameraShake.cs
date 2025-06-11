using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineBasicMultiChannelPerlin))]
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
    public void Shake(float time, float amplitudeGain, float frequencyGain)
    {
        StartCoroutine(PlayShake(time, amplitudeGain, frequencyGain));
    }

    IEnumerator PlayShake(float time, float amplitudeGain, float frequencyGain)
    {
        float _time = time;
        while (_time > 0)
        {
            SetGain(amplitudeGain, frequencyGain);
            _time -= Time.deltaTime;
            yield return null;
        }

        SetGain(0, 0);
        yield break;
    }

    void SetGain(float amplitudeGain, float frequencyGain)
    {
        perlin.AmplitudeGain = amplitudeGain;
        perlin.FrequencyGain = frequencyGain;
    }
}
