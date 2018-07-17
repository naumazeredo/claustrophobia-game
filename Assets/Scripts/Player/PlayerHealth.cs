﻿using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  public int totalHealth = 5;
  public float invincibleTime = 1f;

  // TODO: create animation for blinking?
  public float invincibleBlinkTime = 0.1f;
  SpriteRenderer spriteRenderer;

  int currentHealth;
  bool invincible;

  Transform hullTransform;

  void Start () {
    currentHealth = totalHealth;

    spriteRenderer = GetComponent<SpriteRenderer>();
    hullTransform = GameObject.FindWithTag("Hull").transform;
  }

  public void TakeDamage() {
    if (invincible)
      return;

    Debug.Log("Damage taken!");

    currentHealth--;
    if (currentHealth <= 0) {
      Debug.Log("You are DEAD!");
      return;
    }

    // TODO: Use predefined values?
    // TODO: Smooth transition?
    float newScale = hullTransform.localScale.x - 0.1f;
    hullTransform.localScale = new Vector3(newScale, newScale, 1);

    // TODO: Move hull collider to child, change child scale and change hull sprite!

    StartCoroutine(Invincibility());
  }

  IEnumerator Invincibility() {
    /*
    invincible = true;
    yield return new WaitForSeconds(invincibleTime);
    invincible = false;
    yield return null;
    */

    invincible = true;

    float totalTime = 0f;
    while (totalTime < invincibleTime) {
      spriteRenderer.enabled = !spriteRenderer.enabled;
      yield return new WaitForSeconds(invincibleBlinkTime);
      totalTime += invincibleBlinkTime;
    }

    spriteRenderer.enabled = true;
    invincible = false;

    yield return null;
  }
}