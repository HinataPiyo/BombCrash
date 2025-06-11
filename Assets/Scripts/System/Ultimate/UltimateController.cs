using UnityEngine;

public class UltimateController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerSO;
    UltimateUIController uuiCtrl;

    float coolTime;
    bool isCoolTime;        // true : CT中, false : CT終了
    public bool CanUlt { get; private set; }            // 必殺技が使えるか使えないか
    public Vector2 bombExplosionPoint;

    private void Awake()
    {
        if (playerSO.UltimateSO == null) Debug.LogError("必殺技が設定されていません");

        uuiCtrl = GetComponent<UltimateUIController>();
        coolTime = playerSO.UltimateSO.CoolTime; // テスト。本来は0
        CanUlt = true;
        isCoolTime = false;

        uuiCtrl.UpdateUIColor(isCoolTime);
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
        if (CanUlt && Input.GetKeyDown(KeyCode.V))
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
        CanUlt = false;
        coolTime = 0f;

        uuiCtrl.UpdateUIColor(isCoolTime);
    }

    /// <summary>
    /// クールタイムの更新
    /// </summary>
    private void UpdateTimer()
    {
        if (CanUlt) return;
        coolTime += Time.deltaTime;
        uuiCtrl.UpdateSlider(coolTime);

        if (coolTime >= playerSO.UltimateSO.CoolTime)
        {
            isCoolTime = false;
            CanUlt = true;
            coolTime = 0f;

            uuiCtrl.UpdateUIColor(isCoolTime);
        }
    }
}