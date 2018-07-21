using System;
using UnityEngine;

public class Usable : MonoBehaviour {
	public enum UsableType { dash }

	public UsableType usableType;
	public int cooldown;
	private bool onHold;

	public void Use() {
		GameObject player = GameObject.FindWithTag("Player");

		switch (usableType) {
			case UsableType.dash:
				player.GetComponent<PlayerMode>().Change(PlayerMode.Mode.dashing);
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag != "Player" || onHold) return;
		onHold= true;

		GameObject.FindWithTag("UsableHolder").GetComponent<ItemHolder>().Change(gameObject);
	}
}
