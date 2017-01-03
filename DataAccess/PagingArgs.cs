using System;

namespace TemplateProject.DataAccess
{
    /// <summary>
    /// The paging arguments.
    /// </summary>
    public class PagingArgs
    {
        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets the default paging args.
        /// </summary>
        public static PagingArgs Default => new PagingArgs(0, 1000);

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingArgs"/> class.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagingArgs(int pageIndex, int pageSize = 50)
        {
            if (pageSize > 1000 || pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size should be in range [1, 1000]");
            }
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}