using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public Texture2D cursorTexture; // デフォルトのカーソル画像
    public Vector2 hotspot = Vector2.zero; // ホットスポット（クリック位置）
    public CursorMode cursorMode = CursorMode.Auto;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateCursor(Texture2D texture, Vector2 hotspot, CursorMode mode)
    {
        Cursor.SetCursor(texture, hotspot, mode);
    }

    public void ResetCursor()
    {
        UpdateCursor(cursorTexture, hotspot, cursorMode);
    }
}