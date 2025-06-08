namespace Auditoria.Aplicacao.Base.Requests;

public interface IPagedRequest
{
    int Skip { get; set; }
    int Take { get; set; }
}