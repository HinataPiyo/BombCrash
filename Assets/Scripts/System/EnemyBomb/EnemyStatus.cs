using TMPro;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("敵のステータス情報")]
    [SerializeField] EnemySO enemySO;
    [SerializeField] float currentHp;

    [Header("UI")]
    [SerializeField] TextMeshPro hpText;
    [SerializeField] TextMeshPro countDownText;

    [Header("コンポーネント")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator anim;

    [Header("ダメージ表示")]
    [SerializeField] GameObject applyDamageText_Prefab;
    Transform applyDamageCanvas;

    public EnemySO EnemySO => enemySO;

    void Start()
    {
        // UIをapplyDamageCanvasの子として生成
        applyDamageCanvas = GameObject.Find("ApplyDamageCanvas").transform;
        sprite.sprite = enemySO.EnemyIcon;
    }

    /// <summary>
    /// 先に生成されていた敵は後ろ、後から生成された敵は前に描画されるように
    /// </summary>
    /// <param name="oderinlayer"></param>
    public void SetOrderInLayer(int oderinlayer)
    {
        anim.SetTrigger("Spawn");       // スポーン時のアニメーションを再生
        sprite.sortingOrder = oderinlayer - 1;
        hpText.sortingOrder = oderinlayer;
        countDownText.sortingOrder = oderinlayer;
    }

    /// <summary>
    /// ステータス上処理
    /// </summary>
    /// <param name="inc"></param>
    public void SetHpUP(float inc)
    {
        currentHp = enemySO.UpMaxHp + inc;
        hpText.text = "Hp " + currentHp.ToString("F1");
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (GameSystem.Instance.IsGameOver == true) return;      // ゲームオーバーになっていた場合ダメージ処理を行わない
        CreateDamageText(damage);
        currentHp -= damage;        // ダメージ処理
        hpText.text = "Hp " + currentHp.ToString("F1");
        if (currentHp <= 0)
        {
            GetComponent<DropScrap>().SpawnScrap();     // 死んだらスクラップをドロップ
            Instantiate(enemySO.Explosion_Prefab, transform.position, Quaternion.identity);
            EnemyKillCountController.Instance.AddEnemyCount(enemySO);       // キルカウントを増やす
            Destroy(gameObject);        // 自身を破棄
        }
    }

    /// <summary>
    /// ダメージテキストを生成
    /// </summary>
    /// <param name="damage"></param>
    void CreateDamageText(float damage)
    {
        GameObject obj = Instantiate(applyDamageText_Prefab, applyDamageCanvas);
        obj.GetComponent<ApplyDamageText>()?.SetApplyDamageText(transform.position, damage);
    }
    
}