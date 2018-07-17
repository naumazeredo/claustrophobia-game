using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
  public float fireDelay = 0.2f;
  public Shooting basicShooting;

  bool fired;

  void Start () {
  }

  void Update () {
    if (Input.GetButton("Fire") && !fired) {
      Attack();
    }
  }

  IEnumerator Reload() {
    yield return new WaitForSeconds(fireDelay);
    fired = false;
    yield return null;
  }

  void Attack() {
    basicShooting.Fire();

    fired = true;
    StartCoroutine(Reload());
  }
}
