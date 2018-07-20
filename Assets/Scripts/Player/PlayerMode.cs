using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour {

	public enum Mode{normal, sphereLord, piercing};
	public Mode mode = Mode.normal;

	public GameObject normalBullet, piercingBullet;

	// Use this for initialization
	void Start () {
		change(mode);
	}

	public void change(Mode newMode) {
		Shooting shooting = GetComponentInChildren<Shooting>();
		switch (newMode) {
			case Mode.sphereLord:
        shooting.attack = null;
				break;
			case Mode.piercing:
				shooting.attack = piercingBullet;
				break;
			case Mode.normal:
			default:
        shooting.attack = normalBullet;
				break;
		}

		mode = newMode;
	}
}
