using Quartz;

namespace Girteka.ElectricAggregate.Job;

public interface IElectricityDataDownloaderJob<TInput, TResult> : IJob
{
    void Do(TInput param);
}