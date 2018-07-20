using UnityEngine;

public class UnitHealth : MonoBehaviour {
  public int health = 1;

  GameController gameController;

  void Start() {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
  }

  public void TakeDamage() {
    health--;

    if (health <= 0) {
      if (CompareTag("Enemy")) {
        gameController.AddEnemyDeath();
        gameController.AddEnemyKill(gameObject);
      }

      //gameObject.SetActive(false);
      Destroy(gameObject);
    }
  }
}
