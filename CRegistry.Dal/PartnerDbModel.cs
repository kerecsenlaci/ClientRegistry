namespace CRegistry.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registrydata.partners")]
    public partial class PartnerDbModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PartnerDbModel()
        {
            _switch = new HashSet<SwitchDbModel>();
        }

        public int ID { get; set; }

        [StringLength(80)]
        public string Name { get; set; }

        public int? TypeId { get; set; }

        public int? CountyId { get; set; }

        public int ZipCode { get; set; }

        [Required]
        [StringLength(40)]
        public string City { get; set; }

        [Required]
        [StringLength(60)]
        public string Address { get; set; }

        public int OwnerId { get; set; }

        public virtual ContactDbModel contacts { get; set; }

        public virtual CountyDbModel county { get; set; }

        public virtual PartnerTypeDbModel partnertype { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SwitchDbModel> _switch { get; set; }
    }
}
