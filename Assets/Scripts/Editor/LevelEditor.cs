using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor {
  //bool showActions;

  public override void OnInspectorGUI() {
    Level levelScript = (Level) target;

    DrawDefaultInspector();

    GUILayout.Space(16f);
    //showActions = EditorGUILayout.Foldout(showActions, "Actions");

    //if (showActions) {
      if (GUILayout.Button("Children to Spawns")) {
        levelScript.ChildrenToSpawns();
      }

      if (GUILayout.Button("Spawns to Children")) {
        levelScript.SpawnsToChildren();
      }
    //}
  }
}
