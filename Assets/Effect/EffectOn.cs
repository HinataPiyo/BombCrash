using UnityEngine;

public class EffectOn : MonoBehaviour
{
    [SerializeField]public GameObject effect;
    [SerializeField]public GameObject effectPoint;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(effect,effectPoint.transform.position,effectPoint.transform.rotation);
        }
    } 
}
