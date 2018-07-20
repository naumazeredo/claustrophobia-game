public class KeyMapper {
  private string horizontalMov = "Horizontal";
  private string horizontalFire = "FireHorizontal";
  private string verticalMov = "Vertical";
  private string verticalFire = "FireVertical";

  public void Invert() {
    Utils.Swap(ref horizontalFire, ref horizontalMov);
    Utils.Swap(ref verticalFire, ref verticalMov);
  }

  public string GetHorizontalMov() {
    return horizontalMov;
  }
  public string GetHorizontalFire() {
    return horizontalFire;
  }
  public string GetVerticalMov() {
    return verticalMov;
  }
  public string GetVerticalFire() {
    return verticalFire;
  }
}
