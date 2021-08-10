using System;
using System.Collections.Generic;

#nullable disable

namespace ner_api.Entities
{
    public partial class TbUsersLogin
    {
        public int Items { get; set; }
        public string UserId { get; set; }
        public string DeptCode { get; set; }
        public string Remark { get; set; }
    }
}
