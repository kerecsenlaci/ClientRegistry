namespace ClientRegistry
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registrydata.switch")]
    public partial class Switch
    {
        public int ID { get; set; }

        public int PartnerId { get; set; }

        public int ContactId { get; set; }

        public virtual Contact contacts { get; set; }

        public virtual Partner partners { get; set; }
    }
}
