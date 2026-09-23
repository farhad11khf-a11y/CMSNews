using System;
using System.Web.Mvc;

namespace CMSNews.Classes.Helpers.PersianDate
{
    // Attribute سفارشی برای نمایش تاریخ شمسی
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PersianDateAttribute : Attribute, IMetadataAware
    {
        // فرمت تاریخ
        public PersianDateFormat Format { get; }

        // دریافت فرمت
        public PersianDateAttribute(PersianDateFormat format)
        {
            Format = format;
        }

        // اتصال Attribute به MVC
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            // نام DisplayTemplate
            metadata.TemplateHint = "PersianDate";

            // ذخیره فرمت انتخاب شده
            metadata.AdditionalValues["PersianDateFormat"] = Format;
        }
    }
}