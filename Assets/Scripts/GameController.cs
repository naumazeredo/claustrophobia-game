using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
  public Canvas canvas;

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
  List<Image> enemiesKilled;

  public GameObject enemiesKilledImagesParent;
  /* -----   LEVEL   ------ */

  /* ----- FLASH ----- */
  public CanvasGroup flash;
  bool flashActive;
  /* ----- FLASH ----- */

  void Start () {
    canvas.gameObject.SetActive(true);

    keyMapper = new KeyMapper();

    foreach (var text in gameOverTexts)
      text.enabled = false;

    gameOver = false;
    Time.timeScale = 1f;

    enemiesKilled = new List<Image>();

    flash.gameObject.SetActive(true);
  }

  void Update () {
    if (Input.GetKeyDown(KeyCode.I))
      keyMapper.invert();

    if (gameOver) {
      if (Input.GetKeyDown(KeyCode.R)) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }

    /* FlASH */
    if (flashActive) {
      flash.alpha = Mathf.MoveTowards(flash.alpha, 0, Time.deltaTime);
      flashActive = flash.alpha > Mathf.Epsilon;
    }
    /* FlASH */
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

    if (levelEnemyCount == 0) {
    }

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

    if (levelEnemyCount == 0 && currentLevel.bossPrefab != null) {
      StartCoroutine(SpawnBoss());
    } else if (levelEnemyCount < 0 || currentLevel.bossPrefab == null) {
      levelPlaying = false;
      ShowRanking();
    }
  }

  public void AddEnemyKill(GameObject enemy) {
    var obj = new GameObject();
    Image enemyImage = obj.AddComponent<Image>();
    enemyImage.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
    enemyImage.color = enemy.GetComponent<SpriteRenderer>().color;
    obj.GetComponent<RectTransform>().SetParent(enemiesKilledImagesParent.transform);
    obj.SetActive(false);
  }

  public void ShowRanking() {
  }

  IEnumerator SpawnBoss() {
    Flash();
    var boss = Instantiate(currentLevel.bossPrefab, Vector3.zero, Quaternion.identity);

    boss.GetComponents<MonoBehaviour>().ToList().ForEach(c => c.enabled = false);
    boss.GetComponent<SpriteRenderer>().enabled = true;

    // TODO: Stop player input

    yield return new WaitForSeconds(3f);

    boss.GetComponents<MonoBehaviour>().ToList().ForEach(c => c.enabled = true);
  }
  /* -----   LEVEL   ------ */

  /* -----   FLASH   ------ */
  void Flash() {
    flashActive = true;
    flash.alpha = 1f;
  }
  /* -----   FLASH   ------ */
}
