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
  public bool playLevels;
  public Level[] levels;

  bool levelPlaying;

  int currentLevelIndex;
  Level currentLevel;
  int levelEnemyCount;

  public Image ranking;
  public Text rankingLetter;
  public int rankingEnemyAreaWidth = 350;
  public float rankingEnterWait = 1.5f;
  public float rankingExitWait = 1.5f;
  public float rankingEnemyInterval = 0.1f;
  public float rankingLetterShowWait = 1f;
  List<Image> enemiesKilled;
  /* -----   LEVEL   ------ */

  /* ----- HULL SHINE ----- */
  public Animator hullAnimator;
  /* ----- HULL SHINE ----- */

  /* ----- FLASH ----- */
  public CanvasGroup flash;
  bool flashActive;
  /* ----- FLASH ----- */

  private ItemHolder usableHolder;

  void Start () {
    canvas.gameObject.SetActive(true);

    keyMapper = new KeyMapper();

    foreach (var text in gameOverTexts)
      text.enabled = false;

    gameOver = false;
    Time.timeScale = 1f;

    enemiesKilled = new List<Image>();

    flash.gameObject.SetActive(true);

    usableHolder = GameObject.FindWithTag("UsableHolder").GetComponent<ItemHolder>();

    // Level
    //Debug.Log("Start ending");
    NextLevel();
  }

  void Update () {
    if (Input.GetKeyDown(KeyCode.I))
      keyMapper.Invert();

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

  public void NextLevel() {
    //Debug.Log("NextLevel");
    if (!playLevels)
      return;

    if (levelPlaying) {
      Debug.LogWarning("Level still playing! NextLevel must be called when no level is playing");
      return;
    }

    if (currentLevel)
      Destroy(currentLevel);

    if (currentLevelIndex >= levels.Length) {
      // SHOW END GAME
      Debug.Log("Credits rolling");
      return;
    }

    currentLevel = levels[currentLevelIndex++];
    currentLevel.gameObject.SetActive(true);
    RegisterLevel(currentLevel);
  }

  public void RegisterLevel(Level level) {
    Debug.Log("Register: " + level);

    currentLevel = level;
    levelEnemyCount = level.spawns.Length;

    if (levelEnemyCount == 0 && currentLevel.bossPrefab == null) {
      Debug.LogWarning("Empty level! No enemies and no boss!");
      return;
    }

    hullAnimator.SetBool("Shine", true);

    if (levelEnemyCount == 0) {
      StartCoroutine(SpawnBoss());
    } else {
      StartLevel();
    }
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
    if (gameOver)
      return;

    levelEnemyCount--;

    if (levelEnemyCount == 0 && currentLevel.bossPrefab != null) {
      StartCoroutine(SpawnBoss());
    } else if (levelEnemyCount < 0 || (levelEnemyCount == 0 && currentLevel.bossPrefab == null)) {
      levelPlaying = false;
      StartCoroutine(ShowRanking());
    }
  }

  public void AddEnemyKill(GameObject enemy) {
    var obj = new GameObject("EnemyKill-" + enemy.name);
    obj.AddComponent<ShrinkingFadeIn>();
    Image image = obj.AddComponent<Image>();
    RectTransform rectTransform = obj.GetComponent<RectTransform>();

    SpriteRenderer enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();

    image.sprite = enemySpriteRenderer.sprite;
    image.color = enemySpriteRenderer.color;

    rectTransform.SetParent(ranking.transform, false);
    obj.SetActive(false);

    rectTransform.sizeDelta = new Vector2(
      enemySpriteRenderer.sprite.rect.width,
      enemySpriteRenderer.sprite.rect.height
    );
    rectTransform.localScale = new Vector3(1f, 1f, 1f);

    rectTransform.anchorMin = new Vector2(0.5f, 0f);
    rectTransform.anchorMax = new Vector2(0.5f, 0f);

    enemiesKilled.Add(image);

    // ------
    usableHolder.EnemyKillEvent();
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

  IEnumerator ShowRanking() {
    Flash();
    ranking.gameObject.SetActive(true);

    yield return new WaitForSeconds(rankingEnterWait);

    float stride = Mathf.Min(
      8f,
      (float) rankingEnemyAreaWidth / Mathf.Max(1, enemiesKilled.Count-1)
    );

    float width = stride * (enemiesKilled.Count - 1);
    float posx = - width/2;

    foreach (var enemy in enemiesKilled) {
      enemy.gameObject.SetActive(true);
      enemy.GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, 70f);
      posx += stride;

      yield return new WaitForSeconds(rankingEnemyInterval);
    }

    yield return new WaitForSeconds(rankingLetterShowWait);

    rankingLetter.text = GetRankingLetter();
    rankingLetter.gameObject.SetActive(true);

    yield return new WaitForSeconds(rankingExitWait);

    rankingLetter.gameObject.SetActive(false);
    ranking.gameObject.SetActive(false);
    foreach (var enemy in enemiesKilled)
      Destroy(enemy.gameObject);
    enemiesKilled.Clear();

    levelPlaying = false;

    //Debug.Log("ShowRanking ending");
    NextLevel();
  }

  string GetRankingLetter() {
    bool hasBoss = currentLevel.bossPrefab != null;
    int enemiesCount = enemiesKilled.Count - (hasBoss ? 1 : 0);

    if (enemiesCount == 0) return "P";
    if (enemiesCount == currentLevel.spawns.Length) return "S";

    float ratio = (float) enemiesCount / currentLevel.spawns.Length;
    const float interval = 1f / 6f;
    if (                       ratio <   interval) return "F";
    if (ratio >=   interval && ratio < 2*interval) return "E";
    if (ratio >= 2*interval && ratio < 3*interval) return "D";
    if (ratio >= 3*interval && ratio < 4*interval) return "C";
    if (ratio >= 4*interval && ratio < 5*interval) return "B";
    return "A";
  }
  /* -----   LEVEL   ------ */

  /* -----   FLASH   ------ */
  void Flash() {
    flashActive = true;
    flash.alpha = 1f;
  }
  /* -----   FLASH   ------ */

  public int GetEnemiesKilledCount() {
    return enemiesKilled.Count;
  }
}
