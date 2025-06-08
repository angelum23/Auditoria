using System.ComponentModel.DataAnnotations.Schema;
using Auditoria.Dominio.Geral.Pessoas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Geral.Pessoas;

public class PessoaLeadConfig: IdentificadorTenantConfig<PessoaLead>
{
    public PessoaLeadConfig() : base("idpessoa", DatabaseGeneratedOption.None)
    {
    }

    public override void Configure(EntityTypeBuilder<PessoaLead> builder)
    {
        base.Configure(builder);

        builder.ToTable("pessoalead");
    }
}