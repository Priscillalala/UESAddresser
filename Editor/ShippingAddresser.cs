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

namespace UESAddresser.Editor
{
    public class ShippingAddresser
    {
        const string MENU_ROOT = "Tools/UES Addresser/";
        const string DATA_CONTEXT_ROOT = "CONTEXT/" + nameof(LayoutRuleData) + "/";


        const string CREATE_LABEL_RULES_PATH = MENU_ROOT + "Create RoR2 Label Rules/Primary Layout Rule Data";
        const string REMOVE_LABEL_RULES_PATH = MENU_ROOT + "Remove RoR2 Label Rules/Primary Layout Rule Data";

        [MenuItem(CREATE_LABEL_RULES_PATH, true)]
        [MenuItem(REMOVE_LABEL_RULES_PATH, true)]
        public static bool ValidatePrimaryLayoutRuleData()
        {
            var instance = SmartAddresserProjectSettings.instance;
            return instance && instance.PrimaryData;
        }

        [MenuItem(CREATE_LABEL_RULES_PATH)]
        [MenuItem(DATA_CONTEXT_ROOT + "Create RoR2 Label Rules")]
        public static void CreateLabelRules(MenuCommand command)
        {
            if (command.context is not LayoutRuleData data)
            {
                var instance = SmartAddresserProjectSettings.instance;
                data = instance ? instance.PrimaryData : null;
            }
            if (data)
            {
                RemoveRoR2LabelRules(data.LayoutRule);
                AddRoR2LabelRules(data.LayoutRule);
                EditorUtility.SetDirty(data);
            }
        }

        [MenuItem(REMOVE_LABEL_RULES_PATH)]
        [MenuItem(DATA_CONTEXT_ROOT + "Remove RoR2 Label Rules")]
        public static void RemoveLabelRules(MenuCommand command)
        {
            if (command.context is not LayoutRuleData data)
            {
                var instance = SmartAddresserProjectSettings.instance;
                data = instance ? instance.PrimaryData : null;
            }
            if (data)
            {
                RemoveRoR2LabelRules(data.LayoutRule);
                EditorUtility.SetDirty(data);
            }
        }

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