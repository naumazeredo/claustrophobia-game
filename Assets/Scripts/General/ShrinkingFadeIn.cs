using UnityEngine;

public class ShrinkingFadeIn : MonoBehaviour {
  public float duration = 0.1f;
  public float initialScale = 2f;

  bool running;

  void OnEnable() {
    running = true;
    transform.localScale = new Vector3(initialScale, initialScale, 1f);
  }

  void Update () {
    if (!running)
      return;

    float scale = transform.localScale.x;
    scale = Mathf.MoveTowards(scale, 1f, (initialScale - 1f) * Time.deltaTime/duration);
    transform.localScale = new Vector3(scale, scale, 1f);

    running = scale > 1f+Mathf.Epsilon;
  }
}
