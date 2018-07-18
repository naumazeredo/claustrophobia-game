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

  public void Fire(float duration, bool reverse = false) {
    StartCoroutine(FireStride(duration / (shootLocations.Length + 1), reverse));
  }

  IEnumerator FireStride(float interval, bool reverse) {
    int delta = 1, start = 0, end = shootLocations.Length;
    if (reverse) {
      delta = -1;
      start = end-1;
      end = -1;
    }

    for (int i = start; i != end; i += delta) {
      Instantiate(attack, shootLocations[i].position, shootLocations[i].rotation);
      yield return new WaitForSeconds(interval);
    }
  }
}
