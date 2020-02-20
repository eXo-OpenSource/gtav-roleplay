using System.Collections.Generic;
using System.Reflection;
using server.Util;

namespace server.Updateable
{
    public interface IUpdateable { }

    public class UpdateableManager : IService
    {
        private const string UpdateMethodName = "Tick";

        private readonly List<MethodInfo> _updateables = new List<MethodInfo>();

        public UpdateableManager(MethodIndexer indexer)
        {
            indexer.IndexMethods(Assembly.GetExecutingAssembly(), UpdateMethodName,
                method => method.IsStatic && method.IsPublic, method => _updateables.Add(method));
        }

        public void Tick()
        {
            _updateables.ForEach(m => m.Invoke(null, null));
        }
    }
}