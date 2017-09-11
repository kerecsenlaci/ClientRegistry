namespace ClientRegistry
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registrydata.contacts")]
    public partial class contacts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public contacts()
        {
            partners = new HashSet<partners>();
            _switch = new HashSet<_switch>();
        }

        public string LowerIndex { get { return $"tel.: {Phone}\t\temail: {Email}"; } }

        public int ID { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<partners> partners { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<_switch> _switch { get; set; }
    }
}
