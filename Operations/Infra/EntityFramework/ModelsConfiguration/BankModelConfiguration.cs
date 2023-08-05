using Infra.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.ModelsConfiguration
{
    public class BankModelConfiguration : IEntityTypeConfiguration<BankModel>
    {
        public void Configure(EntityTypeBuilder<BankModel> builder)
        {
            builder.Property(bank => bank.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.HasKey(bank => bank.Id);
            builder.Property(bank => bank.Name).IsRequired(true);
            builder.Property(bank => bank.Ispb).IsRequired(true);
            builder.Property(bank => bank.Active).IsRequired(true);
            builder.Property(bank => bank.Blocked).IsRequired(true);

            builder.HasData(new BankModel(Guid.Parse("b8bb4a7e-aadc-457d-9b16-e75de2e87bda"), "Banco XP s.a.", "33264668", true, false));
        }
    }
}
