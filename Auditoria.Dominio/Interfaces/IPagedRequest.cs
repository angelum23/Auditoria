namespace Auditoria.Dominio.Interfaces;

public interface IPagedRequest
{
    int Skip { get; set; }
    int Take { get; set; }
}