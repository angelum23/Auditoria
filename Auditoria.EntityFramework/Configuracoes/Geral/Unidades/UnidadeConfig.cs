using Auditoria.Dominio.Geral.Unidades;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Geral.Unidades;

public class UnidadeConfig : IdentificadorTenantConfig<Unidade>
    {
        public UnidadeConfig() : base("idunidade")
        {
        }

        public override void Configure(EntityTypeBuilder<Unidade> builder)
        {
            base.Configure(builder);

            builder.ToTable("unidade");

            builder.Property(x => x.Inativo)
                .HasColumnName("inativo")
                .IsRequired();
            
            builder.Property(x => x.FusoHorario)
                .HasColumnName("fusohorario")
                .IsRequired();
            
        }
    }