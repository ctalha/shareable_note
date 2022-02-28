using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedNote.Application.ValidationResponse;
using System.Linq;

namespace SharedNote.Application.Extensions
{
    public static class ControllerAndOptionsExtension
    {
        public static void AddContollerWithOptions(this IServiceCollection services)
        {
            services.AddControllers()
            .ConfigureApiBehaviorOptions(
            option => option.InvalidModelStateResponseFactory = actionContext =>
            {
                return CustomErrorResponse(actionContext);
            });

            BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
            {

                var errorRecordList = actionContext.ModelState
                  .Where(modelError => modelError.Value.Errors.Count > 0)
                  .Select(modelError => new ErrorDescription
                  {
                      FieldName = modelError.Key,
                      FieldDescription = modelError.Value.Errors.Select(p => p.ErrorMessage).ToList()
                  }).ToList();
                var response = new ValidationError();

                foreach (var item in errorRecordList)
                {
                    response.Fields.Add(item.FieldName, item.FieldDescription);
                }

                return new BadRequestObjectResult(new ValidationResponseGlobal
                {
                    IsSuccess = false,
                    Message = "Validation Hatası",
                    StatusCode = 400,
                    Errors = response
                });
            }
        }
    }
}
