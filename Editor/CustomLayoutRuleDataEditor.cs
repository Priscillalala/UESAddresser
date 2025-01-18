using SmartAddresser.Editor.Core.Models.LayoutRules;
using SmartAddresser.Editor.Core.Tools.Addresser.Shared;
using UnityEditor;
using UnityEngine;

namespace UESAddresser.Editor
{
    [CustomEditor(typeof(LayoutRuleData))]
    public class CustomLayoutRuleDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var data = (LayoutRuleData)target;

            if (GUILayout.Button("Open Editor"))
            {
                LayoutRuleDataOpener.OnOpen(target.GetInstanceID(), -1);
            }

            if (GUILayout.Button("Hello"))
            {
                Debug.Log("Hello");
            }

            GUI.enabled = false;
            base.OnInspectorGUI();
            GUI.enabled = true;
        }
    }
}