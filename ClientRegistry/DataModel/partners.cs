namespace ClientRegistry
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registrydata.partners")]
    public partial class partners
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public partners()
        {
            _switch = new HashSet<_switch>();
        }

        public string LowerIndex { get { return $"megye: {county.CountyName}\t\tváros: {City}"; } }

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

        public virtual contacts contacts { get; set; }

        public virtual county county { get
            {
                RegistryModel registry = new RegistryModel();
                return registry.county.Find(CountyId);
            } }

        public virtual partnertype partnertype { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<_switch> _switch { get; set; }
    }
}
