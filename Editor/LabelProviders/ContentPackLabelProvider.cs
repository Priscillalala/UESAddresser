using SmartAddresser.Editor.Core.Models.LayoutRules.LabelRules;
using SmartAddresser.Editor.Core.Models.Shared;
using System;
using UnityEngine;

namespace UESAddresser.Editor.LabelProviders
{
    [Serializable]
    public class ContentPackLabelProvider : ILabelProvider
    {
        [SerializeField] private string _identifier;

        public string Identifier
        {
            get => _identifier;
            set => _identifier = value;
        }

        void IProvider<string>.Setup() { }

        string IProvider<string>.Provide(string assetPath, Type assetType, bool isFolder)
        {
            if (string.IsNullOrEmpty(_identifier))
            {
                return null;
            }

            return "ContentPack:" + _identifier;
        }

        public string GetDescription()
        {
            if (string.IsNullOrEmpty(_identifier))
                return null;

            return $"Content Pack: {_identifier}";
        }
    }
}