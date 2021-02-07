using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using ShopBridge;
using ShopBridge.Controllers;
using ShopBridge.Model;
using ShopBridge.Model.Products;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace ShopBridgeTest
{
    public class ProductsControllerTest
    {

        private readonly IServiceProvider serviceProvider;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ProductsControllerTest()
        {
            var services = new ServiceCollection();

            services.AddEntityFrameworkSqlite().AddDbContext<ProductsDbContext>();
            serviceProvider = services.BuildServiceProvider();
            _server = new TestServer(new WebHostBuilder()
        .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

      [Fact]
        public async System.Threading.Tasks.Task Add_Data_Not_Available_TestAsync()
        {
            var dbContext = serviceProvider.GetRequiredService<ProductsDbContext>();
            dbContext.Database.EnsureCreated();
            var mockEnvironment = new Mock<IHostingEnvironment>();
            mockEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            var Products = new ProductsController(dbContext, mockEnvironment.Object);

            var formVars = new Dictionary<string, string>();
            InventoryDetails inventory = new InventoryDetails();
            inventory.description = "Test Description";
            inventory.price = 200;
            inventory.name = "Test Name";
            formVars.Add("model", JsonConvert.SerializeObject(inventory));
            var content = new FormUrlEncodedContent(formVars);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://Localhost/api/Products/AddProduct");
            request.Content = content;

            var mockFactory = new Mock<IHttpClientFactory>();

            
            // Act
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await _client.SendAsync(request);
                Assert.Equal("200", response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


     [Fact]
        public void Get_Data_Not_Available_Test()
        {
            var dbContext = serviceProvider.GetRequiredService<ProductsDbContext>();
            dbContext.Database.EnsureCreated();
            var mockEnvironment = new Mock<IHostingEnvironment>();
            mockEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            ProductsController Products = new ProductsController(dbContext, mockEnvironment.Object);
            string result = Products.GetData();
            Assert.NotNull(result);
            
        }

        [Theory]
        [InlineData(1)]
        public void Remove_Data_Not_Available_Test(int uniqid)
        {
            var dbContext = serviceProvider.GetRequiredService<ProductsDbContext>();
            dbContext.Database.EnsureCreated();
            var mockEnvironment = new Mock<IHostingEnvironment>();
            mockEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            ProductsController Products = new ProductsController(dbContext, mockEnvironment.Object);
            string result = Products.RemoveProduct(uniqid);
            Assert.NotNull(result);
        }


    }
}
