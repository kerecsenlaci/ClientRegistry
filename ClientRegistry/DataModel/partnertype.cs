namespace ClientRegistry
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registrydata.partnertype")]
    public partial class PartnerType:BaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PartnerType()
        {
            partners = new HashSet<Partner>();
        }

        string _name;
       
        public int ID { get; set; }

        [StringLength(40)]
        public string Name { get { return _name; } set { _name = value; OnPropertyChange("Name"); } }

        public bool ValidateType()
        {
            return string.IsNullOrEmpty(Name);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partner> partners { get; set; }
    }
}
