using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour {
  public GameObject attack;
  public Transform[] shootLocations;

  public void Fire() {
    foreach (var loc in shootLocations) {
      Instantiate(attack, loc.position, loc.rotation);
    }
  }

  public void Fire(float interval) {
    StartCoroutine(FireStride(interval));
  }

  IEnumerator FireStride(float interval) {
    foreach (var loc in shootLocations) {
      Instantiate(attack, loc.position, loc.rotation);
      yield return new WaitForSeconds(interval);
    }
  }
}
