namespace Models
{
    public class GameModel
    {
        public int LevelGeneratorId { get; }
        public int ColorPalletId { get; }
        public int InputDescriptionId { get; }

        public GameModel(int levelGeneratorId,
            int colorPalletId,
            int inputDescriptionId)
        {
            LevelGeneratorId = levelGeneratorId;
            ColorPalletId = colorPalletId;
            InputDescriptionId = inputDescriptionId;
        }
        
    }
}