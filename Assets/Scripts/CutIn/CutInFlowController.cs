using UnityEngine;
using UnityEngine.Playables;

public class CutInFlowController : MonoBehaviour
{
    [SerializeField] PlayableDirector cutinDirector;

    /// <summary>
    /// カットインの再生
    /// </summary>
    public void StartCutin()
    {
        cutinDirector?.Play();
    }

    public bool CutInDirectorState()
    {
        // 再生中じゃなかったらtrue
        return cutinDirector.state != PlayState.Playing;
    }
}