namespace Auditoria.Aplicacao.Results;

public sealed record Erro(string Codigo, string Descricao)
{
    public static readonly Erro None = new(string.Empty, string.Empty);
    public static readonly Erro ErroInterno = new("0", "Ocorreu um erro ao efetuar a operação");
    public static readonly Erro RegistroNaoEncontrado = new("1", "Registro não encontrado");
    public static readonly Erro ApiKeyNaoInformada = new("100", "API Key não informada");
    public static readonly Erro ApiKeyInvalida = new("101", "API Key inválida");
    public static readonly Erro FalhaNaAutenticacao = new("102", "Falha na autenticação");
}