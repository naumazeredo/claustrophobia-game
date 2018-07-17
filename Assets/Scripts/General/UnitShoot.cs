using UnityEngine;

public class UnitShoot : MonoBehaviour {
  public GameObject attack;
  public Transform shootLocation;

  public void Shoot() {
    Instantiate(attack, shootLocation.position, transform.rotation);
  }
}
