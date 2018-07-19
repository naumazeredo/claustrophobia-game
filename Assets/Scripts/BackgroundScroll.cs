using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
	public float scrollSpeed;
	public float tileSizeY;

	private Vector2 startPosition;

	void Start() {
		startPosition = (Vector2) transform.position;
	}

	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
		transform.position = startPosition + Vector2.up * newPosition;
	}
}
