using UnityEngine;

public class DestroyByContact : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D col)  {
    if (col.CompareTag("Hull"))
      return;

    if (col.CompareTag("Player")) {
      Debug.Log("Damage taken!");
    }

    // TODO: Call explosion
    Destroy(col.gameObject);
  }
}
