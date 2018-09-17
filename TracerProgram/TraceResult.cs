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

        }
    }
}
