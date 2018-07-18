using System.Collections;
using UnityEngine;

public class MailgunEnemyBehaviour : MonoBehaviour {
  public float speed = 1f;
  public float moveTime = 2f;
  public float attackInterval = 0.2f;
  public float waitTime = 2f;

  public Shooting[] shootings;

  Rigidbody2D rb;

  void Start () {
    rb = GetComponent<Rigidbody2D>();
    //rb.velocity = transform.up * speed;
    rb.velocity = Vector2.zero;

    StartCoroutine(Behave());
  }

  IEnumerator Behave() {
    while (true) {
      for (int i = 0; i < shootings.Length; i++) {
        shootings[i].Fire(attackInterval/8f);
        if (i < shootings.Length-1)
          yield return new WaitForSeconds(attackInterval);
      }
      //transform.Rotate(0,0,10, Space.Self);
      yield return new WaitForSeconds(waitTime);
    }
  }
}
