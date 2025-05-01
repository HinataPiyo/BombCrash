using TMPro;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] EnemySO enemySO;
    [SerializeField] float currentHp;
    [SerializeField] TextMeshPro hpText;
    [SerializeField] TextMeshPro countDownText;
    [SerializeField] SpriteRenderer sprite;

    public EnemySO EnemySO => enemySO;
    

    public void SetOrderInLayer(int oderinlayer)
    {
        sprite.sortingOrder = oderinlayer;
        hpText.sortingOrder = oderinlayer + 1;
        countDownText.sortingOrder = oderinlayer + 1;
    }

    public void SetHpUP(float inc)
    {
        currentHp = enemySO.UpMaxHp + inc;
        hpText.text = "Hp " + currentHp.ToString("F2");

        //Debug.LogFormat($"<color=green>係数 : {inc}</color>");
        //Debug.LogFormat($"<color=blue>敵のHP : {currentHp}</color>");
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    public void TakeDamage(float damage)
    {
        if(GameSystem.Instance.IsGameOver == true) return;      // ゲームオーバーになっていた場合ダメージ処理を行わない
        Debug.LogFormat($"<color=red>プレイヤーATK : {damage}</color>");
        currentHp -= damage;        // ダメージ処理
        hpText.text = "Hp " + currentHp.ToString("F2");
        if (currentHp <= 0)
        {
            GetComponent<DropScrap>().SpawnScrap();     // 死んだらスクラップをドロップ
            Instantiate(enemySO.Explosion_Prefab, transform.position, Quaternion.identity);
            EnemyKillCountController.Instance.AddEnemyCount(enemySO);       // キルカウントを増やす
            Destroy(gameObject);        // 自身を破棄
        }
    }
    
}