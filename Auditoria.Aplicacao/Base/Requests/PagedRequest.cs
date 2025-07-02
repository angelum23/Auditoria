using System.ComponentModel.DataAnnotations;

namespace Auditoria.Aplicacao.Base.Requests;

public class PagedRequest : IPagedRequest, IValidatableObject
{   
    private const int MaximoDeResultadosPorRequest = 100;
    
    [Range(0, int.MaxValue)]
    public int Skip { get; set; }
    
    [Range(1, int.MaxValue)]
    public virtual int Take { get; set; } = MaximoDeResultadosPorRequest;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Take > MaximoDeResultadosPorRequest)
        {
            yield return new ValidationResult(
                $"Total de registros solicitados maior que o máximo permitido de {MaximoDeResultadosPorRequest}",
                new[] { nameof(Take) });
        }
    }
}