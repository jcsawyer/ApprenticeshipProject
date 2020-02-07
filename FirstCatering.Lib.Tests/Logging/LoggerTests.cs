using System;
using FirstCatering.Lib.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstCatering.Lib.Tests.Logging
{
    [TestClass]
    public class LoggerTests
    {
        private ILogger Logger { get; }
        private object Data { get; } = new { Id = 1, Name = "TEST" };

        public LoggerTests()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ILogger, Logger>();

            Logger = services.BuildServiceProvider().GetService<ILogger>();
        }

        [TestMethod]
        public void Debug()
            => Logger.Debug(nameof(Debug));

        [TestMethod]
        public void Error()
            => Logger.Error(nameof(Error));

        [TestMethod]
        public void ErrorWithException()
            => Logger.Error(new NotImplementedException());

        [TestMethod]
        public void Fatal()
            => Logger.Fatal(new Exception(nameof(Fatal)));

        [TestMethod]
        public void Information()
            => Logger.Information(nameof(Information));

        [TestMethod]
        public void Warning()
            => Logger.Warning(nameof(Warning));
    }
}
