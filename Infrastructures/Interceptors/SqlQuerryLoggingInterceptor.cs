using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ToDoWeb.Infrastructures.Interceptors
{
    
    public class SqlQuerryLoggingInterceptor : DbCommandInterceptor
    {
        private Stopwatch stopwatch = new Stopwatch();
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            
            stopwatch.Start();
            
           
            
            return base.ReaderExecuting(command, eventData, result);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            stopwatch.Stop();
            var miliseconds = stopwatch.ElapsedMilliseconds;
            using StreamWriter writer = new StreamWriter("D:\\Everedu\\ToDoWeb\\sqllog.txt", append: true);
            if (miliseconds > 2)
                writer.WriteLine(command.CommandText + "," + miliseconds);
            return base.ReaderExecuted(command, eventData, result);
        }
    }
}
