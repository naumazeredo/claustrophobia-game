using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed = 150f;
  public float hullPushForce = 10f;

  private GameController gameController;

  Rigidbody2D rb, hullRb;

  GameObject hull;
  CircleCollider2D hullCollider;
  private PlayerMode playerMode;

  float inputX;
  float inputY;
  Vector2 input;

  Animator animator;

  void Start () {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

    playerMode = GetComponent<PlayerMode>();
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    hull = GameObject.FindWithTag("Hull");
    hullRb = GetComponent<Rigidbody2D>();
    hullCollider = hull.GetComponent<CircleCollider2D>();
  }

  void Update () {
    if (GetComponent<PlayerMode>().mode == PlayerMode.Mode.dashing) return;

    KeyMapper keyMapper = gameController.keyMapper;

    inputX = Input.GetAxis(keyMapper.GetHorizontalMov());
    inputX = Mathf.Abs(inputX) < Mathf.Epsilon ? 0f : Mathf.Sign(inputX);

    inputY = Input.GetAxis(keyMapper.GetVerticalMov());
    inputY = Mathf.Abs(inputY) < Mathf.Epsilon ? 0f : Mathf.Sign(inputY);

    input = new Vector2(inputX, inputY);
    input.Normalize();

    rb.velocity = input * speed;

    animator.SetFloat("InputX", inputX);
  }

  void LateUpdate() {
    var rad = hullCollider.radius*hull.transform.localScale.x - GetComponent<CircleCollider2D>().radius;
    var center = (Vector2)hull.transform.position;
    var dist = center - (Vector2)transform.position;

    if (!(dist.magnitude > rad)) return;

    var delta = dist.normalized * (dist.magnitude - rad);
    var deltaV3 = new Vector3(delta.x, delta.y, 0);

    if (playerMode.mode == PlayerMode.Mode.dashing) {
      hull.transform.position -= deltaV3;
      hullRb.velocity = rb.velocity;
    }
    else {
      transform.position += deltaV3;
      hull.GetComponent<Rigidbody2D>().AddForce(-hullPushForce * dist.normalized);
    }
  }
}
