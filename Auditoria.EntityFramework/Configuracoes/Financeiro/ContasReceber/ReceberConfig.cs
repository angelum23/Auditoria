using Auditoria.Dominio.Financeiro.ContasReceber;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.ContasReceber;

public class ReceberConfig : IdentificadorUnidadeConfig<Receber>
{
    public ReceberConfig() : base("idreceber")
    {
    }
    
    public override void Configure(EntityTypeBuilder<Receber> builder)
        {
            base.Configure(builder);

            builder.ToTable("receber");

            builder.Property(x => x.CodigoCliente)
                .HasColumnName("idpessoa")
                .IsRequired();
                
            builder.Property(x => x.CodigoTipoReceber)
                .HasColumnName("idtiporeceber");

            builder.Property(x => x.DataHora)
                .HasColumnName("datahora")
                .IsRequired();

            builder.Property(x => x.DataVencimento)
                .HasColumnName("datavencimento")
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnName("valor")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Observacao)
                .HasColumnName("observacao")
                .HasMaxLength(4000);

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(x => x.CodigoUsuario)
                .HasColumnName("idusuario")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(4000);

            builder.Property(x => x.Recorrente)
                .HasColumnName("recorrente")
                .IsRequired();

            //

            builder.HasMany(x => x.ReceberOrigem)
                .WithOne(x => x.Receber)
                .HasForeignKey(x => x.CodigoReceber);

            builder.HasOne(x => x.ReceberRecebimento)
                .WithOne(x => x.Receber)
                .HasForeignKey<ReceberRecebimento>(x => x.Id);
            
        }
}