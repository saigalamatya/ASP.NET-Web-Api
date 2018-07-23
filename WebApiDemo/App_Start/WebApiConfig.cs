using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WebApiContrib.Formatting.Jsonp;
using System.Web.Http.Cors;

namespace WebApiDemo
{
    public class CustomJsonFormatter : JsonMediaTypeFormatter
    {
        //public CustomJsonFormatter()
        //{
        //    this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        //}

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "RestApi",
            //    routeTemplate: "api/{controller}/{gender}",
            //    defaults: new { gender = RouteParameter.Optional }
            //);

            // for jsonp to resolve cors issue
            // var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            // config.Formatters.Insert(0, jsonpFormatter);

            // Enable CORS using package
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //config.Filters.Add(new RequireHttpsAttribute());

            // for basic authentication
            config.Filters.Add(new BasicAuthenticationAttribute());

            // Remove Xml Media Type irrespective of the accept header value
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            //config.Formatters.Add(new CustomJsonFormatter());

            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
