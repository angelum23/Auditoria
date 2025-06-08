namespace Auditoria.Aplicacao.Base.Responses;

public class ListResponse<T> : IListResponse<T>
{
    private IReadOnlyList<T>? _items;

    public IReadOnlyList<T> Items
    {
        get => _items ??= new List<T>();
        set => _items = value;
    }
    
    public ListResponse(IReadOnlyList<T> items)
    {
        Items = items;
    }

    public ListResponse()
    {
        
    }
}