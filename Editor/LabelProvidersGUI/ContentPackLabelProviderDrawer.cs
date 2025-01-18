using SmartAddresser.Editor.Foundation.CustomDrawers;
using UnityEditor;

namespace UESAddresser.Editor.LabelProviders
{
    [CustomGUIDrawer(typeof(ContentPackLabelProvider))]
    public sealed class ContentPackLabelProviderDrawer : GUIDrawer<ContentPackLabelProvider>
    {
        protected override void GUILayout(ContentPackLabelProvider target)
        {
            var identifierLabel = ObjectNames.NicifyVariableName(nameof(target.Identifier));
            target.Identifier = EditorGUILayout.TextField(identifierLabel, target.Identifier);
        }
    }
}