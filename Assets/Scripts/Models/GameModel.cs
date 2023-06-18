namespace Models
{
    public class GameModel
    {
        public int LevelGeneratorId { get; }
        public int ColorPalletId { get; }
        public int InputDescriptionId { get; }
        public int CameraDescriptionId { get; }
        
        public int UnitDescriptionId { get; }

        public GameModel(int levelGeneratorId,
            int colorPalletId,
            int inputDescriptionId,
            int cameraDescriptionId,
            int unitDescriptionId)
        {
            LevelGeneratorId = levelGeneratorId;
            ColorPalletId = colorPalletId;
            InputDescriptionId = inputDescriptionId;
            CameraDescriptionId = cameraDescriptionId;
            UnitDescriptionId = unitDescriptionId;
        }
        
    }
}