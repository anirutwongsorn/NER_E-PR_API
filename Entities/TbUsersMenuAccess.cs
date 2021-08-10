using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbUsersMenuAccess
    {
        public int Items { get; set; }
        public string UserId { get; set; }
        public int? MenuId { get; set; }
    }
}
