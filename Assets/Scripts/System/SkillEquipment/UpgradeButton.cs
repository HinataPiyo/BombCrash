using UnityEngine;
using UnityEngine.UI;

namespace OtomoSkill
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] bool isHolding = false;
        [SerializeField] Button upgradeButton;
        [SerializeField] RectTransform progressImage;
        const int maxProgress = 200;
        const float progressSpeed = 200f;


        void Start()
        {
            upgradeButton.onClick.AddListener(() => ButtonOnClick());
            progressImage.sizeDelta = new Vector2(0, 40);
        }

        void Update()
        {
            bool isCheck = isHolding && OtomoSkillDetailPanel.Instance.CheckIPCostAndProficiency();
            upgradeButton.interactable = isCheck; // ボタンのインタラクティブを更新
            // 長押し中
            if (isHolding && isCheck)
            {
                if (progressImage.sizeDelta.x < maxProgress)
                {
                    Vector2 size = new Vector2(progressSpeed * Time.deltaTime, 0);
                    progressImage.sizeDelta += size;
                }
                else    // 最大まで押された場合
                {
                    // ボタンを押したときの処理を実行
                    ButtonOnClick();
                }
            }
            else
            {
                if (progressImage.sizeDelta.x > 0)
                {
                    Vector2 size = new Vector2(progressSpeed * 4 * Time.deltaTime, 0);
                    progressImage.sizeDelta -= size;
                }
            }
        }

        /// <summary>
        /// ボタンを押したときの処理
        /// </summary>
        void ButtonOnClick()
        {
            // スキルのレベルアップ処理を実行
            OtomoSkillDetailPanel.Instance.SkillSO.ProficiencyLevelUp();
            Debug.Log("スキルのレベルアップ処理を実行");
            // テキストを更新
            OtomoSkillDetailPanel.Instance.SetText(OtomoSkillDetailPanel.Instance.SkillSO);
        }

        // EventTriggerから呼び出す
        public void OnPointerDown()
        {
            isHolding = true;
        }

        public void OnPointerUp()
        {
            isHolding = false;
        }
    }
}
