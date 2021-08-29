using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlockUsersService.LoggerHelper;
using Newtonsoft.Json;
using System.Text;

namespace BlockUsersService.LoggerHelper
{
    public class Logger : ILogger
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient _httpClient;

        public Logger(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            this.configuration = configuration;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LogModel model = new LogModel();
            model.Id = Guid.NewGuid();
            model.LogLevel = logLevel.ToString();
            model.Microservice = "Blocking Service";
            model.Message = state.ToString();
            model.TimeOfAction = DateTime.Now.ToString();

            string json = JsonConvert.SerializeObject(model);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(configuration.GetValue<string>("URI:url"), stringContent);
        }

    }
}
