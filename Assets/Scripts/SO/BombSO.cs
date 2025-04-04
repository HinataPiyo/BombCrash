using UnityEngine;

[CreateAssetMenu(fileName = "BombSO", menuName = "SO/BombSO")]
public class BombSO : ScriptableObject
{
    [SerializeField] PlayerStatusSO player;
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;

    public float Damage { get { return damage + damage * player.Bomb_RC.AttackDamageUp; } }
    public float ExplosionRadius { get { return explosionRadius + explosionRadius * player.Bomb_RC.ExplosionRadiusUp; } }
}