using UnityEngine;

public class DestroyByBounds : MonoBehaviour {
  void OnTriggerExit2D(Collider2D col) {
    if (col.CompareTag("Bounds")) {
      Destroy(gameObject);
    }
  }
}
