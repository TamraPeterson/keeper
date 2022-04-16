namespace keeper.Models
{
  public class Keep
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int Views { get; set; }
    public int Kept { get; set; }
    public string CreatorId { get; set; }
    public Account? Creator { get; set; }
  }

  public class VKViewModel : Keep
  {
    public int ViewKeepId { get; set; }
    public new string CreatorId { get; set; }
  }
}