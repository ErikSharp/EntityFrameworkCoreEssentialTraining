using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EfStructures.Context
{
    public partial class AdventureWorksContext
    {
        public static readonly LoggerFactory AppLoggerFactory =
            new LoggerFactory(new[] 
            {
                new ConsoleLoggerProvider((a, b) => true, true)
            });
    }
}
