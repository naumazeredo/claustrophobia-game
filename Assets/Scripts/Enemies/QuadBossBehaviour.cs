using System.Collections;
using UnityEngine;

public class QuadBossBehaviour : MonoBehaviour {
  public float cycleDuration = 5f;
  public float attackInterval = 0.2f;

  public Shooting shooting;

  int direction = 1;

  Rigidbody2D rb;

  IEnumerator coroutine;
  bool coroutinePlaying;

  void Start () {
    rb = GetComponent<Rigidbody2D>();

    coroutine = Behave();
    StartCoroutine(coroutine);
    coroutinePlaying = true;
  }

  void OnDisable() {
    if (coroutinePlaying) {
      StopCoroutine(coroutine);
      coroutinePlaying = false;
    }
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

      int attacksPerCycle = (int)(cycleDuration / attackInterval);
      for (int i = 0; i < attacksPerCycle; i++) {
        shooting.Fire();
        yield return new WaitForSeconds(attackInterval);
      }

      Invert();
    }
  }
}
