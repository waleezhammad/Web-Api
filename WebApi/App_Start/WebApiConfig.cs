using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApi.WebApiFilters;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "FoodRounting",
                routeTemplate: "api/nutrintions/Foods/{id}",
                defaults: new { controller = "Foods", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "FoodMeasureRounting",
                routeTemplate: "api/nutrintions/Foods/{foodId}/Measues/{id}",
                defaults: new { controller = "Measures", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DiaryRounting",
                routeTemplate: "api/user/Diaries/{DiaryId}",
                defaults: new { controller = "Diary", DiaryId = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                            name: "DiaryEntryRounting",
                            routeTemplate: "api/user/Diaries/{DiaryId}/DiaryEntry/{id}",
                            defaults: new { controller = "DiaryEntry", id = RouteParameter.Optional }
                        );

            config.Routes.MapHttpRoute(
                name: "FoodMeasureRountingV2",
                routeTemplate: "api/v2/nutrintions/Foods/{foodId}/Measues/{id}",
                defaults: new { controller = "MeasuresV2", id = RouteParameter.Optional }
            );

            #region Force Api reurn Json

            /*
            var jsonFormatter = new JsonMediaTypeFormatter();
            //optional: set serializer settings here
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
            */

            #endregion

            //User Camel Case instead of Pascal case
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            #if !DEBUG
            config.Filters.Add(new ForceHttpsAttribute());
            #endif
            
        }
    }
    public class JsonContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }

        public ContentNegotiationResult Negotiate(
                Type type,
                HttpRequestMessage request,
                IEnumerable<MediaTypeFormatter> formatters)
        {
            return new ContentNegotiationResult(
                _jsonFormatter,
                new MediaTypeHeaderValue("application/json"));
        }
    }
}
