namespace ROCNSBE.MVC.Models
{
  public class InstagramPost
  {
    public required string Id { get; set; }
    public required string Caption { get; set; }
    public required string MediaUrl { get; set; }
    public required string Permalink { get; set; }
    public DateTime Timestamp { get; set; }
  }


}
