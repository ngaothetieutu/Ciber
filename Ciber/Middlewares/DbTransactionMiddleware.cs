using Ciber.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;

namespace HG.WebApp.Middlewares
{
    public class DbTransactionMiddleware
    {
        private RequestDelegate Next { get; }

        public DbTransactionMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext httpContext, ApplicationDbContext dbContext)
        {
            if (httpContext.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
            {
                await Next(httpContext);
                return;
            }

            // If action is not decorated with TransactionAttribute then skip opening transaction
            //var endpoint = httpContext.GetEndpoint();
            //var attribute = endpoint?.Metadata.GetMetadata<TransactionAttribute>();
            //if (attribute == null)
            //{
            //    await Next(httpContext);
            //    return;
            //}

            IDbContextTransaction? transaction = null;

            try
            {
                transaction = await dbContext.Database.BeginTransactionAsync();

                await Next(httpContext);

                if (httpContext.Response.StatusCode >= 400)
                {
                    await transaction.RollbackAsync();
                }
                else
                {
                    int row = await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                throw;
            }
            finally
            {
                if (transaction != null)
                    await transaction.DisposeAsync();
            }
        }
    }
}
