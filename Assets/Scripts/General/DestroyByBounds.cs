using UnityEngine;

public class DestroyByBounds : MonoBehaviour {
  void OnTriggerExit2D(Collider2D col) {
    Debug.Log(col.name + " : " + name);
    if (col.CompareTag("Bounds")) {
      Destroy(gameObject);
    }
  }
}
