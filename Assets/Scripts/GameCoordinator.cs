using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCoordinator : MonoBehaviour {
  public Text[] gameOverTexts;
  public float gameOverTimeScale = .2f;
  public KeyMapper keyMapper;

  bool gameOver;

  void Start () {
    keyMapper = new KeyMapper();

    foreach (var text in gameOverTexts)
      text.enabled = false;

    gameOver = false;
    Time.timeScale = 1f;
  }

  void Update () {
    if (Input.GetKeyDown(KeyCode.I))
      keyMapper.invert();

    if (gameOver) {
      if (Input.GetKeyDown(KeyCode.R)) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }
  }

  public void GameOver() {
    StartCoroutine(GameOverRoutine());
  }

  IEnumerator GameOverRoutine() {
    Time.timeScale = gameOverTimeScale;
    yield return new WaitForSecondsRealtime(.5f);

    foreach (var text in gameOverTexts)
      text.enabled = true;

    gameOver = true;
    yield return null;
  }
}
