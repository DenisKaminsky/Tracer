using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TracerProgram
{
    [Serializable]
    [DataContract]
    public sealed class ThreadInfo
    {
        private int id;
        private long time;
        private List<MethodInfo> methods;
        private Stack<MethodInfo> callmethods;

        [DataMember(Name = "id",Order = 0)]
        public int Id
        {
            get { return id; }
            private set { }
        }
        [DataMember(Name = "time",Order = 1)]
        public string Time
        {
            get { return time.ToString()+"ms"; }
            private set { }
        }
        [DataMember(Name = "methods",Order = 2)]
        public List<MethodInfo> Methods
        {
            get { return methods; }
            private set { }
        }

        public ThreadInfo()
        {
            time = 0;
            methods = new List<MethodInfo>();
            callmethods = new Stack<MethodInfo>();
        }

        public ThreadInfo(int threadID)
        {
            id = threadID;
            time = 0;
            methods = new List<MethodInfo>();
            callmethods = new Stack<MethodInfo>();           
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
