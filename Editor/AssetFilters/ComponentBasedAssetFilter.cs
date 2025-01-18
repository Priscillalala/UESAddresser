using SmartAddresser.Editor.Core.Models.Shared.AssetGroups;
using SmartAddresser.Editor.Core.Models.Shared.AssetGroups.AssetFilterImpl;
using SmartAddresser.Editor.Core.Models.Shared.AssetGroups.ValidationError;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace UESAddresser.Editor.AssetFilters
{
    [Serializable]
    [AssetFilter("Component Filter", "Component Filter")]
    public class ComponentBasedAssetFilter : AssetFilterBase
    {
        public TypeReferenceListableProperty componentType = new TypeReferenceListableProperty();
        public bool matchWithDerivedComponentTypes = true;
        public bool searchChildren = false;

        private readonly List<string> invalidAssemblyQualifiedNames = new List<string>();
        private readonly List<Type> validComponentTypes = new List<Type>();
        private readonly Dictionary<string, Type[]> prefabToComponentTypes = new Dictionary<string, Type[]>();
        private readonly Dictionary<Type, bool> componentResultsCache = new Dictionary<Type, bool>();
        private readonly object componentResultsCacheLocker = new object();

        public override void SetupForMatching()
        {
            invalidAssemblyQualifiedNames.Clear();
            validComponentTypes.Clear();

            foreach (var typeRef in componentType)
            {
                if (typeRef == null || !typeRef.IsValid())
                {
                    continue;
                }

                var assemblyQualifiedName = typeRef.AssemblyQualifiedName;
                var type = Type.GetType(assemblyQualifiedName);
                if (type == null)
                {
                    invalidAssemblyQualifiedNames.Add(assemblyQualifiedName);
                }
                else
                {
                    validComponentTypes.Add(type);
                }
            }

            prefabToComponentTypes.Clear();
            List<Component> results = new List<Component>();
            foreach (var prefabPath in AssetDatabase.FindAssets("t:prefab a:assets").Select(AssetDatabase.GUIDToAssetPath))
            {
                if (prefabToComponentTypes.ContainsKey(prefabPath))
                {
                    continue;
                }
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                if (!prefab)
                {
                    continue;
                }
                results.Clear();
                if (searchChildren)
                {
                    prefab.GetComponentsInChildren(true, results);
                }
                else
                {
                    prefab.GetComponents(results);
                }
                Type[] componentTypes = results
                    .Select(x => x.GetType())
                    .Distinct()
                    .ToArray();
                prefabToComponentTypes.Add(prefabPath, componentTypes);
            }

            componentResultsCache.Clear();
        }

        public override bool Validate(out AssetFilterValidationError error)
        {
            if (invalidAssemblyQualifiedNames.Count >= 1)
            {
                error = new AssetFilterValidationError(
                    this,
                    invalidAssemblyQualifiedNames
                        .Select(qualifiedName => $"Invalid type reference: {qualifiedName}")
                        .ToArray());
                return false;
            }

            error = null;
            return true;
        }

        public override bool IsMatch(string assetPath, Type assetType, bool isFolder)
        {
            if (assetType != typeof(GameObject) || !prefabToComponentTypes.TryGetValue(assetPath, out var componentTypes))
            {
                return false;
            }
            foreach (var componentType in componentTypes)
            {
                if (!componentResultsCache.TryGetValue(componentType, out var componentResult))
                {
                    componentResult = false;
                    foreach (var validComponentType in validComponentTypes)
                    {
                        if (componentType == validComponentType || (matchWithDerivedComponentTypes && componentType.IsSubclassOf(validComponentType)))
                        {
                            componentResult = true;
                            break;
                        }
                    }
                    lock (componentResultsCacheLocker)
                    {
                        componentResultsCache.Add(componentType, componentResult);
                    }
                }
                if (componentResult)
                {
                    return true;
                }
            }
            return false;
        }

        public override string GetDescription()
        {
            var result = new StringBuilder();
            var elementCount = 0;
            foreach (var type in componentType)
            {
                if (type == null || string.IsNullOrEmpty(type.Name))
                    continue;

                if (elementCount >= 1)
                    result.Append(" || ");

                result.Append(type.Name);
                elementCount++;
            }

            if (result.Length >= 1)
            {
                if (elementCount >= 2)
                {
                    result.Insert(0, "( ");
                    result.Append(" )");
                }

                result.Insert(0, "Component: ");
            }

            return result.ToString();
        }
    }
}