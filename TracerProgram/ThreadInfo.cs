using System;
using System.Collections.Generic;
using System.Linq;

namespace TracerProgram
{
    public class ThreadInfo
    {
        private int id;
        private string time;
        private List<MethodInfo> methods;
        private Stack<MethodInfo> callmethods;

        public ThreadInfo(int threadID)
        {
            id = threadID;
            methods = new List<MethodInfo> { };
            callmethods = new Stack<MethodInfo> { };
        }

        public void StartTrace(MethodInfo method)
        {
            if (callmethods.Count == 0)
            {
                callmethods.Push(method);
                methods.Add(method);
            }
            else
            {

            }
        }

        public void StopTrace()
        {

        }
    }
}
