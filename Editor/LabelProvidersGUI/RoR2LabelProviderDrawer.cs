using SmartAddresser.Editor.Foundation.CustomDrawers;
using UnityEditor;
using UnityEngine;

namespace UESAddresser.Editor.LabelProviders
{
    [CustomGUIDrawer(typeof(RoR2LabelProvider))]
    public sealed class RoR2LabelProviderDrawer : GUIDrawer<RoR2LabelProvider>
    {
        protected override void GUILayout(RoR2LabelProvider target)
        {
            var labelLabel = ObjectNames.NicifyVariableName(nameof(target.Label));
            GUI.enabled = false;
            target.Label = EditorGUILayout.TextField(labelLabel, target.Label);
            GUI.enabled = true;
        }
    }
}