using System.Threading;
using System.Threading.Tasks;

namespace Exo.Rp.Core.Tasks
{
    public interface ITask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}