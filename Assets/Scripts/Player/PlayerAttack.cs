using UnityEngine;

public class PlayerAttack : MonoBehaviour {
  public float fireRate = 0.5f;

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

    if (Input.GetButton("Fire") && !fired)
      Fire();
  }

  void Fire() {
    unitShoot.Shoot();

    fired = true;
  }
}
