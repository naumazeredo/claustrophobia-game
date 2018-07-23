using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {
  public string gameName;
  public Text nameText;
  public GameObject pressSpace;
  public GameObject instructions;
  public float letterInterval = 0.15f;
  public float waitInstructions = .5f;
  public float waitPressSpace = 1f;

  bool canPress;

  void Start () {
    StartCoroutine(EntranceCoroutine());
  }

  void Update () {
    if (canPress && Input.GetKey(KeyCode.Space)) {
      SceneManager.LoadScene("Main");
    }
  }

  IEnumerator EntranceCoroutine() {
    canPress = false;
    pressSpace.SetActive(false);
    instructions.SetActive(false);

    for (int i = 1; i <= gameName.Length; i++) {
      nameText.text =
        gameName.Substring(0, i) +
        "<color=#0000>" +
        gameName.Substring(i) +
        "</color>";

      yield return new WaitForSeconds(letterInterval);
    }

    yield return new WaitForSeconds(waitInstructions);
    instructions.SetActive(true);

    yield return new WaitForSeconds(waitPressSpace);
    canPress = true;
    pressSpace.SetActive(true);

    yield return null;
  }
}
