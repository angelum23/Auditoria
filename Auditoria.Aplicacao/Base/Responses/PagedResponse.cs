namespace Auditoria.Aplicacao.Base.Responses;

public class PagedResponse<T> : ListResponse<T>, IPagedResponse<T>
{
    public bool TemProximaPagina { get; set; }
    
    public PagedResponse()
    {

    }
    public PagedResponse(bool temProximaPagina, IReadOnlyList<T> items)
        : base(items)
    {
        TemProximaPagina = temProximaPagina;
    }
}