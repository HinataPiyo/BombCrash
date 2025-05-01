using UnityEngine;
using UnityEngine.UI;

namespace Parts
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
            // 長押し中
            if (isHolding)
            {
                if (progressImage.sizeDelta.x < maxProgress)
                {
                    Vector2 size = new Vector2(progressSpeed * Time.deltaTime, 0);
                    progressImage.sizeDelta += size;
                }
                else    // 最大まで押された場合
                {

                }
            }
            else
            {
                if (progressImage.sizeDelta.x > 0)
                {
                    Vector2 size = new Vector2(progressSpeed * 4 * Time.deltaTime, 0);
                    progressImage.sizeDelta -= size; ;
                }
            }
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
