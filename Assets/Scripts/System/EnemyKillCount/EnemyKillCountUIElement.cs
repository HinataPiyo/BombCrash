using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKillCountUIElement : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI enemyNameText;
    [SerializeField] TextMeshProUGUI killCountText;

    public void SetTextValue(EnemySO enemySO, int killCount)
    {
        icon.sprite = enemySO.EnemyIcon;
        enemyNameText.text = enemySO.EnemyName;
        killCountText.text = "x" + killCount.ToString("F0");
    }
}