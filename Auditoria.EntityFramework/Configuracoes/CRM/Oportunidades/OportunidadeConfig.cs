using Auditoria.Dominio.CRM.Oportunidades;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.CRM.Oportunidades;

public class OportunidadeConfig : IdentificadorUnidadeConfig<Oportunidade>
{
    public OportunidadeConfig() : base("idoportunidade")
    {
    }

    public override void Configure(EntityTypeBuilder<Oportunidade> builder)
    {
        base.Configure(builder);

        builder.ToTable("oportunidade");

        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CodigoPessoa)
            .HasColumnName("idpessoa")
            .IsRequired();

        builder.Property(x => x.CodigoFunilEtapa)
            .HasColumnName("idfuniletapa")
            .IsRequired();

        builder.Property(x => x.Ordem)
            .HasColumnName("ordem")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(x => x.CodigoUsuario)
            .HasColumnName("idusuario")
            .IsRequired();

        builder.Property(x => x.CodigoIndicacao)
            .HasColumnName("idclienteindicado");

        builder.Property(x => x.DataGanhou)
            .HasColumnName("dataganhou");

        builder.Property(x => x.DataPerdeu)
            .HasColumnName("dataperdeu");

        builder.Property(x => x.CodigoMotivoPerda)
            .HasColumnName("idmotivoperda");

        builder.Property(x => x.CodigoNivelInteresse)
            .HasColumnName("idnivelinteresse");

        builder.Property(x => x.CodigoTipoVisita)
            .HasColumnName("idtipovisita");

        builder.Property(x => x.CodigoComoConheceu)
            .HasColumnName("idcomoconheceu");

        builder.Property(x => x.OrigemComoConheceu)
            .HasColumnName("origemcomoconheceu")
            .HasMaxLength(100);
        
        builder.Property(x => x.Inativo)
            .HasColumnName("inativo")
            .IsRequired();

        //
        
    }
}