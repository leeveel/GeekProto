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
            try
            {
                TargetDll = context.LoadFromAssemblyPath(dllPath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"找不到Dll:{dllPath},请确认Proto工程已经编译,{e}");
            }
        }

        public WeakReference Unload()
        {
            context.Unload();
            return new WeakReference(context);
        }
    }
}
