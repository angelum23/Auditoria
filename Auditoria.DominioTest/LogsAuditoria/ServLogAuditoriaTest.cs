using Auditoria.Dominio.Auditaveis;
using Auditoria.Dominio.Interfaces;
using Auditoria.TestInfra;
using AutoFixture;
using FakeItEasy;
using Xunit;

namespace Auditoria.DominioTest.LogsAuditoria;

public class ServLogAuditoriaTest
{
    [Fact]
    public async Task Inserir_DeveChamarRepositorioRetornandoRegistro()
    {
        //arrange
        var fixture = AutoFixtureHelper.MontarFixture();
        var repLogAuditoria = fixture.Freeze<Fake<IRepLogAuditoria>>();
        var servLogAuditoria = fixture.Create<ServLogAuditoria>();

        var logAuditoria = A.Fake<LogAuditoria>();
        
        //act
        var retorno = await servLogAuditoria.Inserir(logAuditoria);
        
        //assert
        A.CallTo(() => repLogAuditoria.FakedObject.CreateAsync(A<LogAuditoria>._)).MustHaveHappenedOnceExactly();
    }
}