using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float waitTime;
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
