using ProcessMining.Core.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Infra.Tools.Reflections
{
    public static class Assemblies
    {
        public static List<Type> GetServices(string assemblyName, Type attribute)
        {
            var services = new List<Type>();

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();
            var referencedPaths = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList().Where(a => a.FullName.Contains(assemblyName));

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(c => c.IsClass && c.IsDefined(attribute));
                foreach (var type in types)
                {
                    services.Add(type);
                }
            }

            return services;

        }
    }
}
