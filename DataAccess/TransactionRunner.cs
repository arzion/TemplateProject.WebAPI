using System;
using System.Threading.Tasks;
using TemplateProject.DataAccess.UnitOfWork;

namespace TemplateProject.DataAccess
{
    /// <summary>
    /// The runner of application transactions.
    /// </summary>
    public class TransactionRunner : ITransactionRunner
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRunner"/> class.
        /// </summary>
        public TransactionRunner()
        {
            var unitOfWork = new UnitOfWork.UnitOfWork(Configuration.UnitOfWorkProcessorFactory());
            _unitOfWork = unitOfWork;
            _unitOfWorkManager = unitOfWork;
        }

        /// <summary>
        /// Runs the application transaction.
        /// </summary>
        /// <param name="action">The action to be executed in scope of application transaction.</param>
        public async Task Run(Action<IUnitOfWorkManager> action)
        {
            action(_unitOfWorkManager);
            await _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Runs the application transaction.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="func">The function to be executed in scope of application transaction.</param>
        /// <returns>The result of the function.</returns>
        public async Task<TResult> Run<TResult>(Func<IUnitOfWorkManager, TResult> func)
        {
            var result = func(_unitOfWorkManager);
            await _unitOfWork.SaveChanges();

            return result;
        }
    }
}