using Auditoria.Dominio.Financeiro.Vendas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.Vendas;

public class VendaContratoConfig : IdentificadorUnidadeConfig<VendaContrato>
{
    public VendaContratoConfig() : base("idvendacontrato")
    {
    }

    public override void Configure(EntityTypeBuilder<VendaContrato> builder)
    {
        base.Configure(builder);

        builder.ToTable("vendacontrato");

        builder.Property(x => x.CodigoVenda)
            .HasColumnName("idvenda")
            .IsRequired();

        builder.Property(x => x.CodigoContratoBase)
            .HasColumnName("idcontratobase")
            .IsRequired();

        builder.Property(x => x.DataInicio)
            .HasColumnName("datainicio")
            .IsRequired();

        builder.Property(x => x.ValorOriginal)
            .HasColumnName("valororiginal")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PercentualDesconto)
            .HasColumnName("percentualdesconto")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.ValorDesconto)
            .HasColumnName("valordesconto")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ValorTotal)
            .HasColumnName("valortotal")
            .HasPrecision(18, 2)
            .IsRequired();

        //

    }
}