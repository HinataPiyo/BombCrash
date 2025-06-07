using UnityEngine;

public abstract class UltimateSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] float coolTime;
    [SerializeField] Sprite icon;

    /// <summary>
    /// 必殺技の実行処理
    /// </summary>
    public abstract void ExecuteUltimate(Transform player, Vector2 explosionPos);

    public string Name => name;
    public float CoolTime => coolTime;
    public Sprite Icon => icon;
}