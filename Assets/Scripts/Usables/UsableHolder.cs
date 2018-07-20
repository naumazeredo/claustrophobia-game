using UnityEngine;

public class UsableHolder : MonoBehaviour {
	private GameObject usable;

	private void Update() {
		if (usable == null) return;
		if (Input.GetKeyDown(KeyCode.Space)) {
			usable.GetComponent<Usable>().use();
			Destroy(usable);
		}
	}

	public void change(GameObject newUsable) {
		Destroy(usable);
		usable = newUsable;
		newUsable.transform.position = transform.position;
	}

}
