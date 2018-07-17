using System.Collections;
using UnityEngine;

public class DummyEnemyAttack : MonoBehaviour {
  public float delay = 1f;
  public float startDelay = 2f;

  public Shooting shooting;

  void Start() {
    StartCoroutine(Attack());
  }

  IEnumerator Attack() {
    yield return new WaitForSeconds(startDelay);

    while (true) {
      shooting.Fire();

      yield return new WaitForSeconds(delay);
    }
  }
}
