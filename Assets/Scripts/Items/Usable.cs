using System;
using UnityEngine;

public class Usable : MonoBehaviour {
	public enum UsableType { dash, bleach }

	public UsableType usableType;
	public int cooldown;
	private bool onHold;
	private ItemHolder itemHolder;
	private Animator hullAnimator;

	void Start() {
		itemHolder = GameObject.FindWithTag("ItemHolder").GetComponent<ItemHolder>();
		hullAnimator = GameObject.FindWithTag("Hull").GetComponent<Animator>();
	}

	public void Use() {
		GameObject player = GameObject.FindWithTag("Player");

		switch (usableType) {
			case UsableType.dash:
				player.GetComponent<PlayerMode>().Change(PlayerMode.Mode.dashing);
				break;
			case UsableType.bleach:
				player.GetComponent<PlayerMode>().Change(PlayerMode.Mode.bleach);
        hullAnimator.SetTrigger("Expand");
				break;
		}

		itemHolder.StartUse();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag != "Player" || onHold) return;
		onHold= true;

		GameObject.FindWithTag("ItemHolder").GetComponent<ItemHolder>().Change(gameObject);
	}
}
