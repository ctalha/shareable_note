using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SharedNote.Application;
using SharedNotes.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using SharedNote.Application.Valdiations.FluentValidation.BaseValidator;
using SharedNote.Application.Valdiations.FluentValidation;
using SharedNote.Domain.Entites;
using SharedNotes.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SharedNote.Application.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShareableNote.API.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using SharedNote.Application.Extensions;

namespace ShareableNote.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddControllers()
            //    .ConfigureApiBehaviorOptions(
            //    option => option.InvalidModelStateResponseFactory = actionContext =>
            //    {
            //        return CustomErrorResponse(actionContext);
            //    });
            services.AddContollerWithOptions();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShareableNote.API", Version = "v1" });
            });

            services.Configure<TokenOption>(Configuration.GetSection("TokenOption"));

            //BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
            //{

            //    var errorRecordList = actionContext.ModelState
            //      .Where(modelError => modelError.Value.Errors.Count > 0)
            //      .Select(modelError => new ErrorDescription
            //      {
            //         FieldName = modelError.Key,
            //         FieldDescription = modelError.Value.Errors.Select(p => p.ErrorMessage).ToList()
            //      }).ToList();
            //    var response = new ValidationError();
                
            //    foreach (var item in errorRecordList)
            //    {
            //        response.Fields.Add(item.FieldName, item.FieldDescription);
            //    }

            //    return new BadRequestObjectResult(new ValidationResponseGlobal
            //    {
            //        IsSuccess = false,
            //        Message = "Validation Hatasý",
            //        StatusCode = 400,
            //        Errors =  response
            //    });
            //}

            services.AddIdentity<User, UserRole>(Opt =>
            {
                Opt.User.RequireUniqueEmail = true;
                Opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            var tokenOptions = Configuration.GetSection("TokenOption").Get<TokenOption>();
            services.JwtOptions(tokenOptions);
            services.AddApplicationService();
            services.AddPressitenceService();
            services.AddLogging();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShareableNote.API v1"));
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.AddApplicationBuilder();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
