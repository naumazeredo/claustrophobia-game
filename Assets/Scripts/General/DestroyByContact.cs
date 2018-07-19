using UnityEngine;


public class DestroyByContact : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D col)  {

    if (col.CompareTag("Hull")) return;
    if (col.CompareTag("Bounds")) return;
    if (col.CompareTag("Spawn")) return;

    if (col.CompareTag("Player")) {
      col.GetComponent<PlayerHealth>().TakeDamage();
    } else {
      var Health = col.GetComponent<UnitHealth>();
      if(Health != null)
        Health.TakeDamage();
    }
  }
}
