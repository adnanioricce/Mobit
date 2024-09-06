using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions; 
namespace Mobit.Extensions;

public static class MobitExtensions
{
    public static Func<Task<IActionResult>> HandleEndpoint<TSource>(
        this ControllerBase controller
        ,Func<Task<IActionResult>> func
        ,ILogger<TSource> logger)
    {
        return () => {
            try
            {
                return func();
            }
            catch (System.Exception ex)
            {
                // var ctx = controller.Request.
                var url = controller.Request.GetEncodedUrl();
                logger.LogError("An exception was throw in a request to {url} at controller {source} -> {ex}"
                    ,controller.Url
                    ,typeof(TSource).FullName
                    ,ex);
                return Task.FromResult<IActionResult>(new StatusCodeResult(500));
            }
        };
    }
}