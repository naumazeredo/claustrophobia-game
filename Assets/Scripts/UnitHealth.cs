using System.Collections;
using System.Linq;
using UnityEngine;

public class UnitHealth : MonoBehaviour {
  public int health = 1;
  public GameObject damageExplosion;
  public GameObject explosion;
  public bool isBoss;

  GameController gameController;

  void Start() {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
  }

  public void TakeDamage(bool destroy = false) {
    Debug.Log("Damage taken: " + name + " : " + health);
    health--;
    if (destroy) health = 0;

    if (damageExplosion)
      Instantiate(damageExplosion, transform.position, Quaternion.identity);

    if (health <= 0) {
      if (CompareTag("Enemy")) {
        if (isBoss) {
          StartCoroutine(DelayedDeath());
        } else {
          gameController.AddEnemyDeath();
          gameController.AddEnemyKill(gameObject);
        }
      }

      var drop = GetComponent<Drop>();
      if (drop != null)
        drop.DropItem(transform.position);

      if (!isBoss)
        Destroy(gameObject);

      if (explosion)
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
  }

  IEnumerator DelayedDeath() {
    GetComponents<MonoBehaviour>()
      .ToList()
      .ForEach(c => c.enabled = false);
    GetComponent<SpriteRenderer>().enabled = true;

    yield return new WaitForSeconds(5f);

    Destroy(gameObject);
    gameController.AddEnemyDeath();
    gameController.AddEnemyKill(gameObject);
  }
}
