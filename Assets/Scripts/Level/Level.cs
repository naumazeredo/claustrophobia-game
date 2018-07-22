using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
  public Spawn[] spawns;
  public GameObject bossPrefab;

  public void StartLevel() {
    var children = new List<GameObject>();
    for (int i = 0; i < transform.childCount; i++)
      children.Add(transform.GetChild(i).gameObject);

    foreach (var child in children)
      Destroy(child);

    foreach (var spawn in spawns) {
      var spawner = Instantiate(spawn.prefab, spawn.position, spawn.rotation);
      spawner.transform.position = spawn.position;
      spawner.transform.rotation = spawn.rotation;
      spawner.transform.SetParent(transform);
    }
  }
}
