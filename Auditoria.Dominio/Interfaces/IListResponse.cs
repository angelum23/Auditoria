namespace Auditoria.Aplicacao.Base.Responses;

public interface IListResponse<T>
{
    IReadOnlyList<T> Items { get; set; }
}