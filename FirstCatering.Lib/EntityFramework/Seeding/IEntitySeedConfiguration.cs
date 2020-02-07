using Microsoft.EntityFrameworkCore;

namespace FirstCatering.Lib.EntityFramework
{
    /// <summary>
    /// Seed configuration
    /// </summary>
    public interface IEntitySeedConfiguration
    {
        /// <summary>
        /// Seeds data for the given <paramref name="builder"/>
        /// </summary>
        /// <param name="builder">EF model builder</param>
        void Seed(ModelBuilder builder);
    }
}