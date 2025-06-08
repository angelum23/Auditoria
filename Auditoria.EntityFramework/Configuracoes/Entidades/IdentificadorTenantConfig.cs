using System.ComponentModel.DataAnnotations.Schema;
using Auditoria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Entidades;

public class IdentificadorTenantConfig<T> : IdentificadorConfig<T> where T : IdentificadorTenant
{
    public IdentificadorTenantConfig(string nomePk, DatabaseGeneratedOption dbDatabaseGeneratedOptionPk = DatabaseGeneratedOption.Identity)
        : base(nomePk, dbDatabaseGeneratedOptionPk)
    {
    }

    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.CodigoTenant)
            .HasColumnName("idtenant")
            .IsRequired();

        builder.HasIndex(x => x.CodigoTenant);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.CodigoTenant);
    }
}