using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbUser
    {
        public TbUser()
        {
            TbPrMains = new HashSet<TbPrMain>();
        }

        public int Items { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string EncryptPass { get; set; }
        public string EmailBoss { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }

        public virtual ICollection<TbPrMain> TbPrMains { get; set; }
    }
}
