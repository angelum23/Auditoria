using Auditoria.Dominio.Geral.Tenants;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Geral.Tenants;

public class TenantConfig : IdentificadorConfig<Tenant>
{
    public TenantConfig() : base("idtenant")
    {
    }

    public override void Configure(EntityTypeBuilder<Tenant> builder)
    {
        base.Configure(builder);

        builder.ToTable("tenant");

        builder.Property(x => x.Inativo)
            .HasColumnName("inativo")
            .IsRequired();

        builder.Property(x => x.UtilizaMultiUnidade)
            .HasColumnName("utilizamultiunidade")
            .IsRequired();
    }
}