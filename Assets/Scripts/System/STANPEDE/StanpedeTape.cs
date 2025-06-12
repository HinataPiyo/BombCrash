using UnityEngine;

public class StanpedeTape : MonoBehaviour
{
    [SerializeField] Transform[] rightTape;
    [SerializeField] Transform[] leftTape;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] Animator anim;
    static readonly float Endline = 12f;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Play()
    {
        gameObject.SetActive(true);
        anim.SetTrigger("Play");
    }

    public void End() => anim.SetTrigger("End");
    public void AnimationIsActive() => gameObject.SetActive(true);

    void Update()
    {
        MoveTapes(rightTape, -moveSpeed, -Endline, Endline);
        MoveTapes(leftTape, moveSpeed, Endline, -Endline);
    }

    void MoveTapes(Transform[] tapes, float speed, float border, float reset)
    {
        if (tapes == null) return;
        for (int i = 0; i < tapes.Length; i++)
        {
            if ((speed > 0 && tapes[i].localPosition.y > border) ||
                (speed < 0 && tapes[i].localPosition.y < border))
            {
                tapes[i].localPosition = new Vector3(0, reset, 0);
            }
            tapes[i].localPosition += new Vector3(0, speed * Time.deltaTime, 0);
        }
    }
}