using SmartAddresser.Editor.Foundation.CustomDrawers;
using UESAddresser.Editor.AssetFilters;

namespace UESAddresser.Editor.AssetFiltersGUI
{
    [CustomGUIDrawer(typeof(ComponentBasedAssetFilter))]
    public class ComponentBasedAssetFilterDrawer : GUIDrawer<ComponentBasedAssetFilter>
    {
        private ComponentTypeReferenceListablePropertyGUI _listablePropertyGUI;

        public override void Setup(object target)
        {
            base.Setup(target);
            _listablePropertyGUI = new ComponentTypeReferenceListablePropertyGUI("Component Type", Target.componentType);
        }

        protected override void GUILayout(ComponentBasedAssetFilter target)
        {
            _listablePropertyGUI.DoLayout();
        }
    }
}