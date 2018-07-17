using UnityEngine;

public class DestroyByContact : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D col)  {
    if (col.CompareTag("Hull"))
      return;

    Destroy(gameObject);

    if (col.CompareTag("Player")) {
      Debug.Log("Damage taken!");
      return;
    }

    // TODO: Call explosion
    Destroy(col.gameObject);
  }
}
