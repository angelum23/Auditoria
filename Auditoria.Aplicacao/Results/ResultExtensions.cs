namespace Auditoria.Aplicacao.Results;

public static class ResultExtensions
{
    public static T Match<T>(
        this ResultOld<T?> resultOld,
        Func<T> onSuccess,
        Func<Erro, T> onFailure)
    {
        return resultOld.IsSuccess ? onSuccess() : onFailure(resultOld.Error);
    }
}