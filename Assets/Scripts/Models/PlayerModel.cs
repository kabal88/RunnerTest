namespace Models
{
    public class PlayerModel
    {
        public int Level { get; private set; }
        public int Score { get; private set; }
        
        public void SetLevel(int level)
        {
            Level = level;
        }
        
        public void SetScore(int score)
        {
            Score = score;
        }
    }
}