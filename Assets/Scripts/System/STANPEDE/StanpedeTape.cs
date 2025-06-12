using UnityEngine;

public class StanpedeTape : MonoBehaviour
{
    [SerializeField] Transform[] rightTape;
    [SerializeField] Transform[] leftTape;
    [SerializeField] float moveSpeed = 0.5f;
    static readonly float endline = 12f;

    void Update()
    {
        for (int r = 0; r < rightTape.Length; r++)
        {
            if (rightTape[r].localPosition.y < -endline)
            {
                rightTape[r].localPosition = new Vector3(0, endline, 0);
            }

            rightTape[r].localPosition += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
        }
        
        for (int l = 0; l < leftTape.Length; l++)
        {
            if (leftTape[l].localPosition.y > endline)
            {
                leftTape[l].localPosition = new Vector3(0, -endline, 0);
            }

            leftTape[l].localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }
}