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
	private Rigidbody2D rb, hullRb;

	private bool invencible;


	// Use this for initialization
	void Start() {
		shooting = GetComponentInChildren<Shooting>();
		dashDir = new Vector2(0, 1);
		rb = GetComponent<Rigidbody2D>();
		hullRb = GameObject.FindWithTag("Hull").GetComponent<Rigidbody2D>();

		Change(mode);
	}

	void Update() {
		if (mode == Mode.dashing) {
			if (rb.velocity.magnitude < 0.1f) {
				rb.drag = 0.0f;
				hullRb.velocity = Vector2.zero;

				invencible = false;
				Change(Mode.normal);
			}
		}
	}

	public void Change(Mode newMode) {
		switch (newMode) {
			case Mode.dashing:
				invencible = true;
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

	public bool IsInvencible() {
		return invencible;
	}
}

