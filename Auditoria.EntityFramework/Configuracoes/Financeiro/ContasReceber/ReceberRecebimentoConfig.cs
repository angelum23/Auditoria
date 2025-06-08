using System.ComponentModel.DataAnnotations.Schema;
using Auditoria.Dominio.Financeiro.ContasReceber;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.ContasReceber;

public class ReceberRecebimentoConfig : IdentificadorUnidadeConfig<ReceberRecebimento>
{
    public ReceberRecebimentoConfig() : base("idreceberrecebimento", DatabaseGeneratedOption.None)
    {
    }

    public override void Configure(EntityTypeBuilder<ReceberRecebimento> builder)
    {
        base.Configure(builder);

        builder.ToTable("receberrecebimento");

        builder.Property(x => x.CodigoMetodoPagamento)
            .HasColumnName("idmetodopagamento")
            .IsRequired();

        builder.Property(x => x.CodigoMetodoPagamentoConfig)
            .HasColumnName("idmetodopagamentoconfig");

        builder.Property(x => x.NumeroParcelas)
            .HasColumnName("numeroparcelas")
            .IsRequired();

        builder.Property(x => x.NumeroAutorizacao)
            .HasColumnName("numeroautorizacao")
            .HasMaxLength(100);

        builder.Property(x => x.DataRecebimento)
            .HasColumnName("datarecebimento")
            .IsRequired();

        builder.Property(x => x.ValorDevido)
            .HasColumnName("valordevido")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PercentualDesconto)
            .HasColumnName("percentualdesconto")
            .HasPrecision(18, 5)
            .IsRequired();

        builder.Property(x => x.ValorDesconto)
            .HasColumnName("valordesconto")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ValorMulta)
            .HasColumnName("valormulta")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ValorTaxa)
            .HasColumnName("valortaxa")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IncluirTaxaNoValor)
            .HasColumnName("incluirtaxanovalor")
            .IsRequired();

        builder.Property(x => x.ValorCredito)
            .HasColumnName("valorcredito")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ValorRecebido)
            .HasColumnName("valorrecebido")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PercentualMulta)
            .HasColumnName("percentualmulta")
            .HasPrecision(18, 5)
            .IsRequired();

        builder.Property(x => x.CodigoUsuarioRecebimento)
            .HasColumnName("idusuariorecebimento")
            .IsRequired();

        builder.Property(x => x.Saldo)
            .HasColumnName("saldo")
            .HasPrecision(18, 2)
            .IsRequired();

        //

    }
}