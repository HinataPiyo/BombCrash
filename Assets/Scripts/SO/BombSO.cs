using UnityEngine;

[CreateAssetMenu(fileName = "BombSO", menuName = "SO/BombSO")]
public class BombSO : ScriptableObject
{
    [SerializeField] PlayerStatusSO player;
    public static readonly float Default_Damage = 6.5f;
    public static readonly float Default_ExplosionRadius = 1f;

    public float AttackDamage 
    { 
        get
        {
            float _damege =  Default_Damage + player.CheckAttachmentStatusName(StatusName.BombAttackDamageUp);
            if(DebugManager.Instance != null) DebugManager.Instance.DamageText = _damege;

            float critical = IsCritical();
            if(critical > 0) _damege *= critical;
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
            return Default_ExplosionRadius + player.CheckAttachmentStatusName(StatusName.ExplosionRadiusUp);
        }    
    }
}