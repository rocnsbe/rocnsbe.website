using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ROCNSBE.MVC.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROCNSBE.MVC.DataSource.Tests
{
  [TestClass()]
  public class InstagramDataSourceTests
  {
    private IServiceProvider _serviceProvider;

    public InstagramDataSourceTests()
    {
      _serviceProvider = InitServiceProvider();
    }

    public static IServiceProvider InitServiceProvider()
    {
      var services = new ServiceCollection();

      services.AddSingleton(InitConfiguration());
      services.AddTransient<IInstagramDataSource, InstagramDataSource>();

      return services.BuildServiceProvider();
    }

    public static IConfiguration InitConfiguration()
    {
      var config = new ConfigurationBuilder()
         .AddJsonFile("appsettings.test.json")
          .AddEnvironmentVariables()
          .Build();
      return config;
    }

    [TestMethod()]
    public async Task LoadInstagramFeedAsyncTest()
    {
      var dataSource = _serviceProvider.GetRequiredService<IInstagramDataSource>();
      var feed = await dataSource.LoadInstagramFeedAsync();

      Assert.IsTrue(feed.Count > 0);
    }
  }
}