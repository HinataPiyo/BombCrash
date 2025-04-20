using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    public ScreenFlash screenFlashManager;

    public void OnButtonClick()
    {
        if (screenFlashManager != null)
        {
            screenFlashManager.Flash();
        }
        else
        {
            Debug.LogError("ScreenFlashManagerがアサインされていません！");
        }
    }
}
