using Microsoft.Extensions.Hosting;
using FirstCatering.Lib.AspNetCore.Extensions;

namespace FirstCatering.Api
{
    public class Program
    {
        /// <summary>
        /// Main Api entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
            => Host.CreateDefaultBuilder().Run<Startup>();
    }
}
