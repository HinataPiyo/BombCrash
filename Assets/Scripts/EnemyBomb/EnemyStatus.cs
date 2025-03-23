using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private float currentHp;

    public EnemySO EnemySO { get { return enemySO; } }

    void Start()
    {
        currentHp = enemySO.MaxHp;
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    public void TakeDamage(float damage)
    {
        if(GameSystem.Instance.IsGameOver == true) return;      // ゲームオーバーになっていた場合ダメージ処理を行わない
        currentHp -= damage;        // ダメージ処理
        if (currentHp <= 0)
        {
            GetComponent<DropScrap>().SpawnScrap();     // 死んだらスクラップをドロップ
            Destroy(gameObject);        // 自信を破棄
        }
    }
    
}