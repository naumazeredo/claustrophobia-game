using UnityEditor;
using UnityEngine;

public class ItemHolder : MonoBehaviour {
	private GameController gameController;
	private GameObject item;
	private int cooldown;
	private bool inCooldown;
	private Animator hullAnimator;

	void Start() {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		hullAnimator = GameObject.FindWithTag("Hull").GetComponent<Animator>();
	}

	private void Update() {
		if (item == null) return;
		if (!Input.GetKeyDown(KeyCode.Space) || inCooldown) return;
		var usable = item.GetComponent<Usable>();

		if (usable == null) return;
		usable.Use();
		cooldown = gameController.GetEnemiesKilledCount() + item.GetComponent<Usable>().cooldown;
		inCooldown = true;
	}

	public void Change(GameObject newUsable) {
		if (item != null) {
			var effect = item.GetComponent<Effect>();
			if (effect != null) effect.RemoveEffect();
			Destroy(item);
		}

		item = newUsable;
		newUsable.transform.position = transform.position;
		newUsable.GetComponent<ForwardMover>().speed = 0;

		var sprite = newUsable.GetComponent<SpriteRenderer>();
		sprite.sortingLayerName = "UI";
	}

	public void EnemyKillEvent() {
		if (inCooldown && gameController.GetEnemiesKilledCount() >= cooldown) {
			inCooldown = false;
			hullAnimator.SetTrigger("Shine");
		}
	}

	public void ResetCooldown() {
		inCooldown = false;
	}

}
