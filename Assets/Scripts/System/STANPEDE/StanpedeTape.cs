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
        // 非アクティブ状態に初期化
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Animationを外部から再生する処理
    /// </summary>
    public void Play()
    {
        gameObject.SetActive(true);
        anim.SetTrigger("Play");
    }

    /// <summary>
    /// スタンピードが終了したときの処理
    /// </summary>
    public void End() => anim.SetTrigger("End");

    /// <summary>
    /// Animation側で呼ぶ処理（Endが再生し終わると処理される）
    /// </summary>
    public void AnimationIsActive() => gameObject.SetActive(true);

    /// <summary>
    /// STANPEDEのテープ画像の動きを処理している
    /// </summary>
    void Update()
    {
        MoveTapes(rightTape, -moveSpeed, -Endline, Endline);
        MoveTapes(leftTape, moveSpeed, Endline, -Endline);
    }

    /// <summary>
    /// 共通処理をまとめている
    /// </summary>
    /// <param name="tapes">右か左か</param>
    /// <param name="speed">速さだが移動方向</param>
    /// <param name="border">テープが初期位置に移動するライン</param>
    /// <param name="reset">初期位置</param>
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