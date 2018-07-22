using System.Collections;
using UnityEngine;

[System.Serializable]
public class Spawn {
  public GameObject prefab;
  public Vector3 position;
  public Quaternion rotation;
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

    // TODO: implement delayed spawn for side spawners?

    GetComponent<SpriteRenderer>().enabled = false;
    gameObject.SetActive(false);
    yield return null;
  }
}
