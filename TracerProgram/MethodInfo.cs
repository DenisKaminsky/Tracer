using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TracerProgram
{
    [Serializable]
    [DataContract]
    public sealed class MethodInfo
    {        
        private string method_name;        
        private string class_name;
        private long time;
        
        private List<MethodInfo> methodslist;
        private Stopwatch stopwatch;

        [DataMember(Name = "name", Order = 0)]
        public string Method_name
        {
            get { return method_name; }
            private set { }
        }
        [DataMember(Name = "class", Order = 1)]
        public string Class_name
        {
            get { return class_name; }
            private set { }
        }

        [DataMember(Name = "time", Order = 2)]
        public string Time
        {
            get { return time.ToString()+"ms"; }
            private set { }
        }

        [XmlIgnore]
        public long TimeInt
        {
            get { return time; }
        }

        [DataMember(Name = "methods", Order = 3)]
        public List<MethodInfo> Methodlist
        {
            get { return methodslist; }
            private set { }
        }

        public MethodInfo()
        {
            methodslist = new List<MethodInfo>();
            stopwatch = new Stopwatch();
            time = 0;
        }

        public MethodInfo(MethodBase method)
        {
            methodslist = new List<MethodInfo>();
            stopwatch = new Stopwatch();
            method_name = method.Name;
            class_name = method.DeclaringType.Name;
            time = 0;
        }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
        }

        public void AddNestedMethod(MethodInfo method)
        {
            methodslist.Add(method);
        }
    }
}
