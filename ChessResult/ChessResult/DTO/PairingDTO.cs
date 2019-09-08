using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessResult.DTO
{
    public class PairingDTO
    {
        public int TourID { get; set; }

        public int RoundID { get; set; }

        public int ParingID { get; set; }

        public string PlayerName { get; set; }

        public float Mark { get; set; }
    }
}