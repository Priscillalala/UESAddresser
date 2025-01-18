using SmartAddresser.Editor.Core.Models.LayoutRules.AddressRules;
using System;

namespace UESAddresser.Editor.AddressProviders
{
    [Serializable]
    public class RoR2StyleAddressProvider : IAddressProvider
    {
        const string ASSETS = "Assets/";

        public void Setup()
        {

        }

        public string Provide(string assetPath, Type assetType, bool isFolder)
        {
            if (assetPath != null && assetPath.StartsWith(ASSETS))
            {
                return assetPath[ASSETS.Length..];
            }
            return assetPath;
        }

        public string GetDescription()
        {
            return "Style: RoR2";
        }
    }
}