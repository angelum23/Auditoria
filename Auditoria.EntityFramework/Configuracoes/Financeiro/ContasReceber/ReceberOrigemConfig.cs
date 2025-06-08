using Auditoria.Dominio.Financeiro.ContasReceber;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.ContasReceber;

public class ReceberOrigemConfig : IdentificadorUnidadeConfig<ReceberOrigem>
{
    public ReceberOrigemConfig() : base("idreceberorigem")
    {
    }
    
    public override void Configure(EntityTypeBuilder<ReceberOrigem> builder)
    {
        base.Configure(builder);

        builder.ToTable("receberorigem");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CodigoReceber)
            .HasColumnName("idreceber")
            .IsRequired();

        builder.Property(x => x.Origem)
            .HasColumnName("origem")
            .IsRequired();

        builder.Property(x => x.CodigoOrigem)
            .HasColumnName("idorigem")
            .IsRequired();

        builder.Property(x => x.PercVenda)
            .HasColumnName("percvenda")
            .HasPrecision(18, 5)
            .IsRequired();

        builder.Property(x => x.CodigoCategoriaReceita)
            .HasColumnName("idcategoriareceita");

        builder.HasOne(x => x.Receber)
            .WithMany(x => x.ReceberOrigem)
            .HasForeignKey(x => x.CodigoReceber);
        
    }
}