using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed = 150f;
  public float circlePushForce = 10f;

  Rigidbody2D rb;

  GameObject circle;
  CircleCollider2D circleCollider;
  Rigidbody2D circleRigidbody;

  float inputX;
  float inputY;
  float angle;

  void Start () {
    rb = GetComponent<Rigidbody2D>();

    circle = GameObject.FindWithTag("Circle");
    circleCollider = circle.GetComponent<CircleCollider2D>();
    circleRigidbody = circle.GetComponent<Rigidbody2D>();
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

  void FixedUpdate() {
    Vector3 circlePlayerDist = transform.position - circle.transform.position;
    if (circlePlayerDist.magnitude > circleCollider.radius) {
      circlePlayerDist = circlePlayerDist.normalized * circleCollider.radius;
      transform.position = circle.transform.position + circlePlayerDist;

      circleRigidbody.AddForce(circlePushForce * circlePlayerDist.normalized);
    }
  }
}
