using Auditoria.Dominio.Financeiro.MovimentosFinanceiros;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.MovimentosFinanceiros;

public class MovtoFinancConfig : IdentificadorUnidadeConfig<MovtoFinanc>
{
    public MovtoFinancConfig() : base("idmovtofinanc")
    {
    }
    
    public override void Configure(EntityTypeBuilder<MovtoFinanc> builder)
        {
            base.Configure(builder);

            builder.ToTable("movtofinanc");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CodigoContaFinanc)
                .HasColumnName("idcontafinanc")
                .IsRequired();

            builder.Property(x => x.DataMovimentacao)
                .HasColumnName("datamovimentacao")
                .IsRequired();

            builder.Property(x => x.DataEfetivacao)
                .HasColumnName("dataefetivacao")
                .IsRequired();

            builder.Property(x => x.Tipo)
                .HasColumnName("tipo")
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnName("valor")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.ValorOriginal)
                .HasColumnName("valororiginal")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.ValorDesconto)
                .HasColumnName("valordesconto")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.ValorMulta)
                .HasColumnName("valormulta")
                .HasPrecision(18,2)
                .IsRequired();

            builder.Property(x => x.ValorTaxa)
                .HasColumnName("valortaxa")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(1000);

            builder.Property(x => x.TipoMetodo)
                .HasColumnName("tipometodo")
                .IsRequired();

            builder.Property(x => x.TipoMetodoConfig)
                .HasColumnName("tipometodoconfig");

            builder.Property(x => x.Antecipado)
                .HasColumnName("antecipado")
                .IsRequired();

            builder.Property(x => x.CodigoUsuario)
                .HasColumnName("idusuario");

            builder.Property(x => x.CodigoCliente)
                .HasColumnName("idpessoa");

            builder.Property(x => x.CodigoFornecedor)
                .HasColumnName("idfornecedor");

            builder.Property(x => x.NumeroAutorizacao)
                .HasColumnName("numeroautorizacao")
                .HasMaxLength(100);

            //
            
        }
}