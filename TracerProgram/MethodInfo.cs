using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;

namespace TracerProgram
{
    [Serializable]
    [DataContract]
    public sealed class MethodInfo
    {
        [DataMember(Name = "name")]
        private string method_name;
        [DataMember(Name = "class")]
        private string class_name;
        [DataMember(Name = "time")]
        private string time;
        [NonSerialized]
        public long inttime;
        [DataMember(Name = "methods")]
        private List<MethodInfo> methodslist;
        private Stopwatch stopwatch;
       
        public string Method_name
        {
            get { return method_name; }
        }

        public string Class_name
        {
            get { return class_name; }
        }

        public string Time
        {
            get { return time; }
        }

        public List<MethodInfo> Methodlist
        {
            get { return methodslist; }
        }

        public MethodInfo()
        {
            methodslist = new List<MethodInfo> { };
            stopwatch = new Stopwatch();
            time = "0ms";
            inttime = 0;
        }

        public MethodInfo(MethodBase method)
        {
            methodslist = new List<MethodInfo> { };
            stopwatch = new Stopwatch();
            method_name = method.Name;
            class_name = method.DeclaringType.Name;
            time = "0ms";
            inttime = 0;
        }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            inttime = stopwatch.ElapsedMilliseconds;
            time = inttime.ToString()+"ms";
            stopwatch.Reset();
        }

        public void AddNestedMethod(MethodInfo method)
        {
            methodslist.Add(method);
        }
    }
}
