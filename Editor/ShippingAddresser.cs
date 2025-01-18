using SmartAddresser.Editor.Core.Models.LayoutRules;
using SmartAddresser.Editor.Core.Models.Shared.AssetGroups;
using SmartAddresser.Editor.Core.Models.Shared.AssetGroups.AssetFilterImpl;
using SmartAddresser.Editor.Core.Models.Shared.AssetGroups.ValidationError;
using SmartAddresser.Editor.Core.Tools.Addresser.Shared;
using SmartAddresser.Editor.Core.Tools.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEditor;
using UnityEditor.EventSystems;
using UnityEditor.Search;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.WSA;

namespace UESAddresser.Editor
{
    public class ShippingAddresser
    {
        [MenuItem("Tools/UES Addresser/LayoutRuleData/Setup")]
        public static void Setup()
        {
            LayoutRuleDataRepository dataRepository = new LayoutRuleDataRepository();
            GenericMenu menu = new GenericMenu();
            foreach (LayoutRuleData layoutRuleData in dataRepository.LoadAll())
            {
                menu.AddItem(new GUIContent(layoutRuleData.name), false,
                        () =>
                        {
                            Debug.Log(layoutRuleData.name);
                        });
            }
            menu.DropDown(Rect.zero);
        }

        [MenuItem("Tools/UES Addresser/LayoutRuleData/Setup", true)]
        public static bool ValidateSetup()
        {
            return true;
        }
    }
}