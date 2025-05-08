using UnityEngine;
using UnityEngine.Playables;

public class CutInFlowController : MonoBehaviour
{
    [SerializeField] PlayableDirector cutinDirector;
    public GameObject cutinEffect;
    public GameObject effectPoint;

    /// <summary>
    /// カットインの再生
    /// </summary>
    public void StartCutin()
    {
        cutinDirector?.Play();
        Invoke("EffectOn",0.25f);
    }

    public bool CutInDirectorState()
    {
        // 再生中じゃなかったらtrue
        return cutinDirector.state != PlayState.Playing;
    }
    public void EffectOn()
    {
        Instantiate(cutinEffect,effectPoint.transform.position,transform.rotation);
    }
}