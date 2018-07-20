using System;
using System.Collections;
using UnityEngine;

public class UsableHolder : MonoBehaviour {
	private GameController gameController;
	private GameObject usable;
	private int cooldown;
	private bool inCooldown;

	void Start() {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

	private void Update() {
		if (usable == null) return;
		if (!Input.GetKeyDown(KeyCode.Space) || inCooldown) return;

		usable.GetComponent<Usable>().Use();
		cooldown = gameController.GetEnemiesKilledCount() + usable.GetComponent<Usable>().cooldown;
		inCooldown = true;
	}

	public void Change(GameObject newUsable) {
		Destroy(usable);
		usable = newUsable;
		newUsable.transform.position = transform.position;
	}

	public void EnemyKillEvent() {
		if (gameController.GetEnemiesKilledCount() >= cooldown) {
			inCooldown = false;
			//TODO: Cooldown end animation
		}
	}

}
