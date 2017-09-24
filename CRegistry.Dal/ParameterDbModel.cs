namespace CRegistry.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registrydata.parameters")]
    public partial class ParameterDbModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(40)]
        public string ParameterName { get; set; }

        [Required]
        [StringLength(20)]
        public string ParameterValue { get; set; }

        [Required]
        [StringLength(60)]
        public string Comment { get; set; }
    }
}
