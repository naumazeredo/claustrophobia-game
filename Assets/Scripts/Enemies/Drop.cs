using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {
	public GameObject item;
	public int dropRate;

	public void DropItem(Vector2 position) {
		if (Random.Range(0, 100) <= dropRate)
			Instantiate(item, position, Quaternion.identity);
	}
}
