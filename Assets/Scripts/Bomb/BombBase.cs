using System.Collections;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    // test
    [SerializeField] GameObject explosionPrefab;

    [SerializeField] protected BombSO bombSO;
    [SerializeField] protected float scaleUpSpeed = 1f;
    [SerializeField] protected float maxScale = 1f;
    [SerializeField] protected float defaultScale = 0.5f;

    const float throwForce = 4f;
    Vector3 explosionPoint;
    public Vector3 ExplosionPoint { set { explosionPoint = value; } }

    void Start()
    {
        StartCoroutine(Explode());
    }

    void Update()
    {
        Move();     // 移動処理
    }

    /// <summary>
    /// 移動処理(爆弾が爆発点に向かう)
    /// </summary>
    void Move()
    {
        Vector3 dis = explosionPoint - transform.position;
        transform.position += dis * throwForce * Time.deltaTime;
    }

    /// <summary>
    /// 爆発処理(爆弾生成時に呼び出す)
    /// </summary>
    IEnumerator Explode()
    {
        // 拡大
        while (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(1, 1, 0) * scaleUpSpeed * Time.deltaTime;
            yield return null;
        }

        // 縮小
        yield return new WaitForSeconds(0.1f);

        while (transform.localScale.x > defaultScale)
        {
            transform.localScale -= new Vector3(1, 1, 0) * (scaleUpSpeed + 0.01f) * Time.deltaTime;
            yield return null;
        }

        // 爆発処理
        BOOM();
        
        Destroy(gameObject);
    }

    /// <summary>
    /// 爆発処理
    /// </summary>
    void BOOM()
    {
        SoundManager.Instance.PlaySE(0);             // サウンド再生
        GameSystem.Instance.CameraShake.Shake(0.1f, 0.2f);      // カメラ振動
        
        // 爆発アニメーションをここで（一旦赤い円を表示している）
        Transform explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity).transform;
        Transform[] exps = explosion.GetComponentsInChildren<Transform>();
        foreach(var exp in exps)
        {
            Vector3 radius = Vector3.one * bombSO.ExplosionRadius;
            exp.localScale = radius * 0.8f;
        }

        // 爆発範囲内の敵にダメージを与える
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, bombSO.ExplosionRadius);
        foreach (var hit in hits)
        {
            // 敵にダメージを与える
            hit.GetComponent<EnemyStatus>()?.TakeDamage(bombSO.AttackDamage);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bombSO.ExplosionRadius);
    }
}
