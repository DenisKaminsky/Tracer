using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace TracerProgram
{
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadInfo> threadslist;

        public TraceResult()
        {
            threadslist = new ConcurrentDictionary<int, ThreadInfo> { };
        }

        public void StartTrace(int id,MethodBase method)
        {
            ThreadInfo threadinfo = threadslist.GetOrAdd(id, new ThreadInfo(id));
            threadinfo.StartTrace(new MethodInfo(method));
        }

        public void StopTrace(int id)
        {
            ThreadInfo threadinfo;
            if (! threadslist.TryGetValue(id, out threadinfo))
            {
                throw new ArgumentException("invalid thread id");
            }
            threadinfo.StopTrace();
        }
    }
}
