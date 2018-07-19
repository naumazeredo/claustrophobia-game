﻿using System.Collections;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Spawn {
  public GameObject prefab;
  public Vector3 position;
  public Quaternion rotation;

  public Spawn(GameObject gameObject, Transform transform_) {
    Object obj = PrefabUtility.GetCorrespondingObjectFromSource(gameObject);
    prefab = (GameObject) AssetDatabase.LoadAssetAtPath(
      AssetDatabase.GetAssetPath(obj),
      typeof(GameObject)
    );

    position = transform_.localPosition;
    rotation = transform_.localRotation;
  }
}

public class Spawner : MonoBehaviour {
  public GameObject unit;

  void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("Bounds")) {
      StartCoroutine(Spawn());
    }
  }

  IEnumerator Spawn() {
    Instantiate(unit, transform.position, transform.rotation);

    GetComponent<SpriteRenderer>().enabled = false;
    Destroy(gameObject);
    yield return null;
  }

  public Spawn ToSpawn() {
    return new Spawn(gameObject, transform);
  }
}
