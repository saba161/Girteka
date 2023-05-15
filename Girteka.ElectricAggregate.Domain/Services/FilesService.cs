namespace Girteka.ElectricAggregate.Domain.Services;

public class FilesService
{
    private readonly IContext<string, Stream> _httpContext;
    private readonly IContext<string, Stream> _localContext;

    public FilesService(IContext<string, Stream> httpContext, IContext<string, Stream> localContext)
    {
        _httpContext = httpContext;
        _localContext = localContext;
    }

    public void Execute()
    {
        _httpContext.Do("sdf");
        _localContext.Do("sdf");
    }
}