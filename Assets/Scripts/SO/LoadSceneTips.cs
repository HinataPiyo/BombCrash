using UnityEngine;

[CreateAssetMenu(fileName = "LoadSceneTips", menuName = "LoadSceneTips")]
public class LoadSceneTips : ScriptableObject
{
    [SerializeField] Sprite[] background;
    [TextArea(5, 10), SerializeField] string[] tips;

    public Sprite[] Background => background;
    public string[] Tips => tips;
}