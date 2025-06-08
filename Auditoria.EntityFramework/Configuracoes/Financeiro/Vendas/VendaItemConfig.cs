using Auditoria.Dominio.Financeiro.Vendas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.Vendas;

public class VendaItemConfig : IdentificadorUnidadeConfig<VendaItem>
{
    public VendaItemConfig() : base("idvendaitem")
    {
    }

    public override void Configure(EntityTypeBuilder<VendaItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("vendaitem");

        builder.Property(x => x.CodigoVenda)
            .HasColumnName("idvenda")
            .IsRequired();

        builder.Property(x => x.CodigoItem)
            .HasColumnName("iditem")
            .IsRequired();

        builder.Property(x => x.Qtde)
            .HasColumnName("qtde")
            .HasPrecision(18, 5)
            .IsRequired();

        builder.Property(x => x.Preco)
            .HasColumnName("preco")
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