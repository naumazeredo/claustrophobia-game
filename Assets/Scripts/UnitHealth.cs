﻿using UnityEngine;

public class UnitHealth : MonoBehaviour {
  public int health = 1;
  public GameObject explosion;
  public bool isBoss;

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

      var drop = GetComponent<Drop>();
      if (drop != null)
        drop.DropItem(transform.position);

      //gameObject.SetActive(false);

      Destroy(gameObject);

      if (explosion)
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
  }
}
