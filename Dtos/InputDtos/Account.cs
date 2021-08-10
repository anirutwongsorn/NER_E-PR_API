using System;
using ner_api.Entities;

namespace ner_pr_api.Dtos.InputDtos
{
    public class Account
    {
        public string UserID { get; set; }
        public string email { get; set; }
        public string FullName { get; set; }
        public DateTime expireDate { get; set; }

        public static Account FromTbUser(TbUser model) => new Account
        {
            UserID = model.UserId,
            email = model.Email,
            FullName = model.FullName,
        };

    }
}