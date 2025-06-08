using System.ComponentModel.DataAnnotations.Schema;
using Auditoria.Dominio.Geral.Pessoas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Geral.Pessoas;

public class PessoaClienteConfig : IdentificadorTenantConfig<PessoaCliente>
{
    public PessoaClienteConfig() : base("idpessoa", DatabaseGeneratedOption.None)
    {
    }
    
    public override void Configure(EntityTypeBuilder<PessoaCliente> builder)
    {
        base.Configure(builder);

        builder.ToTable("pessoacliente");

        builder.Property(x => x.CodigoUsuarioProfessor)
            .HasColumnName("idusuarioprofessor");

        //

    }
}