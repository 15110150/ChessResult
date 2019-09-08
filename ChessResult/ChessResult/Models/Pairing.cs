using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessResult.Models
{
    public class Pairing
    {
        public int PairingID  { get; set; }

        public string TimeStart { get; set; }

        public Tournament Tournament { get; set; }

        public Round Round { get; set; }

        public IEnumerable<PlayerInPair> playerInPairs { get; set; }
    }
}