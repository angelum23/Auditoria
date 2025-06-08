using System.ComponentModel.DataAnnotations.Schema;
using Auditoria.Dominio.Financeiro.ContratosBase;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.ContratosBase;

public class ContratoBaseConfig: IdentificadorUnidadeConfig<ContratoBase>
{
    public ContratoBaseConfig() : base("idcontratobase")
    {
    }

    public override void Configure(EntityTypeBuilder<ContratoBase> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("contratobase");
        
        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Inativo)
            .HasColumnName("inativo")
            .IsRequired();
    }
}