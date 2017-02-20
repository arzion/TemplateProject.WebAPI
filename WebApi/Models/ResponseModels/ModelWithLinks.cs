using System.Collections.Generic;

namespace TemplateProject.WebApi.Models.ResponseModels
{
    /// <summary>
    /// The base class for model that contains links.
    /// </summary>
    public abstract class ModelWithLinks
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWithLinks"/> class.
        /// </summary>
        protected ModelWithLinks()
        {
            Links = new List<LinkModel>();
        }

        /// <summary>
        /// Gets or sets the links that are related to the model.
        /// </summary>
        public IList<LinkModel> Links { get; set; }
    }
}