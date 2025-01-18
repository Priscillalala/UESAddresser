using SmartAddresser.Editor.Foundation.CustomDrawers;
using UESAddresser.Editor.AddressProviders;
using UnityEditor;

namespace UESAddresser.Editor.AddressProvidersGUI
{
    [CustomGUIDrawer(typeof(RoR2StyleAddressProvider))]
    public class RoR2StyleAddressProviderDrawer : GUIDrawer<RoR2StyleAddressProvider>
    {
        protected override void GUILayout(RoR2StyleAddressProvider target)
        {
            EditorGUILayout.LabelField("RoR2 addresses omit the Assets/ folder from the start of the asset path. Mods are encouraged to maintain this style so that addresses begin with the root mod folder.", EditorStyles.wordWrappedLabel);
        }
    }
}