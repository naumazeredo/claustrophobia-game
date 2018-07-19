using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
  public float fireDelay = 0.2f;
  public Shooting basicShooting;

  private GameCoordinator gameCoordinator;

  public bool boiStyle;
  public Shooting boiShooting;


  bool fired;

  float inputX, inputY;
  Vector2 input;

  void Start () {
    gameCoordinator = GameObject.FindWithTag("GameCoordinator").GetComponent<GameCoordinator>();
  }

  void Update () {
    if (!fired) {
      if (!boiStyle) {
        if (Input.GetButton("Fire")) {
          Attack();
        }
      } else {
        KeyMapper keyMapper = gameCoordinator.keyMapper;
        inputX = Input.GetAxis(keyMapper.getHorizontalFire());
        inputX = Mathf.Abs(inputX) < Mathf.Epsilon ? 0f : Mathf.Sign(inputX);

        inputY = Input.GetAxis(keyMapper.getVerticalFire());
        inputY = Mathf.Abs(inputY) < Mathf.Epsilon ? 0f : Mathf.Sign(inputY);

        input = new Vector2(inputX, inputY);
        input.Normalize();

        if (input != Vector2.zero) {
          boiAttack(input);
        }
      }
    }
  }

  IEnumerator Reload() {
    fired = true;
    yield return new WaitForSeconds(fireDelay);
    fired = false;
    yield return null;
  }

  void Attack() {
    basicShooting.Fire();
    StartCoroutine(Reload());
  }

  void boiAttack(Vector2 direction) {
    boiShooting.Fire(direction);
    StartCoroutine(Reload());
  }
}
