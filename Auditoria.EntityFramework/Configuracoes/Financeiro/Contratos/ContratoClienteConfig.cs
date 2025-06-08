using Auditoria.Dominio.Financeiro.Contratos;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Financeiro.Contratos;

public class ContratoClienteConfig : IdentificadorUnidadeConfig<ContratoCliente>
{
    public ContratoClienteConfig() : base("idcontratocliente")
    {
    }

    public override void Configure(EntityTypeBuilder<ContratoCliente> builder)
    {
        base.Configure(builder);

        builder.ToTable("contratocliente");

        builder.Property(x => x.CodigoCliente)
            .HasColumnName("idpessoa")
            .IsRequired();

        builder.Property(x => x.CodigoContratoBase)
            .HasColumnName("idcontratobase")
            .IsRequired();

        builder.Property(x => x.CodigoMotivoEncerramentoContrato)
            .HasColumnName("idmotivoencerramentocontrato");

        builder.Property(x => x.DataInicio)
            .HasColumnName("datainicio")
            .IsRequired();

        builder.Property(x => x.DataBloqueio)
            .HasColumnName("databloqueio");

        builder.Property(x => x.DataRenovacao)
            .HasColumnName("datarenovacao");

        builder.Property(x => x.DataEncerramento)
            .HasColumnName("dataencerramento");

        builder.Property(x => x.DataValidade)
            .HasColumnName("datavalidade")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(x => x.Tipo)
            .HasColumnName("tipo")
            .IsRequired();

        builder.Property(x => x.TipoDuracao)
            .HasColumnName("tipoduracao")
            .IsRequired();

        builder.Property(x => x.TempoDuracao)
            .HasColumnName("tempoduracao")
            .IsRequired();

        builder.Property(x => x.ValorOriginal)
            .HasColumnName("valororiginal")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ValorAdesao)
            .HasColumnName("valoradesao")
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

        builder.Property(x => x.DataSuspensao)
            .HasColumnName("datasuspensao");

        builder.Property(x => x.Recorrente)
            .HasColumnName("recorrente")
            .IsRequired();
        
        //

        builder.HasMany(x => x.Modalidades)
            .WithOne(x => x.ContratoCliente)
            .HasForeignKey(x => x.CodigoContratoCliente);
    }
}