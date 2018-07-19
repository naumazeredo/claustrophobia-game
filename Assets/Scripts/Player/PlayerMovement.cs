using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed = 150f;
  public float hullPushForce = 10f;

  Rigidbody2D rb;
  Collider2D playerCollider;

  GameObject hull;
  CircleCollider2D hullCollider;

  float inputX;
  float inputY;
  float angle;
  Vector2 input;
  Vector2 lastInput; // Used to avoid ghost key-ups and maintain rotation

  Animator animator;

  void Start () {
    rb = GetComponent<Rigidbody2D>();
    playerCollider = GetComponent<Collider2D>();
    animator = gameObject.GetComponent<Animator>();

    hull = GameObject.FindWithTag("Hull");
    hullCollider = hull.GetComponent<CircleCollider2D>();
  }

  void Update () {
    inputX = Input.GetAxis("Horizontal");
    inputX = Mathf.Abs(inputX) < Mathf.Epsilon ? 0f : Mathf.Sign(inputX);

    inputY = Input.GetAxis("Vertical");
    inputY = Mathf.Abs(inputY) < Mathf.Epsilon ? 0f : Mathf.Sign(inputY);

    input = new Vector2(inputX, inputY);
    input.Normalize();
    animator.SetFloat("speedx", inputX);

    // FIXME: Isso nao funciona se o objeto for ser empurrado por forcas externas...
    rb.velocity = input * speed;

    if (input != Vector2.zero && input == lastInput) {
      angle = -Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
      //transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    lastInput = input;
  }

  public void FixedUpdate() {
    var rad = hullCollider.radius*hull.transform.localScale.x - GetComponent<CircleCollider2D>().radius;
    var center = (Vector2)hull.transform.position;

    ColliderDistance2D colDistance = hullCollider.Distance(playerCollider);
    Debug.Log(colDistance.distance);

    var dist = center - (Vector2)transform.position;
    if (dist.magnitude > rad) {
      var delta = dist.normalized * (dist.magnitude - rad);
      var deltaV3 = new Vector3(delta.x, delta.y, 0);
      transform.position += deltaV3;

      hull.GetComponent<Rigidbody2D>().AddForce(-hullPushForce * dist.normalized);
    }
  }
}
