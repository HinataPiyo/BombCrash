using UnityEngine;

public class PulsingSpriteTransparency : MonoBehaviour
{
    [Tooltip("透明度が完全に透明になるまでの時間（半周期）。小さいほど速く点滅します。")]
    [Range(0.1f, 5.0f)] // 0.1秒から5秒の範囲で調整可能にする
    public float pulseSpeed = 1.0f; // 例: 1.0f だと、1秒かけて透明になり、1秒かけて不透明になる

    private SpriteRenderer spriteRenderer;
    private Color originalColor; // スプライトの元の色（RGB値）を保持する

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRendererが見つかりません！スクリプトを無効にします。");
            enabled = false;
            return;
        }

        // スプライトの元の色（RGB値）を保持しておく
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // Time.time はゲームが開始されてからの時間（秒）
        // Mathf.Sin(x) は x が 0 から π（パイ、Mathf.PI）の時、0から1に増加
        // x が π から 2π の時、1から0に減少
        // 2π の周期で -1 から 1 の間で変動する

        // pulseSpeed を使ってサイン波の周期を調整
        // 速度が速いほど、サイン波の周期が短くなり、透明度の変化が速くなる
        float alpha = Mathf.Sin(Time.time / pulseSpeed * Mathf.PI);

        // サイン波は -1 から 1 の値を取るので、これを 0 から 1 の範囲に変換する
        // Mathf.Abs() で絶対値を取ることで、-1 から 1 の範囲を 0 から 1 に変換し、
        // 0 -> 1 -> 0 -> 1 ... のように脈動させる
        alpha = Mathf.Abs(alpha); 

        // 最終的なアルファ値を現在の色に適用
        // スプライトのRGB値は変更せず、アルファ値（透明度）のみを操作する
        Color currentColor = originalColor; // 元の色を取得し、RGB値を保持
        currentColor.a = alpha;            // 新しいアルファ値を設定
        spriteRenderer.color = currentColor; // スプライトに適用
    }
}