using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace TicTacToeServer
{
    internal class WebApiExceptionLogger : ExceptionLogger
    {
        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return true;
        }

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            Log(context);

            return base.LogAsync(context, cancellationToken);
        }

        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Current.Error("Unhandled WebAPI exception processing {0} for {1}: {2}",
                                 context.Exception,
                                 context.Request.Method,
                                 context.Request.RequestUri,
                                 context.Exception);
        }
    }
}