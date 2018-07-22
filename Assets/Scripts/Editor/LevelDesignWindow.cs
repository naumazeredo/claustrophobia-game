using System.Linq;
using UnityEngine;
using UnityEditor;

public class LevelDesignWindow : EditorWindow {
  [SerializeField]
  string levelName;

  [SerializeField]
  GameObject boss;

  [SerializeField]
  bool testOnPlay;

  [SerializeField]
  bool onPlaymode;

  Vector2 scroll;

  [MenuItem("Claustrophobia/Level Designer")]
  static void Init() {
    var window = (LevelDesignWindow) EditorWindow.GetWindow(
      typeof(LevelDesignWindow),
      false, "Level Designer"
    );
    window.Show();

  }

  void OnGUI() {
    scroll = EditorGUILayout.BeginScrollView(scroll);

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

    /*
    if (boss != null && boss.GetComponent<Spawner>() == null) {
      EditorUtility.DisplayDialog(
        "No Spawner",
        "Boss needs a Spawner component to work.",
        "Ok"
      );

      boss = null;
    }
    */

    GUILayout.Space(8f);

    if (GUILayout.Button("Save Level")) {
      SaveLevel();
      GUIUtility.ExitGUI();
    }

    if (GUILayout.Button("Delete all spawners")) {
      DeleteSpawners();
      GUIUtility.ExitGUI();
    }

    if (GUILayout.Button("Remove boss")) {
      RemoveBoss();
      GUIUtility.ExitGUI();
    }

    EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);

    testOnPlay = EditorGUILayout.Toggle("Test Level on Play", testOnPlay);

    EditorGUI.EndDisabledGroup();

    EditorGUILayout.EndScrollView();
  }

  void Update() {
    // FIXME: Does not work if window is not showing! (--> Maximize On Play <--)
    if (!testOnPlay)
      return;

    if (EditorApplication.isPlaying && !onPlaymode) {
      if (FindObjectOfType<Level>()) {
        Debug.LogWarning("Test Level was not created because these's a Level already active on Hierarchy");
      } else {
        Spawn[] spawns =
          SpawnersControl.spawners
          .Select(c => SpawnerToSpawn(c.gameObject))
          .ToArray();

        var level = CreateLevel("Level Test", spawns);

        // XXX
        GameObject
          .FindGameObjectsWithTag("Spawn")
          .ToList()
          .ForEach(c => c.SetActive(false));

        GameObject
          .FindWithTag("GameController")
          .GetComponent<GameController>()
          .RegisterLevel(level.GetComponent<Level>());
      }

      onPlaymode = true;
    }

    if (!EditorApplication.isPlaying && onPlaymode) {
      onPlaymode = false;
    }
  }

  Spawn[] GenerateSpawns() {
    return GameObject.FindGameObjectsWithTag("Spawn")
      .Select(c => SpawnerToSpawn(c))
      .ToArray();
  }

  GameObject CreateLevel(string objName, Spawn[] spawns) {
    var level = new GameObject(objName);
    var levelComponent = level.AddComponent<Level>();

    levelComponent.spawns = spawns;
    levelComponent.bossPrefab = boss;

    return level;
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

    var level = CreateLevel(levelName, GenerateSpawns());

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

  public void RemoveBoss() {
    boss = null;
  }

  Spawn SpawnerToSpawn(GameObject go) {
    Spawn spawn = new Spawn();

    Object obj = PrefabUtility.GetCorrespondingObjectFromSource(go);
    spawn.prefab = (GameObject) AssetDatabase.LoadAssetAtPath(
      AssetDatabase.GetAssetPath(obj),
      typeof(GameObject)
    );

    spawn.position = go.transform.position;
    spawn.rotation = go.transform.rotation;

    return spawn;
  }
}
