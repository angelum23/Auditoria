using Auditoria.Dominio.LogsAuditoria;
using Auditoria.Mongo.LogsAuditoria;
using Auditoria.MongoDbTest.Base;
using Auditoria.TestInfra;
using AutoFixture;
using MongoDB.Driver;
using Xunit;

namespace Auditoria.MongoDbTest.LogsAuditoria;

public class RepLogAuditoriaTest : RepTestBase
{
    [Fact]
    public async Task CreateAsync_QuandoEntidadeValida_DeveInserirNoBancoEAtribuirId()
    {
        // Arrange
        var repositorio = new RepLogAuditoria(_database);
        var fixture = AutoFixtureHelper.MontarFixture();
        var logParaInserir = fixture.Create<LogAuditoria>();

        // Act
        var logResultado = await repositorio.CreateAsync(logParaInserir);

        // Assert
        Assert.NotNull(logResultado);
        Assert.NotEqual(default, logResultado.Id);
        Assert.Equal(logResultado.Id.CreationTime.Date, logResultado.DataCriacao.Date);
        
        var colecao = _database.GetCollection<LogAuditoria>(nameof(LogAuditoria));
        var logSalvoNoBanco = await colecao.Find(l => l.Id == logResultado.Id).SingleOrDefaultAsync();

        Assert.NotNull(logSalvoNoBanco);
        Assert.Equal(logResultado.Id, logSalvoNoBanco.Id);
        Assert.Equal(logResultado.DataCriacao, logSalvoNoBanco.DataCriacao);
    }
}