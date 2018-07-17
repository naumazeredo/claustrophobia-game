using UnityEngine;

public class EnemyAttack : MonoBehaviour {
  public float fireRate = 1f;

  bool fired;
  float fireDelay;

  UnitShoot unitShoot;

  void Start () {
    unitShoot = GetComponent<UnitShoot>();
  }

  void Update () {
    if (fired) {
      fireDelay += Time.deltaTime;
      if (fireDelay >= fireRate) {
        fired = false;
        fireDelay = 0f;
      }
    }
    if (!fired)
      Fire();
  }

  void Fire() {
    unitShoot.Shoot();

    fired = true;
  }
}
