using Infra.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.ModelsConfiguration
{
    public class OperationModelConfiguration : IEntityTypeConfiguration<OperationModel>
    {
        public void Configure(EntityTypeBuilder<OperationModel> builder)
        {

            builder.Property(bank => bank.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.HasKey(operation => operation.Id);
            builder.Property(operation => operation.Type).IsRequired(true);
            builder.Property(operation => operation.Value).IsRequired(true);

            builder.Property(operation => operation.OwnerUserId);
            builder.Property(operation => operation.OwnerUserName);
            builder.Property(operation => operation.OwnerUserDocument);
            builder.Property(operation => operation.OwnerAgency).IsRequired();
            builder.Property(operation => operation.OwnerNumber).IsRequired();
            builder.Property(operation => operation.OwnerDigit).IsRequired();
            builder.Property(operation => operation.OwnerBankId).IsRequired();

            builder.Property(operation => operation.BeneficiaryUserId);
            builder.Property(operation => operation.BeneficiaryUserName);
            builder.Property(operation => operation.BeneficiaryUserDocument);
            builder.Property(operation => operation.BeneficiaryAgency).IsRequired();
            builder.Property(operation => operation.BeneficiaryNumber).IsRequired();
            builder.Property(operation => operation.BeneficiaryDigit).IsRequired();
            builder.Property(operation => operation.BeneficiaryBankId).IsRequired();

            builder.Property(operation => operation.CurrencyId).IsRequired(true);
            builder.HasOne(operation => operation.Currency)
                .WithMany()
                .HasForeignKey(operation => operation.CurrencyId)
                .IsRequired(true);
        }
    }
}
