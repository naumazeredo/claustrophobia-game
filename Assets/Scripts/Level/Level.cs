using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Level : MonoBehaviour {
  public List<Spawn> spawns;
  public GameObject boss;

  GameController gameController;

  void Start () {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    gameController.RegisterLevel(this, spawns.Count);
  }

  public void SpawnBoss() {
    StartCoroutine(SpawnBossCoroutine());
  }

  IEnumerator SpawnBossCoroutine() {
    Debug.Log("Spawn Boss!!!");
    yield return null;
  }

  public void StartLevel() {
    var children = new List<GameObject>();
    for (int i = 0; i < transform.childCount; i++)
      children.Add(transform.GetChild(i).gameObject);

    foreach (var child in children)
      Destroy(child);

    foreach (var spawn in spawns) {
      var spawner = PrefabUtility.InstantiatePrefab(spawn.prefab) as GameObject;
      spawner.transform.position = spawn.position;
      spawner.transform.rotation = spawn.rotation;
      spawner.transform.SetParent(transform);
    }
  }
}
