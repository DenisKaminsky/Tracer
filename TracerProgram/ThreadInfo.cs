using System;
using System.Collections.Generic;
using System.Linq;

namespace TracerProgram
{
    public class ThreadInfo
    {
        private int id { get; }
        private string time;

        private List<MethodInfo> methods;
        private Stack<MethodInfo> callmethods;

        public ThreadInfo(int threadID)
        {
            id = threadID;
            time = "0ms";
            methods = new List<MethodInfo> { };
            callmethods = new Stack<MethodInfo> { };           
        }

        public void StartTrace(MethodInfo method)
        {
            if (callmethods.Count == 0)
                methods.Add(method);
            else
                callmethods.Peek().AddNestedMethod(method);                
            callmethods.Push(method);
            method.StartTrace();
        }

        public void StopTrace()
        {
            callmethods.Peek().StopTrace();
            callmethods.Pop();           
        }
    }
}
