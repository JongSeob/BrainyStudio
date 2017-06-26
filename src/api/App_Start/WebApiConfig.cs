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

            // Default API
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // List recordings in repository
            config.Routes.MapHttpRoute(
                name: "RecordingsInRepository",
                routeTemplate: "api/Repository/{id}/Recording/",
                defaults: new { controller = "RepositoryRecording" }
            );

            // List/Create/Delete comments for recording
            config.Routes.MapHttpRoute(
                name: "CommentsForRecording",
                routeTemplate: "api/Recording/{id}/Comments/{param}",
                defaults: new { controller = "RecordingComments", param = RouteParameter.Optional }
            );

            // Data for recording
            config.Routes.MapHttpRoute(
                name: "DataForRecording",
                routeTemplate: "api/Recording/{id}/Data/{param}",
                defaults: new { controller = "RecordingData", param = RouteParameter.Optional }
            );

            // List/Create/Delete markers for recording
            config.Routes.MapHttpRoute(
                name: "MarkerForRecording",
                routeTemplate: "api/Recording/{id}/Marker/{param}",
                defaults: new { controller = "RecordingMark", param = RouteParameter.Optional }
            );

            //Switch to JSON instead of XML
            config.Formatters.JsonFormatter.SupportedMediaTypes
    .Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}