namespace TemplateProject.WebAPI.Models.ResponseModels
{
    /// <summary>
    /// The link model that reference to the related resources.
    /// </summary>
    public class LinkModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkModel"/> class.
        /// </summary>
        /// <param name="href">The href of the link.</param>
        /// <param name="rel">The link relation name.</param>
        public LinkModel(string href, string rel)
        {
            Href = href;
            Rel = rel;
        }

        /// <summary>
        /// Gets or sets the absolute href of the resource.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the relation name of the resource.
        /// </summary>
        public string Rel { get; set; }
    }
}