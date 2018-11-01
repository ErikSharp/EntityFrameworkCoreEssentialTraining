using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EfStructures.Context
{
    public class MyExecutionStrategy : ExecutionStrategy
    {
        public MyExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
        {
        }

        public MyExecutionStrategy(ExecutionStrategyDependencies dependencies) :
            base(dependencies, DefaultMaxRetryCount, DefaultMaxDelay)
        {
        }

        public MyExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) : base(dependencies, maxRetryCount, maxRetryDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            //In real life this should just be a list of exceptions and not all
            return true;
        }
    }
}
