using System;
using System.Web.Mvc;
using Exceptionless.Plugins;

namespace Exceptionless.Mvc {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ExceptionlessSendErrorsAttribute : FilterAttribute, IExceptionFilter {
        public virtual void OnException(ExceptionContext filterContext) {
            var contextData = CreateContextData(filterContext);
            filterContext.Exception.ToExceptionless(contextData).Submit();
        }

        protected static ContextData CreateContextData(ExceptionContext filterContext) {
            var contextData = new ContextData();
            contextData.MarkAsUnhandledError();
            contextData.SetSubmissionMethod("SendErrorsAttribute");
            contextData.Add("HttpContext", filterContext.HttpContext);
            return contextData;
        }
    }
}
