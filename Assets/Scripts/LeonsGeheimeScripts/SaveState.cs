namespace Logic
{
    /// <summary>
    /// These save states are used to track game progress
    /// </summary>
    public abstract class SaveState
    {
        protected abstract void OnStartGame();
        protected abstract void OnEndGame();
    }
}