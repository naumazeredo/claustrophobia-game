using UnityEngine;

// TODO: Use object pool?
public class DestroyByTime : MonoBehaviour {
  public float countdown = 2f;

  void Start () {
    Destroy(gameObject, countdown);
  }
}
