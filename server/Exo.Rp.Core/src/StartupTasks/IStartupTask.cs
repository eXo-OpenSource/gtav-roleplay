using System.Threading;
using System.Threading.Tasks;

namespace Exo.Rp.Core.StartupTasks
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}