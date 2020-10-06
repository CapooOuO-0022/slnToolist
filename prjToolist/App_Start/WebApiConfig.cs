using System.Web.Http;

namespace prjToolist {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.EnableCors(); //加入這行
            // Web API 設定和服務
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear(); //取消XML
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}