namespace Auditoria.Aplicacao.Base.Responses;

public interface IPagedResponse<T> : IListResponse<T>
{
    bool TemProximaPagina { get; set; }
}