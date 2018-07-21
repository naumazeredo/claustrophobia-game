using UnityEngine;

public class FollowMover : MonoBehaviour {
  public float speed = 5f;
  public float updateRate = 1;

  private Rigidbody2D rb;
  private GameObject player;
  private float updateTime;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    player = GameObject.FindWithTag("Player");
  }

  void Update() {
    if (player == null || Time.time < updateRate) return;

    var playerDir = player.transform.position - transform.position;
    rb.velocity = speed * playerDir;
    updateTime = Time.time + updateRate;
  }
}
