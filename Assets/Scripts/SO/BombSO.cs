using Unity.VisualScripting;
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
            return _damege;
        }
    }
    public float ExplosionRadius
    {
        get
        {
            float baseDamage = Default_ExplosionRadius + b_UpDataSO.GetPlayerData(StatusName.ExplosionRadiusUp).increaseValue;
            return baseDamage + baseDamage * player.Bomb_RC.ExplosionRadiusUp;
        }    
    }
}