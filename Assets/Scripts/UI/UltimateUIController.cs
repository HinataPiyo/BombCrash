using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UltimateUIController : MonoBehaviour
{
    [SerializeField] PlayerStatusSO playerSO;
    [SerializeField] Animator anim;

    [Header("UI")]
    [SerializeField] new TextMeshProUGUI name;
    [SerializeField] Slider ultSlider;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI pressKeyText;

    [SerializeField] Image sliderFill;

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

    public void UpdateUIColor(bool isCoolTime)
    {
        // クールタイム中
        if (isCoolTime)
        {
            anim.SetBool("CanUlt", false);
            sliderFill.color = new Color32(160, 150, 0, 255);
            icon.color = new Color32(200, 200, 200, 255);
            pressKeyText.enabled = false;
        }
        else    // クールタイム終了
        {
            anim.SetBool("CanUlt", true);
            sliderFill.color = new Color32(255, 240, 0, 255);
            icon.color = Color.white;
            pressKeyText.enabled = true;
        }
    }
}