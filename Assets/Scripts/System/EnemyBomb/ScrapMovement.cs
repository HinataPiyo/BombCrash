using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ScrapMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    GameSceneMainCanvas mainCanvas;
    Vector2 haveScrapTextPos = new Vector2(3, -2.5f);
    bool isMove;
    bool isArrival;
    void Start()
    {
        mainCanvas = GameSystem.Instance.MainCanvas.GetComponent<GameSceneMainCanvas>();
        StartCoroutine(MoveFlow());
    }

    void Update()
    {
        if(isMove == true)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                haveScrapTextPos,
                moveSpeed * Time.deltaTime
            );

            if(Vector2.Distance(transform.position, haveScrapTextPos) <= 0)
            {
                isArrival = true;
                isMove = false; // 移動を停止
            }
        }

        if (isArrival == true)
        {
            mainCanvas.ScrapCountUpAnimation();
            Destroy(gameObject);
            isArrival = false; // 処理を1回だけ実行するようにする
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    IEnumerator MoveFlow()
    {
        yield return new WaitForSeconds(1f);
        isMove = true;
        yield break;
    }
}