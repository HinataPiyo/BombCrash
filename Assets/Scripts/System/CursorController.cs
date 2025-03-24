using UnityEngine;

public class CursorControlle : MonoBehaviour
{
    public Texture2D cursorTexture; // カーソル画像
    public Vector2 hotspot = Vector2.zero; // ホットスポット（クリック位置）
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }
}