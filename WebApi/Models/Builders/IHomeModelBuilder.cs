using TemplateProject.WebAPI.Models.ResponseModels;

namespace TemplateProject.WebAPI.Models.Builders
{
    /// <summary>
    /// The builder of the <see cref="HomeResponseModel"/>.
    /// </summary>
    public interface IHomeModelBuilder
    {
        /// <summary>
        /// Builds the <see cref="HomeResponseModel" />.
        /// </summary>
        /// <returns>
        /// Built model for Home Controller.
        /// </returns>
        HomeResponseModel Build();
    }
}