using UnityEngine;

public class DestroyByBounds : MonoBehaviour {
  GameController gameController;

  void Start() {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
  }

  void OnTriggerExit2D(Collider2D col) {
    if (col.CompareTag("Bounds")) {
      if (CompareTag("Enemy")) {
        gameController.AddEnemyDeath();
      }

      Destroy(gameObject);
    }
  }
}
