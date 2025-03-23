using UnityEngine;

[CreateAssetMenu(fileName = "BombSO", menuName = "SO/BombSO")]
public class BombSO : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;

    public float Damage => damage;
    public float ExplosionRadius => explosionRadius;
}