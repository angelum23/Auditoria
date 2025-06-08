using System.ComponentModel.DataAnnotations.Schema;
using Auditoria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Entidades;

public class IdentificadorUnidadeConfig<T>: IdentificadorTenantConfig<T> where T : IdentificadorUnidade
{
    public IdentificadorUnidadeConfig(string nomePk, DatabaseGeneratedOption dbDatabaseGeneratedOptionPk = DatabaseGeneratedOption.Identity) : base(nomePk, dbDatabaseGeneratedOptionPk)
    {
    }

    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.CodigoUnidade)
            .HasColumnName("idunidade")
            .IsRequired();

        builder.HasIndex(x => x.CodigoUnidade);

        builder.HasOne(x => x.Unidade)
            .WithMany()
            .HasForeignKey(x => x.CodigoUnidade);
    }
}