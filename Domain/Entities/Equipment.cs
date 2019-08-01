using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Equipment
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "SerialNumber")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), 
            ErrorMessageResourceName = "SerialNumberNumberUnique")]
        [Index(IsUnique = true)]
        public int SerialNumber { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "NameRequired")]
        [StringLength(160)]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Image")]
        public byte[] Image { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "NextDateControl")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
            ErrorMessageResourceName = "DateTimeGreaterToday")]
        public DateTime NextControlDate { get; set; }
    }
}
