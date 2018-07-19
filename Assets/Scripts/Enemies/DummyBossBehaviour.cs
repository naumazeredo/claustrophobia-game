using System.Collections;
using UnityEngine;

public class DummyBossBehaviour : MonoBehaviour {
  public float moveAmplitude = 1.5f;
  public float moveDuration = 8f;
  public float attackInterval = 0.2f;
  public float waitTime = 2f;

  public Shooting shooting;

  bool moving;
  float startTime;

  Rigidbody2D rb;

  void Start () {
    rb = GetComponent<Rigidbody2D>();

    StartCoroutine(Behave());
  }

  void Update() {
    if (moving) {
      float deltaTime = Time.time - startTime;

      if (deltaTime > moveDuration) {
        deltaTime = moveDuration;
        moving = false;
      }

      Vector3 position = transform.position;
      position.x = Mathf.Sin(2 * Mathf.PI * deltaTime / moveDuration) * moveAmplitude;

      transform.position = position;
    }
  }

  void Move(bool state) {
    moving = state;
    if (moving) startTime = Time.time;
  }

  IEnumerator Behave() {
    // XXX: Temporary entrance! Refactor later
    rb.velocity = transform.up * 0.3f;
    yield return new WaitForSeconds(4f);
    rb.velocity = Vector2.zero;
    yield return new WaitForSeconds(1f);
    // ------------------------------

    while (true) {
      Move(true);

      for (float t = 0f; t < moveDuration; t += attackInterval) {
        shooting.Fire();
        yield return new WaitForSeconds(Mathf.Min(moveDuration - t, attackInterval));
      }

      yield return new WaitForSeconds(waitTime);

      Move(true);

      float interval = moveDuration/8f;
      for (float t = 0f; t < moveDuration; t += interval) {
        shooting.Fire();
        yield return new WaitForSeconds(Mathf.Min(moveDuration - t, interval));
      }

      yield return new WaitForSeconds(waitTime);
    }
  }
}
