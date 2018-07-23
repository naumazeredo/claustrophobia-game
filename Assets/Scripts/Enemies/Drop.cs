using UnityEngine;

public class Drop : MonoBehaviour {
  public GameObject[] items;
  public int dropRate;

  public void DropItem(Vector2 position) {
    if (Random.Range(0, 100) <= dropRate)
      Instantiate(items[Random.Range(0, items.Length)], position, Quaternion.identity);
  }
}
