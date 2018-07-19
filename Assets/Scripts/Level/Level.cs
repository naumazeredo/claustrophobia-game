using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Level : MonoBehaviour {
  public List<Spawn> spawns;
  public GameObject boss;

  public void SpawnsToChildren() {
    var children = new List<GameObject>();
    for (int i = 0; i < transform.childCount; i++)
      children.Add(transform.GetChild(i).gameObject);

    foreach (var child in children)
      Destroy(child);

    foreach (var spawn in spawns) {
      var spawner = PrefabUtility.InstantiatePrefab(spawn.prefab) as GameObject;
      spawner.transform.localPosition = spawn.position;
      spawner.transform.localRotation = spawn.rotation;
      spawner.transform.SetParent(transform);
    }
  }

  public void ChildrenToSpawns() {
    Spawner[] spawners = GetComponentsInChildren<Spawner>();

    spawns.Clear();
    if (spawners != null) {
      foreach (var spawner in spawners) {
        spawns.Add(spawner.ToSpawn());
      }
    }
  }
}
