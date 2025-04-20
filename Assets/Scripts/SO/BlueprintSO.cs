using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlueprintSO", menuName = "Equipment/BlueprintSO")]
public class BlueprintSO : ScriptableObject
{
    [SerializeField] BlueprintData[] blueprintDatas;
    [SerializeField] PartsUpgradeMaterialData[] partsUpgradeMaterialDatas;
    public BlueprintData[] BlueprintDatas { get { return blueprintDatas; } }
    public PartsUpgradeMaterialData[] PartsUpgradeMaterialDatas { get { return partsUpgradeMaterialDatas; } }

    /// <summary>
    /// 設計図
    /// </summary>
    [System.Serializable]
    public class BlueprintData
    {
        public Sprite icon;
        public string blueprintName;
        public Rarity rarity;
    }


    /// <summary>
    /// 強化素材
    /// </summary>
    [System.Serializable]
    public class PartsUpgradeMaterialData
    {
        public Sprite icon;
        public string upgradeMaterialName;
        public Rarity rarity;
    }
}