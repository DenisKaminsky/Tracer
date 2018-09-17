using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TracerProgram
{
    [Serializable]
    [DataContract]
    public class ThreadInfo
    {
        [DataMember]
        private int id;
        [DataMember]
        private string time;
        private long inttime;
        [DataMember]
        private List<MethodInfo> methods;
        private Stack<MethodInfo> callmethods;

        public ThreadInfo(int threadID)
        {
            id = threadID;
            time = "0ms";
            inttime = 0;
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
            MethodInfo lastmethod = callmethods.Peek();
            lastmethod.StopTrace();
            inttime += lastmethod.
            callmethods.Pop();           
        }
    }
}
