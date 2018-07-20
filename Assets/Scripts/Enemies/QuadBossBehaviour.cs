using System.Collections;
using UnityEngine;

public class QuadBossBehaviour : MonoBehaviour {
  public float cycleDuration = 10f;
  public float attackInterval = 0.4f;

  public Shooting shooting;

  int direction = 1;

  Rigidbody2D rb;

  void Start () {
    rb = GetComponent<Rigidbody2D>();

    StartCoroutine(Behave());
  }

  void Rotate() {
    rb.angularVelocity = direction * 360f / cycleDuration;
  }

  void Invert() {
    direction = -direction;
  }

  IEnumerator Behave() {
    while (true) {
      Rotate();

      for (float t = 0f; t < cycleDuration; t += attackInterval) {
        shooting.Fire();
        yield return new WaitForSeconds(Mathf.Min(cycleDuration - t, attackInterval));
      }

      Invert();
    }
  }
}
