using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessResult.Models
{
    public class PlayerInPair
    {
        public int PairingID { get; set; }

        public Player Player { get; set; }

        public float Mark { get; set; }
    }
}