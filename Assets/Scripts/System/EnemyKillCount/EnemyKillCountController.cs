using System.Collections.Generic;
using UnityEngine;

public class EnemyKillCountController : MonoBehaviour
{
    public static EnemyKillCountController Instance;
    [SerializeField] GameObject enemyKillCount_UIElement;
    [SerializeField] Transform enemyKillCount_Parent;
    [SerializeField] List<EnemyCount> enemyCounts = new List<EnemyCount>();

    [System.Serializable]
    public class EnemyCount
    {
        public EnemySO enemySO;
        public int killCount;
    }

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        enemyCounts.Clear();
    }

    /// <summary>
    /// 敵をリストに追加する
    /// </summary>
    public void AddEnemyCount(EnemySO enemySO)
    {
        foreach(var enemy in enemyCounts)
        {
            // 既にリストに存在していたらカウントアップ
            if(enemy.enemySO == enemySO)
            {
                enemy.killCount++;
                return;     // そのまま処理を終了する
            }
        }

        // 新規で追加する
        EnemyCount enemyCount = new EnemyCount() { enemySO = enemySO, killCount = 1 };
        enemyCounts.Add(enemyCount);
    }

    /// <summary>
    /// リザルトパネルに表示するときの処理
    /// </summary>
    public void ResultSetKillCountUI()
    {
        for(int ii = 0; ii < enemyCounts.Count; ii++)
        {
            // UIを生成
            GameObject obj = Instantiate(enemyKillCount_UIElement, enemyKillCount_Parent);
            EnemyKillCountUIElement ui = obj.GetComponent<EnemyKillCountUIElement>();

            // UIに値を設定
            ui.SetTextValue(enemyCounts[ii].enemySO, enemyCounts[ii].killCount);
        }
    }
}