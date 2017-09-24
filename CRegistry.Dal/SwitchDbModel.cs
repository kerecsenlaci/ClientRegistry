namespace CRegistry.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registrydata.switch")]
    public partial class SwitchDbModel
    {
        public int ID { get; set; }

        public int PartnerId { get; set; }

        public int ContactId { get; set; }

        public virtual ContactDbModel contacts { get; set; }

        public virtual PartnerDbModel partners { get; set; }
    }
}
