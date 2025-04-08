using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BasicUpgradItem : MonoBehaviour
{
    [SerializeField] Button panelButton;
    [SerializeField] GameObject openPanel;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        panelButton.onClick.AddListener(OpenOrClosePanel);
    }

    void OpenOrClosePanel()
    {
        // まだOpenしていないとき
        if(openPanel.activeSelf == false)
        {
            anim.SetTrigger("Open");
        }
        else
        {
            anim.SetTrigger("Close");
        }
    }
}
