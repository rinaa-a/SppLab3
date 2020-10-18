using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SppLab3
{
    
    public static class AssemblyBrowser
    {
        public static AssemblyData GetAssemblyData(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            AssemblyData assemblyData = new AssemblyData(assembly.GetName().Name);
            var namespaces = assembly.GetTypes().Select(type => type.Namespace).Distinct().ToList().Where(n => n != null).ToList();
            namespaces.ForEach(n =>
            {
                NamespaceData namespaceData = new NamespaceData(n);
                assemblyData.Namespaces.Add(namespaceData);

                var classes = assembly.GetTypes().Where(type => type.IsClass && type.Namespace == n).ToList();

                classes.ForEach(c =>
                {
                    ClassData classData = new ClassData(c.Name);
                    namespaceData.Classes.Add(classData);

                    var fields = c.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList();
                    var properties = c.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList();
                    var methods = c.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                        .Where(m => !m.IsDefined(typeof(ExtensionAttribute))).ToList();

                    fields.ForEach(f => classData.Members.First(m => m.Name == "Fields").Items.Add(f.ToString()));
                    properties.ForEach(p => classData.Members.First(m => m.Name == "Properties").Items.Add(p.ToString()));
                    methods.ForEach(method => classData.Members.First(m => m.Name == "Methods").Items.Add(method.ToString()));
                });

                classes.ForEach(c =>
                {
                    c.GetMethods().Where(m => m.IsDefined(typeof(ExtensionAttribute), false)).ToList()
                         .ForEach(em =>
                {
                    assemblyData.Namespaces.ToList().ForEach(n =>
                        n.Classes.First(c => c.Name == em.GetParameters()[0].ParameterType.Name)
                        ?.Members.First(m => m.Name == "Methods")
                        .Items.Add("Extension:" + em.ToString()));
                });
                });
            });

            return assemblyData;
        }
    }
}
