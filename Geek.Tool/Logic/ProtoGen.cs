
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
        /// 1: 服务器 2：客户端 3：服务器+客户端
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

            var resolver = new NestTypeTemplate();
            foreach (var type in types)
            {
                var tp = TypeParser.ToTemplate(type, resolver);

                if (tp == null)
                    continue;
             
                if(!tp.isenum)
                    factory.sclasses.Add(tp);
        
                System.Console.WriteLine($"gen {type.Name}");
           
                if (exportModel == 1 || exportModel == 3)
                {
                   
                    Setting.ServerOutPath.CreateAsDirectory();

      
                    Template template = Template.Parse(File.ReadAllText(Setting.MessageTemplatePath));          
                    Template factoryTemplate = Template.Parse(File.ReadAllText(Setting.FactoryTemplatePath));
                    Template resolverTemplate = Template.Parse(File.ReadAllText(Setting.ResolverTemplatePath));


                    var str = template.Render(tp);
                
                    string filePath = Setting.ServerOutPath + type.Name;
             
                    File.WriteAllText(filePath + ".cs", str);
                     
                    string factoryPath = Setting.ServerOutPath + "SClassFactory.cs";
                    var fstr = factoryTemplate.Render(factory);
                    File.WriteAllText(factoryPath, fstr);

                    string resolverPath = Setting.ServerOutPath + "SerializeResolver.cs";
                    var rstr = resolverTemplate.Render(resolver);
                    File.WriteAllText(resolverPath, rstr);
                }
             
                if (exportModel == 2 || exportModel == 3)
                {
                    Setting.ClientOutPath.CreateAsDirectory();
                    Template template = Template.Parse(File.ReadAllText(Setting.ClientMessageTemplatePath));
                    Template factoryTemplate = Template.Parse(File.ReadAllText(Setting.ClientFactoryTemplatePath));
                    Template resolverTemplate = Template.Parse(File.ReadAllText(Setting.ClientResolverTemplatePath));
                    var str = template.Render(tp);

                    //导出客户端
                    string filePath = Setting.ClientOutPath + type.Name;
                    filePath.CreateAsDirectory(true);
                    File.WriteAllText(filePath + ".cs", str);

                    string factoryPath = Setting.ClientOutPath + "SClassFactory.cs";
                    factoryPath.CreateAsDirectory(true);
                    var fstr = factoryTemplate.Render(factory);
                    File.WriteAllText(factoryPath, fstr);

                    string resolverPath = Setting.ServerOutPath + "SerializeResolver.cs";
                    var rstr = resolverTemplate.Render(resolver);
                    File.WriteAllText(resolverPath, rstr);
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
