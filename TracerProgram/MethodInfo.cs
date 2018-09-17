using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace TracerProgram
{
    public class MethodInfo
    {
        private string method_name { get; }
        private string class_name { get; }
        private string time;

        private List<MethodInfo> methodslist;
        private Stopwatch stopwatch;

        public MethodInfo(MethodBase method)
        {
            methodslist = new List<MethodInfo> { };
            stopwatch = new Stopwatch();
            method_name = method.Name;
            class_name = method.DeclaringType.Name;
            time = "0ms";
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds.ToString()+"ms";
            stopwatch.Reset();
        }



    }
}
