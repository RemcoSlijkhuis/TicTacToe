using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TicTacToeShared
{
    /// <summary>
    /// Find's all IPlayerEngine implementations.
    /// Scan in executable directory for assemblies. 
    /// </summary>
    public static class EngineProvider
    {
        public static List<IPlayerEngine> EngineInstances { get; private set; }
        
        static EngineProvider()
        {
            EngineInstances = new List<IPlayerEngine>();
            ScanAndLoad();
        }

        private static void ScanAndLoad()
        {
            foreach (var dllFilename in System.IO.Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory, "*.dll")) 
            {
                var ass = Assembly.LoadFile(dllFilename);
                
                var instances = from t in ass.GetTypes()

                                where t.IsPublic && t.IsClass && 
                                      t.GetInterfaces().Contains(typeof(IPlayerEngine)) && 
                                      t.GetConstructor(Type.EmptyTypes) != null
                                select Activator.CreateInstance(t) as IPlayerEngine;

                foreach (var inst in instances)
                {
                    EngineInstances.Add(inst);
                }
            }
        }
    }
}
