using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed = 150f;
  public float hullPushForce = 10f;

  private GameCoordinator gameCoordinator;

  Rigidbody2D rb;
  Collider2D playerCollider;

  GameObject hull;
  CircleCollider2D hullCollider;

  float inputX;
  float inputY;
  Vector2 input;

  Animator animator;

  void Start () {
    gameCoordinator = GameObject.FindWithTag("GameCoordinator").GetComponent<GameCoordinator>();

    rb = GetComponent<Rigidbody2D>();
    playerCollider = GetComponent<Collider2D>();
    animator = GetComponent<Animator>();

    hull = GameObject.FindWithTag("Hull");
    hullCollider = hull.GetComponent<CircleCollider2D>();
  }

  void Update () {
    KeyMapper keyMapper = gameCoordinator.keyMapper;

    inputX = Input.GetAxis(keyMapper.getHorizontalMov());
    inputX = Mathf.Abs(inputX) < Mathf.Epsilon ? 0f : Mathf.Sign(inputX);

    inputY = Input.GetAxis(keyMapper.getVerticalMov());
    inputY = Mathf.Abs(inputY) < Mathf.Epsilon ? 0f : Mathf.Sign(inputY);

    input = new Vector2(inputX, inputY);
    input.Normalize();

    rb.velocity = input * speed;

    animator.SetFloat("InputX", inputX);
  }

  public void FixedUpdate() {
    var rad = hullCollider.radius*hull.transform.localScale.x - GetComponent<CircleCollider2D>().radius;
    var center = (Vector2)hull.transform.position;

    ColliderDistance2D colDistance = hullCollider.Distance(playerCollider);

    var dist = center - (Vector2)transform.position;
    if (dist.magnitude > rad) {
      var delta = dist.normalized * (dist.magnitude - rad);
      var deltaV3 = new Vector3(delta.x, delta.y, 0);
      transform.position += deltaV3;

      hull.GetComponent<Rigidbody2D>().AddForce(-hullPushForce * dist.normalized);
    }
  }
}
