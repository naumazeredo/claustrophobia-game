using UnityEngine;

public class BoundsLimit : MonoBehaviour {
  public Vector2 bounds;
  public float initialForce = 20f;
  public float proportionalForce = 20f;

  SpriteRenderer spriteRenderer;
  Rigidbody2D rb;
  private PlayerMode playerMode;

  void Start () {
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    playerMode = GameObject.FindWithTag("Player").GetComponent<PlayerMode>();
  }

  void FixedUpdate() {
    Vector2 spriteBounds = spriteRenderer.bounds.max - transform.position;

    Vector3 newPosition = new Vector3(
      Mathf.Clamp(transform.position.x, - bounds.x + spriteBounds.x, bounds.x - spriteBounds.x),
      Mathf.Clamp(transform.position.y, - bounds.y + spriteBounds.y, bounds.y - spriteBounds.y),
      0f
    );

    if (transform.position != newPosition && playerMode.mode != PlayerMode.Mode.bleach) {
      Vector2 delta = newPosition - transform.position;
      rb.AddForce(delta * (initialForce + proportionalForce * delta.magnitude));
    }
  }
}