using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
  public float fireDelay = 0.2f;

  public Shooting Shooting;

  GameController gameController;

  bool fired;

  float inputX, inputY;
  Vector2 input;

  void Start () {
    gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
  }

  void Update () {
    if (!fired) {
      KeyMapper keyMapper = gameController.keyMapper;
      inputX = Input.GetAxis(keyMapper.GetHorizontalFire());
      inputX = Mathf.Abs(inputX) < Mathf.Epsilon ? 0f : Mathf.Sign(inputX);

      inputY = Input.GetAxis(keyMapper.GetVerticalFire());
      inputY = Mathf.Abs(inputY) < Mathf.Epsilon ? 0f : Mathf.Sign(inputY);

      input = new Vector2(inputX, inputY);
      input.Normalize();

      if (input != Vector2.zero) {
        Attack(input);
      }
    }
  }

  IEnumerator Reload() {
    fired = true;
    yield return new WaitForSeconds(fireDelay);
    fired = false;
    yield return null;
  }

  void Attack(Vector2 direction) {
    Shooting.Fire(direction);
    StartCoroutine(Reload());
  }
}
