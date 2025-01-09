
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int money = 10;

        public float ballLifeTime = 20;
        public float ballMaxSpeed = 10;

        public int maxSpeedPrice = 5;
        public int lifeTimePrice = 5;
        public int spawnIntervalPrice = 5;
        public int colorIntervalPrice = 5;

        public bool gameMoted = false;
        public bool fxOn = true;
        public bool cameraShakeOn = true;

        public float rainbowCircleSpawnInterval = 0.3f;
        public float rainbowCircleColorChangeSpeed = 0.3f;

        public SavesYG()
        {
        }
    }
}
