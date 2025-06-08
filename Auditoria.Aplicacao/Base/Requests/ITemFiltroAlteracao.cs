namespace Auditoria.Aplicacao.Base.Requests;

public interface ITemFiltroAlteracao
{
    DateTime? DataAlteracaoInicio { get; set; }
    DateTime? DataAlteracaoFim { get; set; }
}