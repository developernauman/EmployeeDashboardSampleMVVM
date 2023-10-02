using Sample.Common.Configuration;
using Sample.Common.Enum;
using Sample.Common.Log4Net;
using Sample.Common.Response;
using System;
using System.Threading.Tasks;

namespace Sample.Common.BAL
{
    public abstract class BaseService : LoggingService
    {
        protected readonly Type _concreteType;

        protected BaseService(Type concreteType) : base(concreteType)
        {
            _concreteType = concreteType;
        }


        protected virtual async Task<Response<T>> RunMethodAsync<T>(Func<Task<Response<T>>> taskMethod)
        {
            try
            {
                var result = await taskMethod();

                if (!result.Success)
                {
                    LogErrorItem(result.Error);

                    result.Error = EnsureErrorItem(result.Error);

                    if ((BaseConfiguration.Instance.ReadConfigureStringValue("Environment")
                            .Equals("PRODUCTION", StringComparison.InvariantCultureIgnoreCase) &&
                        result.Error.Exception == null)

                        || !BaseConfiguration.Instance.ReadConfigureStringValue("Environment")
                            .Equals("PRODUCTION"))
                    {
                        return new Response<T>() { Success = false, Error = result.Error };
                    }

                    return new Response<T>() { Success = false, Error = new ErrorItem("Something went wrong, please try again later.") };
                }

                result.ResponseCode = (int)ResponseCode.Success;
                return result;
            }
            catch (Exception ex)
            {
                GetLogger.Error("RunMethod Exception", ex);
                return new Response<T>() { Success = false, Error = new ErrorItem(ex.Message) };
            }
        }


        protected void ThrowError(ErrorItem error)
        {
            if (error == null)
                return;

            throw new ArgumentException(error.Message, error.Exception);
        }

        protected void LogErrorItem(ErrorItem error)
        {
            if (error != null)
            {
                if (!string.IsNullOrWhiteSpace(error.Message))
                {
                    if (error.Exception != null)
                    {
                        GetLogger.Error(error.Message, error.Exception);
                    }
                    else
                    {
                        GetLogger.Error(error.Message);
                    }
                }
                else if (error.Exception != null)
                {
                    GetLogger.Error("Error occur", error.Exception);
                }
            }
        }

        protected ErrorItem EnsureErrorItem(ErrorItem error)
        {
            if (error?.Exception == null)
                return error;

#if !DEBUG
            if (error?.Exception != null)
                error.Exception = null;
#endif

            return error;
        }
    }
}
