using System.Collections;
using UnityEngine;

public class HullDash : MonoBehaviour {
	public float damageRate;

	private GameObject player;
	private PlayerMode playerMode;
	private bool damageGiven;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
    playerMode = player.GetComponent<PlayerMode>();
	}
	
	void OnTriggerStay2D(Collider2D col) {
		if (playerMode.mode != PlayerMode.Mode.dashing || damageGiven)
			return;

		UnitHealth health = col.GetComponent<UnitHealth>();
		if (health != null) {
			StartCoroutine(GiveDamage(health));
		}
	}

	IEnumerator GiveDamage(UnitHealth unitHealth) {
		damageGiven = true;
    unitHealth.TakeDamage();
		yield return new WaitForSeconds(damageRate);
		damageGiven = false;
	}
}
