using api.MessageHandlers;
using System.Net.Http.Headers;
using System.Web.Http;

namespace api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyHandler());
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CommentsForRecording",
                routeTemplate: "api/Recording/{id}/Comments/{param}",
                defaults: new { controller = "RecordingComments", param = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CommentsForUser",
                routeTemplate: "api/User/{id}/Comments/{param}",
                defaults: new { controller = "UserComments", param = RouteParameter.Optional }
            );

            //Switch to JSON instead of XML
            config.Formatters.JsonFormatter.SupportedMediaTypes
    .Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}