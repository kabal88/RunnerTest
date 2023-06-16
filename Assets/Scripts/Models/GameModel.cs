namespace Models
{
    public class GameModel
    {
        public int LevelGeneratorId { get; }

        public GameModel(int levelGeneratorId)
        {
            LevelGeneratorId = levelGeneratorId;
        }
        
    }
}