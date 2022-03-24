using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Tool.Logic
{
    public class ProtoGen
    {
        private string dllPath;
        private string outputPath;
        private string templatePath;
        private string mainTemplatePath;
        private bool canUseId;
        /// <summary>
        /// /
        /// </summary>
        /// <param name="mainTemplatePath"></param>
        /// <param name="templatePath"></param>
        /// <param name="dllPath"></param>
        /// <param name="outputPath"></param>
        /// <param name="canUseID">预留DBState(mongodb主键)</param>
        public ProtoGen(string mainTemplatePath, string templatePath, string dllPath, string outputPath, bool canUseID)
        {
            this.dllPath = dllPath;
            this.outputPath = outputPath;
            this.templatePath = templatePath;
            canUseId = canUseID;
            this.mainTemplatePath = mainTemplatePath;
        }

        public void Gen()
        {
            outputPath.CreateAsDirectory();
            Template template = Template.Parse(File.ReadAllText(templatePath));
            Template factoryTemplate = Template.Parse(File.ReadAllText(mainTemplatePath));

            var factory = new FactoryTemplate();
            var targetDll = LoadDll();
            var types = targetDll.GetTypes();
            foreach (var type in types)
            {
                var tp = TypeParser.ToTemplate(type, canUseId);
                if (tp == null)
                    continue;
                factory.sclasses.Add(tp);
                System.Console.WriteLine($"gen {type.Name}");
                var str = template.Render(tp);
                string filePath = outputPath + type.Name;
                File.WriteAllText(filePath + ".cs", str);
            }

            string factoryPath = outputPath + "SClassFactory.cs";
            var fstr = factoryTemplate.Render(factory);
            File.WriteAllText(factoryPath, fstr);
        }

        private Assembly LoadDll()
        {
            var loader = new DllLoader(dllPath);
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
                var refPath = Environment.CurrentDirectory + $"/{asb.Name}.dll";
                if (File.Exists(refPath))
                    Assembly.LoadFrom(refPath);
            }

            return loader.TargetDll;
        }

    }
}
