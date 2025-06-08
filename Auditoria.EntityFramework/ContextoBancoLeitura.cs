using Auditoria.Infra.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Auditoria.EntityFramework;

public class ContextoBancoLeitura: ContextoBanco
{
    public ContextoBancoLeitura(DbContextOptions<ContextoBanco> contexto, IAmbienteHelper ambienteHelper) : base(contexto, ambienteHelper)
    {
    }
    
    public override int SaveChanges()
    {
        throw new Exception("Não é permitido salvar no banco de leitura");
    }

    
}