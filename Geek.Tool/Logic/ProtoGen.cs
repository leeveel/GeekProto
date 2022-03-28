using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Tool.Logic
{
    public class ProtoGen
    {
        /// <summary>
        /// 0: 服务器 1：客户端 2：服务器+客户端
        /// </summary>
        private int exportModel;
        /// <summary>
        /// /
        /// </summary>
        /// <param name="factoryTemplatePath"></param>
        /// <param name="msgTemplatePath"></param>
        /// <param name="dllPath"></param>
        /// <param name="outputPath"></param>
        /// <param name="canUseID">预留DBState(mongodb主键)</param>
        public ProtoGen(int exportModel)
        {
            this.exportModel = exportModel;
        }

        public void Gen()
        {

            var factory = new FactoryTemplate();
            var targetDll = LoadDll();
            var types = targetDll.GetTypes();
            foreach (var type in types)
            {
                var tp = TypeParser.ToTemplate(type);
                if (tp == null)
                    continue;
                factory.sclasses.Add(tp);
                System.Console.WriteLine($"gen {type.Name}");

                if (exportModel == 1 || exportModel == 3)
                {
                    Setting.ServerOutPath.CreateAsDirectory();
                    Template template = Template.Parse(File.ReadAllText(Setting.MessageTemplatePath));
                    Template factoryTemplate = Template.Parse(File.ReadAllText(Setting.FactoryTemplatePath));
                    var str = template.Render(tp);

                    //导出服务器
                    string filePath = Setting.ServerOutPath + type.Name;
                    File.WriteAllText(filePath + ".cs", str);

                    string factoryPath = Setting.ServerOutPath + "SClassFactory.cs";
                    var fstr = factoryTemplate.Render(factory);
                    File.WriteAllText(factoryPath, fstr);
                }
                if (exportModel == 2 || exportModel == 3)
                {
                    Setting.ClientOutPath.CreateAsDirectory();
                    Template template = Template.Parse(File.ReadAllText(Setting.ClientMessageTemplatePath));
                    Template factoryTemplate = Template.Parse(File.ReadAllText(Setting.ClientFactoryTemplatePath));
                    var str = template.Render(tp);

                    //导出客户端
                    string filePath = Setting.ClientOutPath + type.Name;
                    filePath.CreateAsDirectory(true);
                    File.WriteAllText(filePath + ".cs", str);

                    string factoryPath = Setting.ClientOutPath + "SClassFactory.cs";
                    factoryPath.CreateAsDirectory(true);
                    var fstr = factoryTemplate.Render(factory);
                    File.WriteAllText(factoryPath, fstr);
                }
            }
        }

        private Assembly LoadDll()
        {
            var loader = new DllLoader(Setting.DllPath);
            loader.Load();

            //依赖的dll
            var asbArr = AppDomain.CurrentDomain.GetAssemblies();
            var asbList = new List<string>();
            foreach (var asb in asbArr)
                asbList.Add(asb.GetName().Name);
            var refArr = loader.TargetDll.GetReferencedAssemblies();
            foreach (var asb in refArr)
            {
                if (asbList.Contains(asb.Name))
                    continue;
                //var refPath = Environment.CurrentDirectory + $"/{asb.Name}.dll";
                var refPath = Path.GetDirectoryName(Setting.DllPath) + $"/{asb.Name}.dll";
                if (File.Exists(refPath))
                    Assembly.LoadFrom(refPath);
            }

            return loader.TargetDll;
        }

    }
}
