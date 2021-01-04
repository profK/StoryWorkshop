using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Proteus
{
    public static class ProteusContext
    {
        
        private static IJSInProcessRuntime JsRuntime { get; set; }

        /***
         * This must be set before the static methods are called
         */
        public static void SetRuntime(IJSInProcessRuntime rt)
        {
            JsRuntime = rt;
        }


        public static TReturn JSInvoke<TReturn>(string funcname, params Object[] arguments)
        {
            return JsRuntime.Invoke<TReturn>(funcname, arguments);
        }

        public static void JSInvokeVoid(string funcname, params Object[] arguments)
        {
            JsRuntime.InvokeVoid(funcname,arguments);
        }

        public static void Log(string s)
        {
         
            JsRuntime.InvokeVoid("Proteus.log",s);
        }
    }
}