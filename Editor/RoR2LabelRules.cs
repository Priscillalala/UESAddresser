using RoR2.ContentManagement;
using SmartAddresser.Editor.Core.Models.LayoutRules;
using SmartAddresser.Editor.Core.Models.LayoutRules.LabelRules;
using SmartAddresser.Editor.Core.Models.Shared.AssetGroups;
using SmartAddresser.Editor.Core.Models.Shared.AssetGroups.AssetFilterImpl;
using SmartAddresser.Editor.Core.Tools.Shared;
using System;
using UESAddresser.Editor.AssetFilters;
using UESAddresser.Editor.LabelProviders;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace UESAddresser.Editor
{
    public static class RoR2LabelRules
    {
        public static void AddRoR2LabelRules(LayoutRule layoutRule)
        {
            foreach ((Type assetType, string label) in AddressablesLabels.assetTypeLabels)
            {
                LabelRule labelRule = new LabelRule();
                labelRule.Name.Value = $"RoR2, {label} Assets";

                labelRule.AssetGroups.Clear();
                AssetGroup assetGroup = new AssetGroup();
                assetGroup.Name.Value = "Asset Type Group";
                assetGroup.Filters.Clear();
                assetGroup.Filters.Add(new TypeBasedAssetFilter
                {
                    Type = { IsListMode = false, Value = TypeReference.Create(assetType) },
                    MatchWithDerivedTypes = true,
                });
                labelRule.AssetGroups.Add(assetGroup);

                labelRule.LabelProvider.Value = new RoR2LabelProvider { Label = label };

                layoutRule.LabelRules.Add(labelRule);
            }
            foreach ((Type componentType, string label) in AddressablesLabels.componentTypeLabels)
            {
                LabelRule labelRule = new LabelRule();
                labelRule.Name.Value = $"RoR2, {label} Prefabs";

                labelRule.AssetGroups.Clear();
                AssetGroup assetGroup = new AssetGroup();
                assetGroup.Name.Value = "Component Type Group";
                assetGroup.Filters.Clear();
                assetGroup.Filters.Add(new ComponentBasedAssetFilter
                {
                    componentType = { IsListMode = false, Value = TypeReference.Create(componentType) },
                    matchWithDerivedComponentTypes = true,
                    searchChildren = false,
                });
                labelRule.AssetGroups.Add(assetGroup);

                labelRule.LabelProvider.Value = new RoR2LabelProvider { Label = label };

                layoutRule.LabelRules.Add(labelRule);
            }
        }

        public static void RemoveRoR2LabelRules(LayoutRule layoutRule)
        {
            for (int i = layoutRule.LabelRules.Count - 1; i >= 0; i--)
            {
                LabelRule labelRule = layoutRule.LabelRules[i];
                if (labelRule.LabelProvider.Value is RoR2LabelProvider)
                {
                    layoutRule.LabelRules.RemoveAt(i);
                }
            }
        }
    }
}