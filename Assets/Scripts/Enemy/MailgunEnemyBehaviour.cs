using System.Collections;
using UnityEngine;

public class MailgunEnemyBehaviour : MonoBehaviour {
  public float speed = 1f;
  public float moveTime = 2f;
  public float attackInterval = 0.2f;
  public float waitTime = 2f;

  public Shooting[] shootings;

  void Start () {
    StartCoroutine(Behave());
  }

  IEnumerator Behave() {
    while (true) {
      for (int i = 0; i < shootings.Length; i++) {
        shootings[i].Fire(attackInterval);
        yield return new WaitForSeconds(attackInterval);
        shootings[i].Fire(attackInterval, true);
        yield return new WaitForSeconds(attackInterval);

        /*
        if (i < shootings.Length-1)
          yield return new WaitForSeconds(attackInterval);
          */
      }

      //yield return new WaitForSeconds(waitTime);
    }
  }
}
