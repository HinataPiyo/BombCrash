using UnityEngine;

public class UltimateController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerSO;
    UltimateUIController uuiCtrl;

    float coolTime;
    bool isCoolTime;        // true : CT中, false : CT終了
    bool canUlt;            // 必殺技が使えるか使えないか
    public bool UseUlt { get; private set; }
    public Vector2 bombExplosionPoint;

    private void Awake()
    {
        if (playerSO.UltimateSO == null) Debug.LogError("必殺技が設定されていません");

        uuiCtrl = GetComponent<UltimateUIController>();
        coolTime = playerSO.UltimateSO.CoolTime; // テスト。本来は0
        canUlt = true;
        isCoolTime = false;
        UseUlt = false;
    }

    private void Update()
    {
        UpdateTimer();
        HandleUltimateInput();
    }

    /// <summary>
    /// 必殺技の入力処理
    /// </summary>
    private void HandleUltimateInput()
    {
        if (!isCoolTime && Input.GetKeyDown(KeyCode.V))
        {
            UseUlt = !UseUlt;
        }

        if (UseUlt && canUlt && Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteUltimate();
        }
    }

    /// <summary>
    /// 必殺技の実行
    /// </summary>
    private void ExecuteUltimate()
    {
        playerSO.UltimateSO.ExecuteUltimate(GameSystem.Instance.Player.transform, bombExplosionPoint);
        isCoolTime = true;
        canUlt = false;
        UseUlt = false;
        coolTime = 0f;
    }

    /// <summary>
    /// クールタイムの更新
    /// </summary>
    private void UpdateTimer()
    {
        if (canUlt) return;
        coolTime += Time.deltaTime;
        uuiCtrl.UpdateSlider(coolTime);

        if (coolTime >= playerSO.UltimateSO.CoolTime)
        {
            isCoolTime = false;
            canUlt = true;
            coolTime = 0f;
        }
    }
}