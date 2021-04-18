using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Consts;
using CoreFX.Abstractions.Contracts.Extensions;
using CoreFX.Abstractions.Enums;
using CoreFX.Abstractions.Logging.Extensions;
using CoreFX.Abstractions.Utils;
using CoreFX.Caching.Redis.Extensions;
using CoreFX.Common.Extensions;
using Hello6.Domain.Common;
using Hello6.Domain.Contract.EchoServices.Cache;
using Hello6.Domain.Contract.EchoServices.Config;
using Hello6.Domain.Contract.EchoServices.DB;
using Hello6.Domain.Contract.EchoServices.Dump;
using Hello6.Domain.Contract.EchoServices.Version;
using Hello6.Domain.DataAccess.Database.Echo.Interfaces;
using Hello6.Domain.Endpoint.Controllers.Bases;
using Hello6.Domain.Endpoint.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Hello6.Domain.Endpoint.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/echo")]
    public class EchoController : DomainContollerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IEchoRepository _repository;

        public EchoController(IEchoRepository repository, IDistributedCache distributedCache = null)
        {
            _repository = repository;
            _distributedCache = distributedCache;
        }

        [Route("ver")]
        [Route("version")]
        [HttpGet]
        public async Task<ActionResult> EchoVersion()
        {
            var res = new EchoVersion_ResponseDto
            {
                Data = SdkRuntime.Version
            }.Success();

            await Task.CompletedTask;
            return new JsonResult(res);
        }

        [AuthorizeActionFilter]
        [Route("throw")]
        [HttpGet]
        public Task<IActionResult> EchoThrow()
        {
            throw new NotImplementedException();
        }

        [AuthorizeActionFilter]
        [Route("db")]
        [HttpGet]
        public async Task<ActionResult> EchoDB(string ver = null)
        {
            var res = new EchoDB_ResponseDto();
            if (!string.IsNullOrEmpty(ver))
            {
                await _repository.SetVerision(ver);
            }

            var dbResult = await _repository.GetVerision();
            if (dbResult.Any())
            {
                res.Success(SvcCodeEnum.Success, dbResult.Msg).SetData(dbResult.Data);
            }
            else
            {
                res.Error(SvcCodeEnum.Error, dbResult?.Msg ?? SvcMsg.DatabaseUnavailable);
            }

            return new JsonResult(res)
            {
                StatusCode = (int)(res.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable)
            };
        }

        [AuthorizeActionFilter]
        [Route("cache")]
        [HttpGet]
        public async Task<ActionResult> EchoCache()
        {
            var res = new EchoCache_ResponseDto();
            var cacheKey = $"{GetType()}{SvcConst.Separator}{NetworkUtil.LocalIP}";

            if (_distributedCache != null)
            {
                var cacheResult = await _distributedCache.SetAsync(cacheKey, SvcConst.DefaultHealthyResponse, DateTime.UtcNow.AddSeconds(5));
                if (cacheResult.Any())
                {
                    res.Success(SvcCodeEnum.Success, cacheResult.Msg).SetData(cacheResult.Data);
                }
                else
                {
                    res.Error(SvcCodeEnum.Error, cacheResult?.Msg ?? SvcMsg.CacheUnavailable);
                }
            }
            else
            {
                res.Error(SvcCodeEnum.Error, SvcMsg.CacheUnavailable);
            }

            return new JsonResult(res)
            {
                StatusCode = (int)(res.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable)
            };
        }

        [AuthorizeActionFilter]
        [Route("config")]
        [HttpGet]
        public async Task<ActionResult> EchoConfig()
        {
            var appSettingConfig = System.IO.File.Exists(
                SvcConst.DefaultAppSettingsFile.AddingBeforeExtension(SdkRuntime.SdkEnv));
            var helloSettingConfig = System.IO.File.Exists(
                HelloContext.Settings.Name);
            var logConfig = System.IO.File.Exists(
                Path.Combine(SvcConst.DefaultConfigFolder, SvcConst.DefaultLog4netConfigFile.AddingBeforeExtension(SdkRuntime.SdkEnv)));

            var res = new EchoConfig_ResponseDto();
            res.SetStatusCode((appSettingConfig & helloSettingConfig & logConfig))
                .SetData(new Dictionary<string, bool>
                {
                    {nameof(appSettingConfig), appSettingConfig },
                    {nameof(helloSettingConfig), helloSettingConfig },
                    {nameof(logConfig), logConfig },
                });

            await Task.CompletedTask;
            return new JsonResult(res)
            {
                StatusCode = (int)(res.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable)
            };
        }

        [AuthorizeActionFilter]
        [Route("dump")]
        [HttpGet]
        public async Task<ActionResult> EchoDump()
        {
            var res = new EchoDump_ResponseDto();
            res.ExtMap ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            res.ExtMap.AddDebugData();
            res.Success();

            await Task.CompletedTask;
            return new JsonResult(res);
        }
    }
}
