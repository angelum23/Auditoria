namespace Auditoria.Aplicacao.Results;

public readonly struct Result<TValue, TError>
{
    private readonly TValue? _value;
    private readonly TError? _error;

    private Result(TValue value)
    {
        EhErro = false;
        _value = value;
        _error = default;
    }

    private Result(TError error)
    {
        EhErro = true;
        _error = error;
        _value = default;
    }
    
    public bool EhErro { get; }
    public bool EhSucesso => !EhErro;

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    public TResult Match<TResult>(
        Func<TValue, TResult> sucesso,
        Func<TError, TResult> falha) =>
        !EhErro ? sucesso(_value!) : falha(_error!);


}