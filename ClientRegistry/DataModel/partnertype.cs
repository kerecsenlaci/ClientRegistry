namespace ClientRegistry
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registrydata.partnertype")]
    public partial class PartnerType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PartnerType()
        {
            partners = new HashSet<Partner>();
        }

        public int ID { get; set; }

        [StringLength(40)]
        public string Name { get; set; }


        public bool ValidateType()
        {
            return string.IsNullOrEmpty(Name);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partner> partners { get; set; }
    }
}
