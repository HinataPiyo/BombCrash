using UnityEngine;

public class HomeCharacterAnimation : MonoBehaviour
{
    [SerializeField] GameObject homeObj;
    [SerializeField] GameObject characterObj;
    [SerializeField] Animator anim;

    void Update()
    {
        ChackActiveCaracter();
    }

    /// <summary>
    /// ホーム画面にいるキャラクター画像
    /// </summary>
    void ChackActiveCaracter()
    {
        characterObj.SetActive(homeObj.activeSelf);
        anim.SetBool("Move", homeObj.activeSelf);
    }
}