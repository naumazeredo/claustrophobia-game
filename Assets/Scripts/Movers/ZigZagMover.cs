using UnityEngine;
using System.Collections;

public class ZigZagMover : MonoBehaviour {
  public float speedx = 2f;
  public float speedy = 2f;
  public float tilt = 45f;
  public float tiltInterval = 1f;

  Rigidbody2D rb;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    rb.velocity = new Vector2(speedx*Mathf.Sin(Mathf.Deg2Rad*tilt), -speedy*Mathf.Cos(Mathf.Deg2Rad*tilt));
    StartCoroutine(ZigZag());
  }

  IEnumerator ZigZag() {
    while (true) {
      rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
      yield return new WaitForSeconds(tiltInterval);
    }
  }
}
