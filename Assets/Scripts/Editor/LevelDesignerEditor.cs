using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelDesigner))]
public class LevelDesignerEditor : Editor {
  //bool showActions;

  public override void OnInspectorGUI() {
    var script = (LevelDesigner) target;

    //DrawDefaultInspector();

    //showActions = EditorGUILayout.Foldout(showActions, "Actions");
    //if (showActions) {
    //}

    EditorGUILayout.HelpBox(
      "Position Spawn objects on the Scene.\n" +
      "After you complete the desired level, " +
      "name the level, place a boss and " +
      "click Save Level",
      MessageType.Info
    );

    script.levelName = EditorGUILayout.TextField("Level name", script.levelName);
    script.boss = (GameObject) EditorGUILayout.ObjectField("Boss", script.boss, typeof(GameObject), false);

    GUILayout.Space(8f);

    if (GUILayout.Button("Save Level")) {
      script.SaveLevel();
    }

    if (GUILayout.Button("Show level spawners")) {
      script.ShowLevelSpawners();
    }

    if (GUILayout.Button("Delete all spawners")) {
      script.DeleteSpawners();
    }
  }
}
