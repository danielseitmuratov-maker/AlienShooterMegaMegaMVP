using System.IO;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Editor
{
    public class ProjectFoldersBuilder : EditorWindow
    {
        private const string RootFolder = "Assets/_Project";

        private static readonly string[] Folders =
        {
            "Scenes",
            "Scenes/Gameplay",

            "CodeBase",
            "CodeBase/Runtime",
            "CodeBase/Runtime/Core",
            "CodeBase/Runtime/Common",
            "CodeBase/Runtime/Meta",
            "CodeBase/Editor",

            "Data",
            "Data/Configs",
            "Data/Items",

            "Art",
            "Art/Models",
            "Art/Materials",
            "Art/Textures",
            "Art/Animations",

            "Audio",
            "Audio/Music",
            "Audio/SFX",

            "Prefabs",

            "UI",

        };

        [MenuItem("Tools/Project/Generate Folder Structure")]
        public static void GenerateFolders()
        {
            if (Directory.Exists(RootFolder))
            {
                bool overwrite = EditorUtility.DisplayDialog(
                    "Структура уже существует",
                    $"Папка {RootFolder} уже существует. Продолжить и создать недостающие папки?",
                    "Да", "Отмена");

                if (!overwrite) return;
            }

            int created = 0;

            if (!Directory.Exists(RootFolder))
            {
                Directory.CreateDirectory(RootFolder);
                created++;
            }

            foreach (string folder in Folders)
            {
                string fullPath = Path.Combine(RootFolder, folder);
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                    created++;
                }
            }

            AssetDatabase.Refresh();

            Debug.Log($"[ProjectFolderGenerator] Готово! Создано папок: {created}. Корень: {RootFolder}");
        }
    }
}