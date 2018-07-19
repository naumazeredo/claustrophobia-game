using UnityEngine;

public class ForwardMover : MonoBehaviour {
  public float speed = 5f;

  Rigidbody2D rb;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
  }
  void Update() {
    rb.velocity = speed * transform.up;
  }
}
