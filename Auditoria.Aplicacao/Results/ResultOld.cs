namespace Auditoria.Aplicacao.Results;

public class ResultOld<T>
{
    private ResultOld(bool isSuccess, Erro error, T? content)
    {
        if (isSuccess && error != Erro.None ||
            !isSuccess && error == Erro.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Content = content;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;
    public T? Content { get; }

    public Erro Error { get; }

    public static ResultOld<T> Success(T content) => new(true, Erro.None, content);

    public static ResultOld<T?> Failure(Erro error) => new(false, error, default!);
}