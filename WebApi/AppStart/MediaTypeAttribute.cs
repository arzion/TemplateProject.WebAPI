using System;

namespace TemplateProject.WebAPI.AppStart
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MediaTypeAttribute : Attribute
    {
        public string Name { get; set; }

        public MediaTypeAttribute(string name)
        {
            Name = name;
        }
    }
}