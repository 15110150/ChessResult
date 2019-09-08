using System;
using System.Collections.Generic;

namespace ChessResult.Models
{
    public class Tournament
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Federation Federation { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string Organizer { get; set; }

        public string Director { get; set; }

        public string Location { get; set; }

        public int ParentTourID { get; set; }

        public DateTime? CreatedDate { get; }

        public DateTime? UpdatedDate { get; }

        public bool Status { get; }

        public IEnumerable<Federation> FederationPaticipate { get; set; }

        public IEnumerable<Round> Rounds { get; set; }
    }
}