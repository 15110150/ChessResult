namespace ChessResult.Models
{
    public class Board
    {
        public int TourID { get; set; }

        public int RoundID { get; set; }

        public int PlayerID { get; set; }

        public bool Winer { get; set; }

        public float TotalMark { get; set; }
    }
}