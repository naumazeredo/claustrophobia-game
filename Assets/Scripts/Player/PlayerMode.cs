using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour {

	public enum Mode {
		normal,
		dashing,
		piercing
	};

	public Mode mode = Mode.normal;
	public float dashIntensity;
	public float dashDrag;

	public GameObject normalBullet, piercingBullet;

	private Shooting shooting;
	private Vector2 dashDir;
	private Rigidbody2D rb;


	// Use this for initialization
	void Start() {
		shooting = GetComponentInChildren<Shooting>();
		dashDir = new Vector2(0, 1);
		rb = GetComponent<Rigidbody2D>();

		change(mode);
	}

	void Update() {
		if (mode == Mode.dashing) {
			if (rb.velocity.normalized.magnitude == 0.0f) {
				rb.drag = 0.0f;
				change(Mode.normal);
			}
		}
	}

	public void change(Mode newMode) {
		switch (newMode) {
			case Mode.dashing:
				shooting.attack = null;
				rb.velocity = dashDir * dashIntensity;
				rb.drag = dashDrag;
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

