using Auditoria.Dominio.Geral.ConfigApisPublicas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Geral.ConfigApisPublicas;

public class ConfigAuditoriaChaveConfig : IdentificadorUnidadeConfig<ConfigAuditoriaChave>
{
    public ConfigAuditoriaChaveConfig() : base("idconfigauditoriachave")
    {
    }
    
    public override void Configure(EntityTypeBuilder<ConfigAuditoriaChave> builder)
    {
        base.Configure(builder);

        builder.ToTable("configauditoriachave");

        builder.Property(x => x.Chave)
            .HasColumnName("chave")
            .HasMaxLength(68)
            .IsRequired();
        
        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(100);
        
        builder.Property(x => x.CodigoUsuario)
            .HasColumnName("idusuario");
            
        builder.Property(x => x.CodigoConfigAuditoria)
            .HasColumnName("idconfigauditoria");
        
        builder.Property(x => x.Inativo)
            .HasColumnName("inativo")
            .IsRequired();
        
        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.CodigoUsuario);
        
        builder.HasOne(x => x.ConfigAuditoria)
            .WithMany(x => x.Chaves)
            .HasForeignKey(x => x.CodigoConfigAuditoria);

    }
    
}