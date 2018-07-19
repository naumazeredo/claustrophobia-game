using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class LevelDesigner : MonoBehaviour {
  public GameObject boss;
  public string levelName;

  List<Spawn> spawns;

  void GenerateLevel() {
    Spawner[] spawners = GameObject.FindGameObjectsWithTag("Spawn")
      .Select(n => n.GetComponent<Spawner>())
      .ToArray();

    spawns = new List<Spawn>();
    if (spawners != null) {
      foreach (var spawner in spawners) {
        spawns.Add(spawner.ToSpawn());
        //DestroyImmediate(spawner.gameObject);
      }
    }
  }

  public void SaveLevel() {
  }

  public void ShowLevelSpawners() {
  }

  public void DeleteSpawners() {
  }

  // TODO: Save level
  // TODO: Show scene
  // TODO: Destroy spawners
}
