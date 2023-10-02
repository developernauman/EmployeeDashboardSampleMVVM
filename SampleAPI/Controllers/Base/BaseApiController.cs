using log4net;
using Microsoft.AspNetCore.Mvc;
using Sample.Common;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SampleAPI.Controllers.Base
{
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly ILog Logger;

        protected BaseApiController(Type concreteType)
        {
            Logger = LogManager.GetLogger(concreteType);
        }

        protected async Task<IActionResult> Handle<T>(IDataProvider<T> dataProvider) where T : class
        {
            try
            {
                await dataProvider.Execute();
                return Ok(dataProvider.Data);
            }
            catch (ArgumentException ex)
            {
                Logger.Error(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Error(ex.Message, ex);
                return Unauthorized();
            }
            catch (AccessViolationException avEx)
            {
                Logger.Error(avEx.Message, avEx);
                return StatusCode((int)HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return StatusCode(500, ex);
            }
        }
    }
}
