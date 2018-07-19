using UnityEngine;

public class UnitHealth : MonoBehaviour {
  public int health = 1;

  /*
  void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("Hull")) return;
    if (col.CompareTag("Bounds")) return;
    if (col.CompareTag("Spawn")) return;

    TakeDamage();
  }
  */

  public void TakeDamage() {
    health--;

    if (health <= 0) {
      // XXX
      Destroy(gameObject);
    }
  }
}
