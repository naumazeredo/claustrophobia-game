using UnityEngine;

public class CreateInsideBleach : MonoBehaviour {
  public bool justCreated;

  void Start () {
    justCreated = true;
  }

  void Update() {
    justCreated = false;
  }
}
