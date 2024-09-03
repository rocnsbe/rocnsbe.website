using ROCNSBE.MVC.Models;

namespace ROCNSBE.MVC.DataSource
{
  public interface IInstagramDataSource
  {
    Task<List<InstagramPost>> LoadInstagramFeedAsync();
  }
}