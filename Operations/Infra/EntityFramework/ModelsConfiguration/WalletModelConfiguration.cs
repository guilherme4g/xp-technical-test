using Domain.Entities;
using Infra.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.ModelsConfiguration
{
    public class WalletModelConfiguration : IEntityTypeConfiguration<WalletModel>
    {
        public void Configure(EntityTypeBuilder<WalletModel> builder)
        {

            builder.Property(wallet => wallet.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.HasKey(wallet => wallet.Id);
            builder.Property(wallet => wallet.Name).IsRequired(true);
            builder.Property(wallet => wallet.UserId).IsRequired(true);
            builder.Property(wallet => wallet.Balance).IsRequired(true);
            builder.Property(wallet => wallet.Standart).IsRequired(true);
            builder.Property(wallet => wallet.Active).IsRequired(true);

            builder.Property(wallet => wallet.CurrencyId).IsRequired(true);
            builder.HasOne(wallet => wallet.Currency)
                .WithMany()
                .HasForeignKey(wallet => wallet.CurrencyId)
                .IsRequired(true);

            builder.HasData(new WalletModel(Guid.Parse("445500db-fada-4b31-8300-5ebe9a1c5bde"), "Carteira Principal" , Guid.Parse("85f80757-e64e-4e9d-956c-442137af0311"), Guid.Parse("ae3b7f39-303b-427d-8e57-a5bbd9cf52d3"), 5000, true, true));
        }
    }
}
