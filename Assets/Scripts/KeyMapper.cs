public class KeyMapper {
  private string horizontalMov = "Horizontal";
  private string horizontalFire = "FireHorizontal";
  private string verticalMov = "Vertical";
  private string verticalFire = "FireVertical";

  public void invert() {
    Utils.Swap(ref horizontalFire, ref horizontalMov);
    Utils.Swap(ref verticalFire, ref verticalMov);
  }

  public string getHorizontalMov() {
    return horizontalMov;
  }
  public string getHorizontalFire() {
    return horizontalFire;
  }
  public string getVerticalMov() {
    return verticalMov;
  }
  public string getVerticalFire() {
    return verticalFire;
  }
}
