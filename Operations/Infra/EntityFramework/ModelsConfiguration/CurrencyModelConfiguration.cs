using Domain.Entities;
using Infra.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.ModelsConfiguration
{
    public class CurrencyModelConfiguration : IEntityTypeConfiguration<CurrencyModel>
    {
        public void Configure(EntityTypeBuilder<CurrencyModel> builder)
        {
            builder.Property(currency => currency.Id)
               .HasDefaultValueSql("newsequentialid()");

            builder.HasKey(currency => currency.Id);
            builder.Property(currency => currency.Name).IsRequired(true);
            builder.Property(currency => currency.Symbol).IsRequired(true);

            builder.HasData(new CurrencyModel(Guid.Parse("ae3b7f39-303b-427d-8e57-a5bbd9cf52d3"), "Real", "BRL"));
        }
    }
}
