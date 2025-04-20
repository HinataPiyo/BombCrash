using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage; // InspectorからアサインするImage
    public float flashDuration = 0.1f; // 光らせる時間
    public Color flashColor = Color.white; // 光の色

    public void Flash()
    {
        if (flashImage != null)
        {
            StartCoroutine(FlashCoroutine());
        }
        else
        {
            Debug.LogError("FlashImageがアサインされていません！");
        }
    }

    private IEnumerator FlashCoroutine()
    {
        flashImage.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        flashImage.color = Color.clear; // または new Color(1f, 1f, 1f, 0f);
    }
}