using Auditoria.Dominio.Financeiro.Contratos;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.Contratos;

public class ContratoClienteModalidadeConfig : IdentificadorUnidadeConfig<ContratoClienteModalidade>
{
    public ContratoClienteModalidadeConfig()
        : base("idcontratoclientemodalidade")
    {
    }

    public override void Configure(EntityTypeBuilder<ContratoClienteModalidade> builder)
    {
        base.Configure(builder);

        builder.ToTable("contratoclientemodalidade");

        builder.Property(x => x.CodigoContratoCliente)
            .HasColumnName("idcontratocliente")
            .IsRequired();
        
        builder.Property(x => x.Tipo)
            .HasColumnName("tipo")
            .IsRequired();

        builder.Property(x => x.CodigoContratoCliente)
            .HasColumnName("idcontratocliente")
            .IsRequired();

        builder.Property(x => x.CodigoModalidade)
            .HasColumnName("idmodalidade")
            .IsRequired();

        //

    }
}