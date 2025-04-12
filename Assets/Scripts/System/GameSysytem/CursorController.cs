using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorControlle : MonoBehaviour
{
    void Start()
    {
        UpdateCursorSize();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーンロード後にカーソルを再設定
        UpdateCursorSize();
    }

    void UpdateCursorSize()
    {
        // ウィンドウサイズを取得
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // カーソルのサイズをウィンドウサイズに基づいて計算
        int newWidth = Mathf.Clamp(screenWidth / 20, 16, 128); // 最小16, 最大128
        int newHeight = Mathf.Clamp(screenHeight / 20, 16, 128);

        // カーソル画像をリサイズ
        Texture2D resizedCursor = ResizeTexture(CursorManager.Instance.cursorTexture, newWidth, newHeight);

        // カーソルを設定
        CursorManager.Instance.UpdateCursor(resizedCursor, CursorManager.Instance.hotspot, CursorManager.Instance.cursorMode);
    }

    Texture2D ResizeTexture(Texture2D originalTexture, int width, int height)
    {
        Texture2D resizedTexture = new Texture2D(width, height);
        Color[] pixels = originalTexture.GetPixels();
        Color[] resizedPixels = resizedTexture.GetPixels();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float u = (float)x / width;
                float v = (float)y / height;
                resizedPixels[y * width + x] = originalTexture.GetPixelBilinear(u, v);
            }
        }

        resizedTexture.SetPixels(resizedPixels);
        resizedTexture.Apply();
        return resizedTexture;
    }
}