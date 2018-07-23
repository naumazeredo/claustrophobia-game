using UnityEngine;
using UnityEngineInternal;

public class ItemHolder : MonoBehaviour {
	private GameController gameController;
	private GameObject item;
	private GameObject nextItem;
	private Animator hullAnimator;
	private int cooldown;

	private bool inUse;
  private AudioSource source;
	private bool inCooldown;

	void Start() {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		hullAnimator = GameObject.FindWithTag("Hull").GetComponent<Animator>();
    source = GetComponent<AudioSource>();
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

	public void Change(GameObject newItem) {
    source.Play();
		nextItem = newItem;
		newItem.active = false;
		ChangeAfterUse();
	}

	public void ChangeAfterUse() {
		if (inUse || nextItem == null) return;

		if (item != null) {
			var effect = item.GetComponent<Effect>();
			if (effect != null) effect.RemoveEffect();
			Destroy(item);
		}

		item = nextItem;
		item.active = true;
		nextItem = null;

		item.transform.position = transform.position;
		item.GetComponent<ForwardMover>().speed = 0;
		ResetCooldown();

		var sprite = item.GetComponent<SpriteRenderer>();
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
		hullAnimator.SetTrigger("Shine");
	}

	public void StartUse() {
		inUse = true;
	}

	public void EndUse() {
		inUse = false;
		ChangeAfterUse();
	}

}
