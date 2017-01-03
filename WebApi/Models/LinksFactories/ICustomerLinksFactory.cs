using TemplateProject.WebAPI.Models.ResponseModels;

namespace TemplateProject.WebAPI.Models.LinksFactories
{
    /// <summary>
    /// Builder of the link models for customer resource.
    /// </summary>
    public interface ICustomerLinksFactory
    {
        /// <summary>
        /// Gets the customer template link model.
        /// </summary>
        LinkModel CustomerTemplate { get; }

        /// <summary>
        /// Gets the customer link model.
        /// </summary>
        LinkModel CustomerLink(int id);

        /// <summary>
        /// Gets the customer search link model.
        /// </summary>
        LinkModel CustomerSearchTemplate { get; }
    }
}