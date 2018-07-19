using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed = 150f;
  public float hullPushForce = 10f;

  Rigidbody2D rb;
  Collider2D playerCollider;

  GameObject hull;
  Collider2D hullCollider;
  Rigidbody2D hullRigidbody;

  float inputX;
  float inputY;
  float angle;
  Vector2 input;
  Vector2 lastInput; // Used to avoid ghost key-ups and maintain rotation

  void Start () {
    rb = GetComponent<Rigidbody2D>();
    playerCollider = GetComponent<Collider2D>();

    hull = GameObject.FindWithTag("Hull");
    hullCollider = hull.GetComponent<Collider2D>();
    hullRigidbody = hull.GetComponent<Rigidbody2D>();
  }

  void Update () {
    inputX = Input.GetAxis("Horizontal");
    inputX = Mathf.Abs(inputX) < Mathf.Epsilon ? 0f : Mathf.Sign(inputX);

    inputY = Input.GetAxis("Vertical");
    inputY = Mathf.Abs(inputY) < Mathf.Epsilon ? 0f : Mathf.Sign(inputY);

    input = new Vector2(inputX, inputY);
    input.Normalize();

    // FIXME: Isso nao funciona se o objeto for ser empurrado por forcas externas...
    rb.velocity = input * speed;

    if (input != Vector2.zero && input == lastInput) {
      angle = -Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
      transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    lastInput = input;
  }

  public void SeparateCollider() {
    ColliderDistance2D colDistance = hullCollider.Distance(playerCollider);
    if (colDistance.isValid && colDistance.distance < 0f) {
      Vector3 delta = colDistance.distance * colDistance.normal;

      transform.position -= delta;
      rb.velocity = Vector2.zero;

      hullRigidbody.AddForce(hullPushForce * delta.normalized);
    }
  }

  void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("Hull")) {
      SeparateCollider();
    }
  }

  void OnTriggerStay2D(Collider2D col) {
    if (col.CompareTag("Hull")) {
      SeparateCollider();
    }
  }
}
