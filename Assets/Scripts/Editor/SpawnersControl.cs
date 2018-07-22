using System.Linq;
using UnityEngine;
using UnityEditor;

[InitializeOnLoadAttribute]
public static class SpawnersControl {
  [SerializeField]
  public static Spawner[] spawners;

  static SpawnersControl() {
    EditorApplication.playModeStateChanged += DisableSpawners;
  }

  private static void DisableSpawners(PlayModeStateChange state) {
    if (state == PlayModeStateChange.EnteredPlayMode) {
      spawners =
        GameObject
        .FindGameObjectsWithTag("Spawn")
        .Select(c => c.GetComponent<Spawner>())
        .ToArray();

      /*
      spawners
        .ToList()
        .ForEach(c => c.gameObject.SetActive(false));
        */
    }
  }
}
