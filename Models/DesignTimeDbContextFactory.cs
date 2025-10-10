using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MfApisWebServicesFuelManager.Models
{
    // Factory usada pelo EF Core CLI para instanciar o AppDbContext em design-time
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // Usar a instância padrão do SQL Server local (Windows Authentication)
            // Adicionamos TrustServerCertificate=True para evitar falhas de validação de certificado
            optionsBuilder.UseSqlServer("Server=.;Database=mf_fuel_db;Trusted_Connection=True;TrustServerCertificate=True;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
