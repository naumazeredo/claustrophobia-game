using System.Linq;
using UnityEngine;
using UnityEditor;

public class LevelDesignWindow : EditorWindow {
  string levelName;
  GameObject boss;

  [MenuItem("Window/Level Designer")]
  static void Init() {
    var window = (LevelDesignWindow) EditorWindow.GetWindow(
      typeof(LevelDesignWindow),
      false, "Level Designer"
    );
    window.Show();
  }

  void OnGUI() {
    EditorGUILayout.HelpBox(
      "Position Spawn objects on the Scene.\n" +
      "After you complete the desired level, " +
      "name the level, place a boss and " +
      "click Save Level",
      MessageType.Info
    );

    GUILayout.Label("Level properties", EditorStyles.boldLabel);
    levelName = EditorGUILayout.TextField("Name", levelName);
    boss = (GameObject) EditorGUILayout.ObjectField("Boss", boss, typeof(GameObject), false);

    if (boss != null && boss.GetComponent<Spawner>() == null) {
      EditorUtility.DisplayDialog(
        "No Spawner",
        "Boss needs a Spawner component to work.",
        "Ok"
      );

      boss = null;
    }

    GUILayout.Space(8f);

    if (GUILayout.Button("Save Level")) {
      SaveLevel();
      GUIUtility.ExitGUI();
    }

    if (GUILayout.Button("Delete all spawners")) {
      DeleteSpawners();
      GUIUtility.ExitGUI();
    }
  }

  Spawn[] GenerateSpawns() {
    return GameObject.FindGameObjectsWithTag("Spawn")
      .Select(n => n.GetComponent<Spawner>().ToSpawn())
      .ToArray();
  }

  public void SaveLevel() {
    if (string.IsNullOrEmpty(levelName)) {
      EditorUtility.DisplayDialog(
        "No level name",
        "Level needs a name, right?",
        "Ok, I'll give it a name..."
      );
      return;
    }

    var level = new GameObject(levelName);
    level.AddComponent<Level>();

    var levelComponent = level.GetComponent<Level>();
    levelComponent.spawns = GenerateSpawns();
    levelComponent.boss = boss;

    string path = "Assets/Prefabs/Levels/" + levelName + ".prefab";

    if (AssetDatabase.LoadAssetAtPath(path, typeof(GameObject))) {
      if (EditorUtility.DisplayDialog("Are you sure?",
                                      "The prefab already exists. Do you want to overwrite it?",
                                      "Yes", "No"))
        CreatePrefab(level, path);
    } else {
        CreatePrefab(level, path);
    }
  }

  static void CreatePrefab(GameObject obj, string path) {
    PrefabUtility.CreatePrefab(path, obj);
    DestroyImmediate(obj);
    //PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
  }

  public static void DeleteSpawners() {
    bool confirm = EditorUtility.DisplayDialog(
      "Are you sure?",
      "This action will Destroy every Spawner on the Scene. Are you sure?",
      "Delete Spawners", "Cancel"
    );

    if (!confirm)
      return;

    var spawners = GameObject.FindGameObjectsWithTag("Spawn");
    foreach (var spawner in spawners)
      DestroyImmediate(spawner);
  }
}
