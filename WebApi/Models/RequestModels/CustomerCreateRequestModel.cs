﻿namespace TemplateProject.WebApi.Models.RequestModels
{
    /// <summary>
    /// The request model that is used for customer creation.
    /// </summary>
    public class CustomerCreateRequestModel
    {
        /// <summary>
        /// Gets or sets the first name of customer to create.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of customer to create.
        /// </summary>
        public string LastName { get; set; }
    }
}