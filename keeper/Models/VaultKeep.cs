namespace keeper.Models
{
  public class VaultKeep
  {
    public int Id { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }

  }

  public class VaultKeepViewModel : VaultKeep
  {
    public string CreatorId { get; set; }
  }
}