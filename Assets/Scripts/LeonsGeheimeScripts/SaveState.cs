namespace Logic
{
    /// <summary>
    /// These save states are used to track game progress
    /// </summary>
    public abstract class SaveState
    {
        public abstract void OnStartGame();
        public abstract void OnEndGame();
    }
}