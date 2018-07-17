using UnityEngine;

public class Shooting : MonoBehaviour {
  public GameObject attack;
  public Transform[] shootLocations;

  public void Fire() {
    foreach (var loc in shootLocations) {
      Instantiate(attack, loc.position, loc.rotation);
    }
  }
}
