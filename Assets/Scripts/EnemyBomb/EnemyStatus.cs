using TMPro;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] EnemySO enemySO;
    [SerializeField] float currentHp;
    [SerializeField] TextMeshPro hpText;
    [SerializeField] TextMeshPro countDownText;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator anim;

    [Header("ダメージを表示する")]
    [SerializeField] GameObject applyDamageText_Prefab;
    Transform applyDamageCanvas;

    public EnemySO EnemySO => enemySO;

    void Start()
    {
        applyDamageCanvas = GameObject.Find("ApplyDamageCanvas").transform;
    }

    public void SetOrderInLayer(int oderinlayer)
    {
        anim.SetTrigger("Spawn");       // スポーン時のアニメーションを再生
        sprite.sortingOrder = oderinlayer - 1;
        hpText.sortingOrder = oderinlayer;
        countDownText.sortingOrder = oderinlayer;
    }

    public void SetHpUP(float inc)
    {
        currentHp = enemySO.UpMaxHp + inc;
        hpText.text = "Hp " + currentHp.ToString("F1");

        //Debug.LogFormat($"<color=green>係数 : {inc}</color>");
        //Debug.LogFormat($"<color=blue>敵のHP : {currentHp}</color>");
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