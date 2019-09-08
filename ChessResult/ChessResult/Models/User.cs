using System;

namespace ChessResult.Models
{
    public class User
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int RoleID { get; set; }

        public DateTime? CreatedDate { get; }

        public DateTime? UpdatedDate { get; }

        public bool Status { get; }
    }
}