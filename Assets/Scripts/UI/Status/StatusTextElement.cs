using TMPro;
using UnityEngine;

public class StatusTextElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI basicValueText;
    [SerializeField] TextMeshProUGUI equipvalueText;
    
    public TextMeshProUGUI BasicValueText { get { return basicValueText; } }
    public TextMeshProUGUI EquipValueText { get { return equipvalueText; } }
}
