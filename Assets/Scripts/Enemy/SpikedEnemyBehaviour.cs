using System.Collections;
using UnityEngine;

public class SpikedEnemyBehaviour : MonoBehaviour {
  public float speed = 1f;
  public float moveTime = 2f;
  public float attackInterval = 1f;
  public float waitTime = 2f;

  public Shooting[] shootings;

  Rigidbody2D rb;

  void Start () {
    rb = GetComponent<Rigidbody2D>();

    StartCoroutine(Behave());
  }

  IEnumerator Behave() {
    while (true) {
      rb.velocity = transform.up * speed;
      yield return new WaitForSeconds(moveTime);

      rb.velocity = Vector2.zero;
      for (int i = 0; i < shootings.Length; i++) {
        shootings[i].Fire();
        if (i < shootings.Length-1)
          yield return new WaitForSeconds(attackInterval);
      }

      yield return new WaitForSeconds(waitTime);
    }
  }
}
