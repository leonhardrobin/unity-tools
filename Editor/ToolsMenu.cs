using UnityEditor;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;
using static System.IO.Directory;
using static System.IO.Path;

namespace leonhardrobin
{
    public static class ToolsMenu
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Dir("_Project", "Scripts", "Prefabs", "Art", "Scenes");
            Refresh();
        }
        
        public static void Dir(string root, params string[] folders)
        {
            var path = Combine(dataPath, root);
            foreach (var folder in folders)
            {
                CreateDirectory(Combine(path, folder));
            }
        }
    }
}
