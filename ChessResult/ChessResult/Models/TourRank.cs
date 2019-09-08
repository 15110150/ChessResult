using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessResult.Models
{
    public class TourRank
    {
        public int TourID { get; set; }

        public string PlayerID { get; set; }

        public string PlayerName { get; set; }

        public float TotalMark{ get; set; }
    }
}