using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TracerProgram
{
    [Serializable]
    [DataContract]
    public sealed class ThreadInfo
    {
        [DataMember(Name = "id")]
        private int id;
        [DataMember(Name = "time")]
        private long time;
        [DataMember(Name = "methods")]
        private List<MethodInfo> methods;
        private Stack<MethodInfo> callmethods;

        public int Id
        {
            get { return id; }
        }

        public string Time
        {
            get { return time.ToString()+"ms"; }
        }

        public List<MethodInfo> Methods
        {
            get { return methods; }
        }

        public ThreadInfo()
        {
            time = 0;
            methods = new List<MethodInfo> { };
            callmethods = new Stack<MethodInfo> { };
        }

        public ThreadInfo(int threadID)
        {
            id = threadID;
            time = 0;
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
            time += lastmethod.TimeInt;
            callmethods.Pop();           
        }
    }
}
