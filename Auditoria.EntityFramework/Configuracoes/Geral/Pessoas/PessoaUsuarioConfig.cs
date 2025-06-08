using Auditoria.Dominio.Geral.Pessoas;
using Auditoria.Dominio.Shared.Pessoas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Geral.Pessoas;

public class PessoaUsuarioConfig : IdentificadorTenantConfig<PessoaUsuario>
{
    public PessoaUsuarioConfig() : base("idusuario")
    {
    }

    public override void Configure(EntityTypeBuilder<PessoaUsuario> builder)
    {
        base.Configure(builder);

        builder.ToTable("pessoausuario");
        
        builder.Property(x => x.FormaLogin)
            .HasColumnName("formalogin")
            .HasDefaultValue(EnumFormaLogin.Email)
            .IsRequired();
    }
}