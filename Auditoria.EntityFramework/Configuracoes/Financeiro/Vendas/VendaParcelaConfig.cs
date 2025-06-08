using Auditoria.Dominio.Financeiro.Vendas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.Vendas;

public class VendaParcelaConfig : IdentificadorUnidadeConfig<VendaParcela>
{
    public VendaParcelaConfig() : base("idvendaparcela")
    {
    }

    public override void Configure(EntityTypeBuilder<VendaParcela> builder)
    {
        base.Configure(builder);

        builder.ToTable("vendaparcela");

        builder.Property(x => x.CodigoVenda)
            .HasColumnName("idvenda")
            .IsRequired();

        builder.Property(x => x.Numero)
            .HasColumnName("numero")
            .IsRequired();

        builder.Property(x => x.DataVencimento)
            .HasColumnName("datavencimento")
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasColumnName("valor")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CodigoReceber)
            .HasColumnName("idreceber");

        //

    }
}