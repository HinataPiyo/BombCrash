using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UltimateUIController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerSO;

    [Header("UI")]
    [SerializeField] new TextMeshProUGUI name;
    [SerializeField] Slider ultSlider;
    [SerializeField] Image icon;

    void Start()
    {
        if (playerSO.UltimateSO == null) return;
        name.text = playerSO.UltimateSO.Name;
        ultSlider.maxValue = playerSO.UltimateSO.CoolTime;
        ultSlider.value = playerSO.UltimateSO.CoolTime;
        icon.sprite = playerSO.UltimateSO.Icon;
    }

    public void UpdateSlider(float time)
    {
        ultSlider.value = time;
    }
}