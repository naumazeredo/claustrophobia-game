using UnityEngine;

public class DestroyByContact : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D col)  {
    if (col.CompareTag("Hull") || col.CompareTag("Bounds"))
      return;

    Destroy(gameObject);

    if (col.CompareTag("Player")) {
      col.GetComponent<PlayerHealth>().TakeDamage();
      return;
    }

    // TODO: Call explosion
    Destroy(col.gameObject);
  }
}
