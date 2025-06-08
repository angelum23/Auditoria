using Auditoria.Dominio.Geral.Pessoas;
using Auditoria.EntityFramework.Configuracoes.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Geral.Pessoas;

public class PessoaConfig : IdentificadorTenantConfig<Pessoa>
    {
        public PessoaConfig() : base("idpessoa")
        {
        }

        public override void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            base.Configure(builder);

            builder.ToTable("pessoa");

            builder.Property(x => x.Tipo)
                .HasColumnName("tipo")
                .IsRequired();

            builder.Property(x => x.Inativo)
                .HasColumnName("inativo")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(100);

            builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Cpf)
                .HasColumnName("cpf")
                .HasMaxLength(11);

            builder.Property(x => x.Rg)
                .HasColumnName("RG")
                .HasMaxLength(20);

            builder.Property(x => x.DataNascimento)
                .HasColumnName("datanascimento");

            builder.Property(x => x.Sexo)
                .HasColumnName("sexo");

            builder.Property(x => x.Cep)
                .HasColumnName("cep")
                .HasMaxLength(8);

            builder.Property(x => x.Endereco)
                .HasColumnName("endereco")
                .HasMaxLength(100);

            builder.Property(x => x.NumEndereco)
                .HasColumnName("numendereco")
                .HasMaxLength(10);

            builder.Property(x => x.Bairro)
                .HasColumnName("bairro")
                .HasMaxLength(100);

            builder.Property(x => x.CompleEndereco)
                .HasColumnName("compleendereco")
                .HasMaxLength(100);

            builder.Property(x => x.Observacao)
                .HasColumnName("observacao");

            builder.Property(x => x.DddFone)
                .HasColumnName("dddfone");

            builder.Property(x => x.Fone)
                .HasColumnName("fone");

            builder.Property(x => x.CodigoCidade)
                .HasColumnName("idcidade");

            builder.Property(x => x.DataCadastro)
                .HasColumnName("datacadastro")
                .IsRequired();

            builder.Property(x => x.CodigoObjetivo)
                .HasColumnName("idobjetivo");

            builder.Property(x => x.TemResponsavel)
                .HasColumnName("temresponsavel")
                .IsRequired();

            builder.Property(x => x.CodigoClienteResponsavel)
                .HasColumnName("idpessoaresponsavel");
            
            builder.Property(x => x.CodigoUsuarioConsultor)
                .HasColumnName("idusuarioconsultor");
            
            //

            builder.HasOne(x => x.Cliente)
                .WithOne(x => x.Pessoa)
                .HasForeignKey<PessoaCliente>(x => x.Id);

            builder.HasOne(x => x.Lead)
                .WithOne(x => x.Pessoa)
                .HasForeignKey<PessoaLead>(x => x.Id);

            builder.HasOne(x => x.Usuario)
                .WithOne(x => x.Pessoa)
                .HasForeignKey<PessoaUsuario>(x => x.Id);


            builder.HasIndex(x => new { x.CodigoTenant, x.Tipo });
        }
    }