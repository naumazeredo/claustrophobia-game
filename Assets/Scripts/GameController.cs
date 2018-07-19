using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
  public KeyMapper keyMapper;

  /* ----- GAME OVER ------ */
  public Text[] gameOverTexts;
  public float gameOverTimeScale = .2f;
  bool gameOver;
  /* ----- GAME OVER ------ */

  /* -----   LEVEL   ------ */
  bool levelPlaying;
  Level currentLevel;
  int levelEnemyCount;
  List<GameObject> enemiesKilled;
  /* -----   LEVEL   ------ */

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

  /* ----- GAME OVER ------ */
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
  /* ----- GAME OVER ------ */

  /* -----   LEVEL   ------ */
  public void RegisterLevel(Level level, int enemyCount) {
    currentLevel = level;
    levelEnemyCount = enemyCount;

    StartLevel();
  }

  public void StartLevel() {
    if (levelPlaying) {
      Debug.LogWarning("Level already playing!");
      return;
    }

    levelPlaying = true;
    currentLevel.StartLevel();
  }

  public void AddEnemyDeath() {
    levelEnemyCount--;

    if (levelEnemyCount == 0) {
      currentLevel.SpawnBoss();
    } else if (levelEnemyCount < 0) {
      levelPlaying = false;
      ShowRanking();
    }
  }

  public void AddEnemyKill(GameObject enemy) {
    enemy.GetComponents<MonoBehaviour>().ToList().ForEach(c => c.enabled = false);
    enemy.GetComponent<SpriteRenderer>().enabled = true;

    enemiesKilled.Add(enemy);
  }

  public void ShowRanking() {
  }
  /* -----   LEVEL   ------ */
}
