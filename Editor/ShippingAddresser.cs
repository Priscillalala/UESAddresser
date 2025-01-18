using SmartAddresser.Editor.Core.Models.LayoutRules;
using SmartAddresser.Editor.Core.Tools.Shared;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace UESAddresser.Editor
{
    public static class ShippingAddresser
    {
        const string MENU_ROOT = "Tools/UES Addresser/";
        public const string PACKAGE_ASSETS_DIRECTORY = "Packages/groovesalad.uesaddresser/Assets/";

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
                RoR2LabelRules.RemoveRoR2LabelRules(data.LayoutRule);
                RoR2LabelRules.AddRoR2LabelRules(data.LayoutRule);
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
                RoR2LabelRules.RemoveRoR2LabelRules(data.LayoutRule);
                EditorUtility.SetDirty(data);
            }
        }

        [MenuItem(MENU_ROOT + "Install Addressables Group Template")]
        public static void InstallAddressableGroupTemplate()
        {
            var aa = AddressableAssetSettingsDefaultObject.Settings;
            if (!aa)
            {
                return;
            }
            string modAssetsTemplatePath = PACKAGE_ASSETS_DIRECTORY + "AssetGroupTemplates/Mod Assets.asset";
            var modAssetsTemplate = AssetDatabase.LoadAssetAtPath<AddressableAssetGroupTemplate>(modAssetsTemplatePath);
            if (!modAssetsTemplate)
            {
                Debug.LogError($"Could not find mod assets template at {modAssetsTemplatePath}");
                return;
            }
            if (aa.GroupTemplateObjects.Contains(modAssetsTemplate))
            {
                return;
            }
            for (int i = 0; i < aa.GroupTemplateObjects.Count; i++)
            {
                var template = aa.GetGroupTemplateObject(i);
                if (template != null && template.Name == "Packed Assets")
                {
                    aa.SetGroupTemplateObjectAtIndex(i, modAssetsTemplate);
                    return;
                }
            }
            aa.AddGroupTemplateObject(modAssetsTemplate);
        }
    }
}