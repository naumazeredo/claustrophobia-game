using System;
using UnityEngine;

public class Usable : MonoBehaviour {
	public enum UsableType { dash }

	public UsableType usableType;
	private bool onHold;

	public void use() {
		GameObject player = GameObject.FindWithTag("Player");

		switch (usableType) {
			case UsableType.dash:
				player.GetComponent<PlayerMode>().change(PlayerMode.Mode.dashing);
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag != "Player" || onHold) return;
		onHold= true;

		GameObject.FindWithTag("UsableHolder").GetComponent<UsableHolder>().change(gameObject);
	}
}
