using UnityEngine;

public class DestroyByContact : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D col)  {

    if (col.CompareTag("Hull")) return;
    if (col.CompareTag("Bounds")) return;
    if (col.CompareTag("Spawn")) return;

    Destroy(gameObject);

    if (col.CompareTag("Player")) {
      col.GetComponent<PlayerHealth>().TakeDamage();
    } else if (col.CompareTag("Enemy")) {
      col.GetComponent<UnitHealth>().TakeDamage();
    } else {
      //Destroy(col.gameObject);
    }
  }
}
