using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {
	public enum EffectType { piercing }

	public EffectType effectType;
	private bool onHold;

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag != "Player" || onHold) return;
		onHold= true;

		GameObject.FindWithTag("UsableHolder").GetComponent<ItemHolder>().Change(gameObject);
		ActivateEffect();
	}

	private void ActivateEffect() {
		var mode = GameObject.FindWithTag("Player").GetComponent<PlayerMode>();

		switch (effectType) {
			case EffectType.piercing:
				mode.Change(PlayerMode.Mode.piercing);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public void RemoveEffect() {
		var mode = GameObject.FindWithTag("Player").GetComponent<PlayerMode>();

		mode.Change(PlayerMode.Mode.normal);
	}
}
