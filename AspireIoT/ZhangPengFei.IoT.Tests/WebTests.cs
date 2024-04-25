using System.Net;
using System.Text;
using ZhangPengFei.IoT.ApiService.Model;

namespace ZhangPengFei.IoT.Tests;

public class WebTests
{
    [Fact]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.ZhangPengFei_IoT_AppHost>();
        await using var app = await appHost.BuildAsync();
        await app.StartAsync();
    
        // Act
        var httpClient = app.CreateHttpClient("api-service");
        var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(new GateWay
            {
                Name = "gateway",
                State = true,
                Desc = "test"
            }
        ), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/api/gateway/add", jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}