using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed = 150f;
  public float hullPushForce = 10f;

  Rigidbody2D rb;
  Collider2D playerCollider;

  GameObject hull;
  Rigidbody2D hullRigidbody;

  float inputX;
  float inputY;
  float angle;

  void Start () {
    rb = GetComponent<Rigidbody2D>();
    playerCollider = GetComponent<Collider2D>();

    hull = GameObject.FindWithTag("Hull");
    hullRigidbody = hull.GetComponent<Rigidbody2D>();
  }

  void Update () {
    inputX = Input.GetAxis("Horizontal");
    inputY = Input.GetAxis("Vertical");

    // FIXME: Isso nao funciona se o objeto for ser empurrado por forcas externas...
    Vector2 velocity = new Vector2(inputX, inputY) * speed;
    rb.velocity = velocity;

    // TODO: rotation speed?
    if (Mathf.Max(Mathf.Abs(inputX), Mathf.Abs(inputY)) > 0.01f) {
      angle = -Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
      transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
  }

  void SeparateCollider(Collider2D col) {
    ColliderDistance2D colDistance = col.Distance(playerCollider);
    if (colDistance.isValid && colDistance.distance < 0f) {
      Vector3 delta = colDistance.distance * colDistance.normal;

      transform.position -= delta;
      rb.velocity = Vector2.zero;

      hullRigidbody.AddForce(hullPushForce * delta.normalized);
    }
  }

  void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("Hull")) {
      SeparateCollider(col);
    }
  }

  void OnTriggerStay2D(Collider2D col) {
    if (col.CompareTag("Hull")) {
      SeparateCollider(col);
    }
  }
}
