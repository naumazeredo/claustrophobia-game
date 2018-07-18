using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
  public GameObject enemy;
  public float delay;

  void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("Bounds")) {
      StartCoroutine(Spawn());
    }
  }

  IEnumerator Spawn() {
    GetComponent<SpriteRenderer>().enabled = false;
    yield return new WaitForSeconds(delay);
    Instantiate(enemy, transform.position, transform.rotation);
  }
}
