using System.Collections;
using UnityEngine;

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
