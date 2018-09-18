using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace TracerProgram
{
    [Serializable]
    [DataContract]
    public class TraceResult
    {
        
        private ConcurrentDictionary<int, ThreadInfo> threadslist;
        [DataMember(Name = "threads")]
        public List<ThreadInfo> threads; 

        public TraceResult()
        {
            threadslist = new ConcurrentDictionary<int, ThreadInfo>();
            threads = new List<ThreadInfo>();
        }

        public void StartTrace(int id,MethodBase method)
        {
            //ThreadInfo threadinfo = threadslist.GetOrAdd(id, new ThreadInfo(id));
            //threadinfo.StartTrace(new MethodInfo(method));
            int count = threadslist.Count;
            ThreadInfo threadinfo = threadslist.GetOrAdd(id, new ThreadInfo(id));
            if (threadslist.Count != count)
                threads.Add(threadinfo);
            threadinfo.StartTrace(new MethodInfo(method));
            
        }

        public void StopTrace(int id)
        {
            ThreadInfo threadinfo;
            if (! threadslist.TryGetValue(id, out threadinfo))
            {
                throw new ArgumentException("Invalid thread ID");
            }
            threadinfo.StopTrace();
        }
    }
}
