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
            progressImage.sizeDelta = new Vector2(0, 40);
        }

        void Update()
        {
            bool isCheck = OtomoSkillDetailPanel.Instance.CheckIPCostAndProficiency();
            upgradeButton.interactable = isCheck; // ボタンのインタラクティブを更新
            // 長押し中
            if (isHolding && isCheck)
            {
                if (progressImage.sizeDelta.x < maxProgress)
                {
                    Vector2 size = new Vector2(progressSpeed * Time.deltaTime, 0);
                    progressImage.sizeDelta += size;
                }
                else if (progressImage.sizeDelta.x >= maxProgress)    // 最大まで押された場合
                {
                    // ボタンを押したときの処理を実行
                    ExecuteUpgrade();
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
        void ExecuteUpgrade()
        {
            OtomoSkillDetailPanel.Instance.SkillSO.ProficiencyLevelUp();        // スキルのレベルアップ
            Debug.Log("スキルのレベルアップ処理を実行");
            OtomoSkillDetailPanel.Instance.SetText(OtomoSkillDetailPanel.Instance.SkillSO);     // スキルの情報を更新
            progressImage.sizeDelta = new Vector2(0, 40);       // プログレスバーをリセット
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
