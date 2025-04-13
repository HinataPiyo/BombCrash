using UnityEngine;

[CreateAssetMenu(fileName = "BombSO", menuName = "SO/BombSO")]
public class BombSO : ScriptableObject
{
    [SerializeField] PlayerStatusSO player;
    [SerializeField] BasicUpgradeData b_UpDataSO;
    public const float Default_Damage = 10f;
    public const float Default_ExplosionRadius = 1f;

    public float AttackDamage 
    { 
        get
        { 
            float baseDamage = Default_Damage + b_UpDataSO.GetPlayerData(StatusName.BombAttackDamageUp).increaseValue;
            float _damege =  baseDamage + baseDamage * player.Bomb_RC.AttackDamageUp;
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
            float baseDamage = Default_ExplosionRadius * (1 + b_UpDataSO.GetPlayerData(StatusName.ExplosionRadiusUp).increaseValue);
            return baseDamage + baseDamage * player.Bomb_RC.ExplosionRadiusUp;
        }    
    }
}