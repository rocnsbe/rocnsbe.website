using Microsoft.Extensions.Configuration;
using ROCNSBE.MVC.Models;
using System.Text.Json;

namespace ROCNSBE.MVC.DataSource
{
  public class InstagramDataSource : IInstagramDataSource
  {
    private readonly IConfiguration _configuration;

    private string? _accessToken => _configuration["Instagram:AccessToken"];
    private string? _userId => _configuration["Instagram:UserId"];

    public InstagramDataSource(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    /// <summary>
    /// Loads the Instagram feed for a given user using the Instagram Graph API.
    /// </summary>
    /// <param name="accessToken">The access token for authenticating API requests.</param>
    /// <param name="userId">The Instagram user ID for which to load the feed.</param>
    /// <returns>A list of recent Instagram posts.</returns>
    /// <remarks>
    /// This function uses HttpClient to make a GET request to the Instagram Graph API.
    /// Ensure that the access token has the necessary permissions to access the user's feed.
    /// </remarks>
    public async Task<List<InstagramPost>> LoadInstagramFeedAsync()
    {
      var posts = new List<InstagramPost>();
      var apiUrl = $"https://graph.instagram.com/{_userId}/media?fields=id,caption,media_url,permalink,timestamp&access_token={_accessToken}";

      using (var httpClient = new HttpClient())
      {
        var response = await httpClient.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
          var json = await response.Content.ReadAsStringAsync();
          var jsonDocument = JsonDocument.Parse(json);

          foreach (var element in jsonDocument.RootElement.GetProperty("data").EnumerateArray())
          {
            var post = new InstagramPost
            {
              Id = element.GetProperty("id").GetString(),
              Caption = element.GetProperty("caption").GetString(),
              MediaUrl = element.GetProperty("media_url").GetString(),
              Permalink = element.GetProperty("permalink").GetString(),
              Timestamp = element.GetProperty("timestamp").GetDateTime()
            };
            posts.Add(post);
          }
        }
        else
        {
          // Handle error response
          throw new Exception($"Failed to load Instagram feed: {response.StatusCode}");
        }
      }

      return posts;
    }

    /// <summary>
    /// Represents an Instagram post retrieved from the Instagram Graph API.
    /// </summary>

  }


}
