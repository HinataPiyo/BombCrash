using UnityEngine;
using UnityEngine.Playables;

public class CutInFlowController : MonoBehaviour
{
    [SerializeField] PlayableDirector cutinDirector;
    public enum CutinType { CutinTypeA, CutinTypeB } 
    public CutinType cutinType = CutinType.CutinTypeA;
    public GameObject cutinEffect;
    public GameObject effectPoint;

    /// <summary>
    /// カットインの再生
    /// </summary>
    public void StartCutin()
    {
        cutinDirector?.Play();
        if(cutinType == CutinType.CutinTypeA)
        {
            
        }
        else
        {
            Invoke("EffectOn",0.25f);
        }
        
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