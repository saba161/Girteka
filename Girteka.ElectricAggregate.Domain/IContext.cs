namespace Girteka.ElectricAggregate.Domain;

public interface IContext<TInput, TResult>
{
    public TResult Do(TInput input);
}