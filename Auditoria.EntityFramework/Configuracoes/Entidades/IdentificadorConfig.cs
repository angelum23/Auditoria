using System.ComponentModel.DataAnnotations.Schema;
using Auditoria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.EntityFramework.Configuracoes.Entidades;

public class IdentificadorConfig<T> : IEntityTypeConfiguration<T> where T : Identificador
{
    public string NomePk { get; set; }
    public DatabaseGeneratedOption DbDatabaseGeneratedOptionPk { get; set; }
    public IdentificadorConfig(string nomePk, DatabaseGeneratedOption dbDatabaseGeneratedOptionPk = DatabaseGeneratedOption.Identity)
    {
        NomePk = nomePk;
        DbDatabaseGeneratedOptionPk = dbDatabaseGeneratedOptionPk;
    }

    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        if (DbDatabaseGeneratedOptionPk != DatabaseGeneratedOption.Identity)
        {
            builder.Property(x => x.Id)
                .HasColumnName(NomePk)
                .IsRequired();
        }
        else
        {
            builder.Property(x => x.Id)
                .HasColumnName(NomePk)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }

        builder.Property(x => x.DataCriacao)
            .HasColumnName("datacriacao")
            .IsRequired();

        builder.Property(x => x.DataAlteracao)
            .HasColumnName("dataalteracao")
            .IsRequired();

        builder.Property(x => x.CodigoUsuarioCriacao)
            .HasColumnName("idusuariocriacao");

        builder.Property(x => x.CodigoUsuarioAlteracao)
            .HasColumnName("idusuarioalteracao");
    }
}