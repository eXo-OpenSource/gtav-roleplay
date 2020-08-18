using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Exo.Rp.Core.Util;

namespace Exo.Rp.Core.Updateable
{
    public class InvalidIUpdateableTargetException : Exception
    {
        public InvalidIUpdateableTargetException(string message = null) : base(message) { }
    }

    public interface IUpdateable
    {
        public void Tick();
    }

    public class UpdateableManager : IService
    {
        private const string UpdateMethodName = "Tick";
        private const double Throttle = 0.1 * 1000; // 100ms

        private readonly List<Type> _updateables;
        private DateTime _lastTick = DateTime.UtcNow;

        public UpdateableManager(RuntimeIndexer indexer)
        {
            _updateables = indexer.IndexImplementsInterface<IUpdateable>(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterface(typeof(IManager).Name) != default)
                .ToList();
        }

        public void Tick()
        {
            if ((DateTime.UtcNow - _lastTick).TotalMilliseconds < Throttle) return;

            // Decouple operation to own thread, so the main thread will not be blocked
            Task.Run(() =>
            {
                try
                {
                    _updateables.ForEach(t =>
                    {
                        var method = t.GetMethod(UpdateMethodName);
                        if (method == default) return;

                        var instance = Core.GetService(t);
                        if (instance != default)
                        {
                            method.Invoke(instance, null);
                        }
                        else
                        {
                            _updateables.Remove(t);
                            throw new InvalidIUpdateableTargetException(
                                $"Instance of Manager {t.FullName} has not been found.");
                        }
                    });
                }
                catch (Exception e)
                {
                    (e.InnerException ?? e).TrackOrThrow();
                }
                finally
                {
                    _lastTick = DateTime.UtcNow;
                }
            });
        }
    }
}