using SmartAddresser.Editor.Core.Models.LayoutRules.LabelRules;
using SmartAddresser.Editor.Core.Models.Shared;
using System;
using UnityEngine;

namespace UESAddresser.Editor.LabelProviders
{
    [Serializable]
    [IgnoreLabelProvider]
    public class RoR2LabelProvider : ILabelProvider
    {
        [SerializeField] private string _label;

        public string Label
        {
            get => _label;
            set => _label = value;
        }

        void IProvider<string>.Setup() { }

        string IProvider<string>.Provide(string assetPath, Type assetType, bool isFolder)
        {
            if (string.IsNullOrEmpty(_label))
            {
                return null;
            }

            return _label;
        }

        public string GetDescription()
        {
            if (string.IsNullOrEmpty(_label))
                return null;

            return $"RoR2: {_label}";
        }
    }
}