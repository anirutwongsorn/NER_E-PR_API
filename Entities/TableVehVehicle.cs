using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TableVehVehicle
    {
        public int VehId { get; set; }
        public int? TypeId { get; set; }
        public string Vehicle { get; set; }
        public string LicensePlate { get; set; }
    }
}
