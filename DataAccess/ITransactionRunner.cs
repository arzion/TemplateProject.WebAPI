using System;
using System.Threading.Tasks;
using TemplateProject.DataAccess.UnitOfWork;

namespace TemplateProject.DataAccess
{
    /// <summary>
    /// The runner of application transactions.
    /// </summary>
    public interface ITransactionRunner
    {
        /// <summary>
        /// Runs the application transaction.
        /// </summary>
        /// <param name="action">The action to be executed in scope of application transaction.</param>
        Task Run(Action<IUnitOfWorkManager> action);

        /// <summary>
        /// Runs the application transaction.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="func">The function to be executed in scope of application transaction.</param>
        /// <returns>The result of the function.</returns>
        Task<TResult> Run<TResult>(Func<IUnitOfWorkManager, TResult> func);
    }
}