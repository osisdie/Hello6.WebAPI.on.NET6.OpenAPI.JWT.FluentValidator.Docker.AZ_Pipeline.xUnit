using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Consts;
using CoreFX.Abstractions.Serializers;
using CoreFX.Common.App_Start;
using Hello6.Domain.Contract.EchoServices.Cache;
using Hello6.Domain.Contract.EchoServices.Config;
using Hello6.Domain.Contract.EchoServices.DB;
using Hello6.Domain.Contract.EchoServices.Dump;
using Hello6.Domain.Contract.EchoServices.Version;
using IntegrationTest.Hello6.App_Start;
using TestAbstractions.Consts;
using Xunit;

namespace IntegrationTest.Hello6
{
    public class CI_Test : DerivedUnitTestBase
    {
        private readonly string _endpointHostUrl = "http://localhost:5006";

        public CI_Test()
        {
            var endpointHostUrl = Environment.GetEnvironmentVariable(TestConst.DefaultEndpointKey);
            if (!string.IsNullOrEmpty(endpointHostUrl))
            {
                _endpointHostUrl = endpointHostUrl;
            }
        }

        [Fact(Skip = "Test if endpoint is accessible")]
        public async Task Integration_Test()
        {
            var uri = new Uri(_endpointHostUrl);
            using (var httpClient = new HttpClient() { BaseAddress = uri })
            {
                // is accessible ?
                {
                    var res = await httpClient.GetAsync("/health");
                    Assert.Equal(HttpStatusCode.OK, res.StatusCode);

                    var content = await res.Content.ReadAsStringAsync();
                    Assert.Equal(SvcConst.DefaultHealthyResponse, content);
                }

                // version number
                {
                    var res = await httpClient.GetAsync("/api/echo/version");
                    Assert.Equal(HttpStatusCode.OK, res.StatusCode);

                    var content = await res.Content.ReadAsStringAsync();
                    Assert.NotNull(content);
                    var respDto = DefaultJsonSerializer.Deserialize<EchoVersion_ResponseDto>(content);
                    Assert.True(respDto?.IsSuccess);
                    Assert.NotNull(respDto.Data);
                    Assert.Contains(new Version(1, 0, 0).ToString(), respDto.Data);
                }

                // config
                {
                    var res = await httpClient.GetAsync("/api/echo/config");
                    Assert.Equal(HttpStatusCode.OK, res.StatusCode);

                    var content = await res.Content.ReadAsStringAsync();
                    Assert.NotNull(content);
                    var respDto = DefaultJsonSerializer.Deserialize<EchoConfig_ResponseDto>(content);
                    Assert.True(respDto?.IsSuccess);
                    Assert.NotEmpty(respDto.Data);
                }

                // db
                {
                    var res = await httpClient.GetAsync("/api/echo/db");
                    Assert.Equal(HttpStatusCode.OK, res.StatusCode);

                    var content = await res.Content.ReadAsStringAsync();
                    Assert.NotNull(content);
                    var respDto = DefaultJsonSerializer.Deserialize<EchoDB_ResponseDto>(content);
                    Assert.True(respDto?.IsSuccess);
                    Assert.NotNull(respDto.Data);
                }

                // cache
                {
                    var res = await httpClient.GetAsync("/api/echo/cache");
                    Assert.Equal(HttpStatusCode.OK, res.StatusCode);

                    var content = await res.Content.ReadAsStringAsync();
                    Assert.NotNull(content);
                    var respDto = DefaultJsonSerializer.Deserialize<EchoCache_ResponseDto>(content);
                    Assert.True(respDto?.IsSuccess);
                    Assert.NotNull(respDto.Data);
                }

                // dump
                {
                    var res = await httpClient.GetAsync("/api/echo/dump");
                    Assert.Equal(HttpStatusCode.OK, res.StatusCode);

                    var content = await res.Content.ReadAsStringAsync();
                    Assert.NotNull(content);
                    var respDto = DefaultJsonSerializer.Deserialize<EchoDump_ResponseDto>(content);
                    Assert.True(respDto?.IsSuccess);
                    Assert.NotEmpty(respDto.ExtMap);
                }
            }
        }

        [Fact]
        public void EnvironmentVariable_Test()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Assert.NotNull(env);
            Assert.Equal("Development", EnvConst.Development);
            Assert.True(SvcContext.IsDevelopment());
            Assert.Equal(env, SdkRuntime.SdkEnv);
        }
    }
}
