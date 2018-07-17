using UnityEngine;

public class HullPushInteraction : MonoBehaviour {
  public float pushForce = 20f;

  bool pushed;

  Rigidbody2D rb;

  void Start () {
    rb = GetComponent<Rigidbody2D>();
  }

  void OnTriggerEnter2D(Collider2D col) {
    if (pushed)
      return;

    if (col.CompareTag("Hull")) {
      pushed = true;
      // TODO: reference since Start?
      col.gameObject.GetComponent<Rigidbody2D>().AddForce(rb.velocity.normalized * pushForce);
    }
  }
}
