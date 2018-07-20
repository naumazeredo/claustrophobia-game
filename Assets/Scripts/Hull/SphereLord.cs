using System.Collections;
using UnityEngine;

public class SphereLord : MonoBehaviour {
	public float damageHate;

	private GameObject player;
	private bool damageGiven;

	// Use this for initialization
	void Start () {
    player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player.GetComponent<PlayerMode>().mode == PlayerMode.Mode.dashing)
			transform.position = player.transform.position;
	}

	void OnTriggerStay2D(Collider2D col) {
		if (player.GetComponent<PlayerMode>().mode != PlayerMode.Mode.dashing || damageGiven)
			return;

		UnitHealth health = col.GetComponent<UnitHealth>();
		if (health != null) {
			StartCoroutine(GiveDamage(health));
		}
	}

	IEnumerator GiveDamage(UnitHealth unitHealth) {
		damageGiven = true;
    unitHealth.TakeDamage();
		yield return new WaitForSeconds(damageHate);
		damageGiven = false;
	}
}
