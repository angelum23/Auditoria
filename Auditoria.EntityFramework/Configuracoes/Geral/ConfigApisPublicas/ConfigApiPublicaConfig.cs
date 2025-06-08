using Auditoria.Dominio.Geral.ConfigApisPublicas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Geral.ConfigApisPublicas;

public class ConfigAuditoriaConfig : IdentificadorUnidadeConfig<ConfigAuditoria>
{
    public ConfigAuditoriaConfig() : base("idconfigauditoria")
    {
    }
    
    public override void Configure(EntityTypeBuilder<ConfigAuditoria> builder)
    {
        base.Configure(builder);

        builder.ToTable("configauditoria");

        builder.Property(x => x.Habilitado)
            .HasColumnName("habilitado")
            .IsRequired();

        builder.Property(x => x.DataLimiteTrial)
            .HasColumnName("datalimitetrial");

        builder.Property(x => x.LimiteRequisicoesTrial)
            .HasColumnName("requisicoeslimitetrial");
        
        builder.Property(x => x.RequisicoesEfetuadas)
            .HasColumnName("requisicoesefetuadas");
        
        builder.HasMany(x => x.Chaves)
            .WithOne(x => x.ConfigAuditoria)
            .HasForeignKey(x => x.CodigoConfigAuditoria);
    }
}