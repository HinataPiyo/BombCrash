using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "BombSO", menuName = "SO/BombSO")]
public class BombSO : ScriptableObject
{
    [SerializeField] PlayerStatusSO player;
    [Header("基本値")]
    [SerializeField] float attackDamage = 6.5f;
    [SerializeField] float explosionRadius = 1f;
    [Header("係数")]    // デフォ爆弾は1
    [SerializeField] float attackDamage_Coefficient = 1f;
    [SerializeField] float explosionRadius_Coefficient = 1f;

    public float AttackDamage
    {
        get
        {
            float _damege = (attackDamage * attackDamage_Coefficient) + player.CheckAttachmentStatusName(StatusName.BombAttackDamageUp);
            if (DebugManager.Instance != null) DebugManager.Instance.DamageText = _damege;

            float critical = IsCritical();
            if (critical > 0) _damege *= critical;
            return _damege;
        }
    }


    /// <summary>
    /// クリティカルの抽選
    /// </summary>
    /// <returns></returns>
    float IsCritical()
    {
        float r_val = Random.Range(0f, 1f);
        if(r_val < player.CriticalChance)
        {
            Debug.Log("クリティカルが発生しました");
            return player.CriticalDamage;
        }
        return 0;
    }

    /// <summary>
    /// 爆発範囲の設定
    /// </summary>
    public float ExplosionRadius
    {
        get
        {
            return (explosionRadius * explosionRadius_Coefficient) + player.CheckAttachmentStatusName(StatusName.ExplosionRadiusUp);
        }    
    }
}