using System;

namespace ChessResult.Models
{
    public class Player
    {
        public int PlayerID { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Image { get; set; }

        public int FederationID { get; set; }

        public int Rating { get; set; }

        public string Sex { get; set; }

        public DateTime? CreatedDate { get; }

        public DateTime? UpdatedDate { get; }

        public bool Status { get; }
    }
}