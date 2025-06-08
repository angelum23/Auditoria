namespace Auditoria.Aplicacao.Base.DTOs;

public class IdentificadorView
{
    public int Id { get; set; }
    
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
    public int? CodigoUsuarioCriacao { get; set; }
    public int? CodigoUsuarioAlteracao { get; set; }
}