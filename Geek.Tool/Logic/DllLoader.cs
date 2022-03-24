using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Tool.Logic
{
    public class DllLoader
    {
        class HostAssemblyLoadContext : AssemblyLoadContext
        {
            public HostAssemblyLoadContext() : base(true)
            {
            }

            protected override Assembly Load(AssemblyName name)
            {
                return null;
            }
        }

        string dllPath;
        HostAssemblyLoadContext context;
        public Assembly TargetDll { private set; get; }

        public DllLoader(string dllPath)
        {
            this.dllPath = dllPath;
            context = new HostAssemblyLoadContext();
        }

        public void Load()
        {
            TargetDll = context.LoadFromAssemblyPath(dllPath);
        }

        public WeakReference Unload()
        {
            context.Unload();
            return new WeakReference(context);
        }
    }
}
