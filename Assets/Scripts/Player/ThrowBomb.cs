using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    [SerializeField] PlayerStatusSO statusSO;
    [SerializeField] Transform bombExplosionPoint;
    [SerializeField] GameObject bombPrefab;
    MainCanvas mainCanvas;
    float explosionPointSpeed = 5f;
    float reloadProgressTime;
    Range rangeY = new Range { min = -1.5f, max = 4f };
    int currentHaveBomb;
    public int CurrentHaveBomb { get { return currentHaveBomb; } }

    void Start()
    {
        mainCanvas = FindFirstObjectByType<MainCanvas>();
        bombExplosionPoint.position = Vector2.zero;     // 着地地点を0に設定

        // 現在の爆弾の数の更新
        currentHaveBomb = statusSO.MaxHaveBomb;
        mainCanvas.BombHaveUpdate(currentHaveBomb);

        // 制作時間の初期化
        reloadProgressTime = statusSO.CreateBombTime;
    }

    void Update()
    {
        Throw();
        Reload();
        ExplosionPointPosition();       // 爆弾の着地地点
    }

    /// <summary>
    /// 爆弾を投げる処理
    /// </summary>
    void Throw()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentHaveBomb > 0)
        {
            // 爆弾をプレイヤーの位置に生成
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            bomb.GetComponent<BombBase>().ExplosionPoint = bombExplosionPoint.position;
            currentHaveBomb--;
            mainCanvas.BombHaveUpdate(currentHaveBomb);
        }
    }

    /// <summary>
    /// 爆弾を制作する
    /// </summary>
    void Reload()
    {
        if(currentHaveBomb > statusSO.MaxHaveBomb) return;
        reloadProgressTime -= Time.deltaTime;
        if(reloadProgressTime <= 0)
        {
            currentHaveBomb++;      // 爆弾制作
            mainCanvas.BombHaveUpdate(currentHaveBomb);
            reloadProgressTime = statusSO.CreateBombTime;
        }
    }

    /// <summary>
    /// 爆弾の着地地点
    /// </summary>
    void ExplosionPointPosition()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 pos = bombExplosionPoint.position;
            pos.y = Mathf.Clamp(pos.y + explosionPointSpeed * Time.deltaTime, rangeY.min, rangeY.max);
            bombExplosionPoint.position = pos;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 pos = bombExplosionPoint.position;
            pos.y = Mathf.Clamp(pos.y - explosionPointSpeed * Time.deltaTime, rangeY.min, rangeY.max);
            bombExplosionPoint.position = pos;
        }
    }

}
