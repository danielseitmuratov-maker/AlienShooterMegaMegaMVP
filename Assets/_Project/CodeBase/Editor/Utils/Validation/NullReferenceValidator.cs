#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Project.CodeBase.Common.Attributes;
using UnityEditor;
using UnityEngine;

namespace _Project.CodeBase.Editor.Utils.Validation
{
    public class NullReferenceValidator : EditorWindow
    {
        [MenuItem("Tools/Validation/Scan Null References")]
        public static void ScanAll()
        {
            string[] soGuids = AssetDatabase.FindAssets("t:ScriptableObject");

            foreach (var guid in soGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                if (so == null)
                    continue;

                CheckFields(so, path);
            }

            string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");

            foreach (var guid in prefabGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab == null)
                    continue;

                foreach (MonoBehaviour mb in prefab.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    if (mb == null)
                        continue;

                    CheckFields(mb, path);
                }
            }

            Debug.Log("Сканирование завершено. Проверь консоль на предупреждения.");
        }

        private static void CheckFields(UnityEngine.Object obj, string assetPath)
        {
            Type type = obj.GetType();

            IEnumerable<FieldInfo> fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(f => f.IsPublic || f.IsDefined(typeof(SerializeField), true))
                .Where(f => typeof(UnityEngine.Object).IsAssignableFrom(f.FieldType)
                            || (f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(List<>)));

            foreach (var field in fields)
            {
                if (!field.IsDefined(typeof(RequiredAttribute), true)) continue;

                var value = field.GetValue(obj);
                if (value == null || (value is UnityEngine.Object uo && uo == null))
                {
                    Debug.LogWarning(
                        $"[Null Reference] {assetPath} → {type.Name}.{field.Name} — поле обязательно, но не заполнено.",
                        obj);
                }
            }
        }
    }
}
#endif