using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMode : MonoBehaviour {

	public enum Mode {
		normal,
		dashing,
		piercing,
		bleach
	};

	public Mode mode = Mode.normal;
	public float dashIntensity;
	public float dashDrag;

	public GameObject normalBullet, piercingBullet;

	private Shooting shooting;
	private Vector2 dashDir;
	private Rigidbody2D rb, hullRb;
	private GameObject player;
	private PlayerMovement playerMoviment;
	private ItemHolder itemHolder;

	private bool invencible;


	// Use this for initialization
	void Start() {
		shooting = GetComponentInChildren<Shooting>();
		rb = GetComponent<Rigidbody2D>();
		hullRb = GameObject.FindWithTag("Hull").GetComponent<Rigidbody2D>();
		itemHolder = GameObject.FindWithTag("ItemHolder").GetComponent<ItemHolder>();

		player = GameObject.FindWithTag("Player");
		playerMoviment = player.GetComponent<PlayerMovement>();

		Change(mode);
	}

	void Update() {
		if (mode == Mode.dashing) {
			if (rb.velocity.magnitude < 0.1f) {
				rb.drag = 0.0f;
				hullRb.velocity = Vector2.zero;
				invencible = false;
				itemHolder.EndUse();
				Change(Mode.normal);
			}
		}
	}

	public void Change(Mode newMode) {
		invencible = false;
		switch (newMode) {
			case Mode.dashing:
				dashDir = playerMoviment.GetInput();
				if(dashDir.magnitude == 0) dashDir = Vector2.up;

				invencible = true;
				shooting.attack = null;
				rb.velocity = dashDir * dashIntensity;
				rb.drag = dashDrag;
				break;
			case Mode.piercing:
				shooting.attack = piercingBullet;
				break;
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

