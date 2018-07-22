using System.Collections;
using UnityEditor;
using UnityEngine;

public class HullMode : MonoBehaviour {
	public float damageRate = 0;
	public float hullBleachSpeed = 1;
	public float hullBleachMax = 20;

	private GameObject player;
	private PlayerMode playerMode;
	private bool damageGiven;

	private Vector3 baseHullSize;
	private ParticleSystem.ShapeModule shape;
	private bool increasingHull = false;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
    playerMode = player.GetComponent<PlayerMode>();
		shape = transform.GetChild(0).GetComponent<ParticleSystem>().shape;
	}
	
	void OnTriggerStay2D(Collider2D col) {
		if (playerMode.mode == PlayerMode.Mode.dashing && !damageGiven) {
			UnitHealth health = col.GetComponent<UnitHealth>();
			if (health != null) {
				StartCoroutine(GiveDamage(health));
			}
		} else if (playerMode.mode == PlayerMode.Mode.bleach && col.tag == "Bullet") {
			col.GetComponent<UnitHealth>().TakeDamage(true);
		}
	}

	void FixedUpdate() {
		if (playerMode.mode == PlayerMode.Mode.bleach) {
			if (!increasingHull) {
				increasingHull = true;
        baseHullSize = transform.localScale;
			}
			if (transform.localScale.magnitude >= hullBleachMax * baseHullSize.magnitude) {
				playerMode.Change(PlayerMode.Mode.normal);
				increasingHull = false;
				transform.position = player.transform.position;
				transform.localScale = baseHullSize;
			}
			else {
				transform.localScale = transform.localScale * (1 + hullBleachSpeed * 0.01f);
				shape.radius = transform.localScale.x / 2f;
			}
		}
	}

	IEnumerator GiveDamage(UnitHealth unitHealth) {
		damageGiven = true;
    unitHealth.TakeDamage();
		yield return new WaitForSeconds(damageRate);
		damageGiven = false;
	}
}
