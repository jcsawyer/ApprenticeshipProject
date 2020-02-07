using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FirstCatering.Lib.IoC;
using FirstCatering.Lib.Logging;
using FirstCatering.Lib.Security.Hashing;
using FirstCatering.Lib.Security.Jwt;

namespace FirstCatering.Lib.Tests.IoC
{
    [TestClass]
    public class ServiceCollectionExtensionsTests
    {
        [TestMethod]
        public void AddHash()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IHash>(_ => new Hash(10000, 128));

            var hash = services.BuildServiceProvider().GetService<IHash>();

            Assert.IsNotNull(hash);
            Assert.IsInstanceOfType(hash, typeof(IHash));
        }

        [TestMethod]
        public void AddLogger()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ILogger, Logger>();

            var logger = services.BuildServiceProvider().GetService<ILogger>();

            Assert.IsNotNull(logger);
            Assert.IsInstanceOfType(logger, typeof(ILogger));
        }

        [TestMethod]
        public void AddJsonWebTokenKeyExpires()
        {
            var services = new ServiceCollection();
            services.AddJsonWebToken(new JsonWebTokenSettings("jwt", TimeSpan.FromHours(4)));

            var token = services.BuildServiceProvider().GetService<IJsonWebToken>();

            Assert.IsNotNull(token);
            Assert.IsInstanceOfType(token, typeof(IJsonWebToken));
        }

        [TestMethod]
        public void AddJsonWebTokenKeyExpiresAudienceIssuer()
        {
            var services = new ServiceCollection();
            services.AddJsonWebToken(new JsonWebTokenSettings("jwt", TimeSpan.FromHours(4), "audience", "issuer"));

            var token = services.BuildServiceProvider().GetService<IJsonWebToken>();

            Assert.IsNotNull(token);
            Assert.IsInstanceOfType(token, typeof(IJsonWebToken));
        }

        [TestMethod]
        public void AddJsonWebTokenSettings()
        {
            var settings = new JsonWebTokenSettings("jwt", TimeSpan.FromHours(4), "audience", "issuer");

            var services = new ServiceCollection();
            services.AddJsonWebToken(settings);

            var token = services.BuildServiceProvider().GetService<IJsonWebToken>();

            Assert.IsNotNull(token);
            Assert.IsInstanceOfType(token, typeof(IJsonWebToken));
        }
    }
}