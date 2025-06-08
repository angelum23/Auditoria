using Auditoria.Dominio.Financeiro.Vendas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.Vendas;

public class VendaConfig : IdentificadorUnidadeConfig<Venda>
{
    public VendaConfig() : base("idvenda")
    {
    }

    public override void Configure(EntityTypeBuilder<Venda> builder)
    {
        base.Configure(builder);

        builder.ToTable("venda");

        builder.Property(x => x.CodigoCliente)
            .HasColumnName("idpessoa")
            .IsRequired();

        builder.Property(x => x.Data)
            .HasColumnName("data")
            .IsRequired();

        builder.Property(x => x.PercentualDesconto)
            .HasColumnName("percentualdesconto")
            .HasPrecision(18, 5)
            .IsRequired();

        builder.Property(x => x.ValorDesconto)
            .HasColumnName("valordesconto")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ValorTotal)
            .HasColumnName("valortotal")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ValorOriginal)
            .HasColumnName("valororiginal")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.CodigoUsuario)
            .HasColumnName("idusuario")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .IsRequired();
        
        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(4000);

        builder.Property(x => x.Gympass)
            .HasColumnName("gympass")
            .IsRequired();

        builder.Property(x => x.TotalPass)
            .HasColumnName("totalpass")
            .IsRequired();

        builder.Property(x => x.RenovarAutomaticamente)
            .HasColumnName("renovarautomaticamente")
            .IsRequired();

        builder.Property(x => x.Recorrente)
            .HasColumnName("recorrente")
            .IsRequired();

        //
        builder.HasMany(x => x.VendaItem)
            .WithOne()
            .HasForeignKey(x => x.CodigoVenda);

        builder.HasMany(x => x.VendaContrato)
            .WithOne()
            .HasForeignKey(x => x.CodigoVenda);

        builder.HasMany(x => x.VendaParcela)
            .WithOne()
            .HasForeignKey(x => x.CodigoVenda);
    }
}