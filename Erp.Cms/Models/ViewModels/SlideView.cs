namespace Erp.Cms.Models
{
    using System;

    public class SlideView
    {
        public Guid Id
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public int Rate
        {
            get; set;
        }

        public string CreatedDate
        {
            get; set;
        }
    }
}