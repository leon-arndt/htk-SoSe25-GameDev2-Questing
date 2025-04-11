namespace Logic
{
    /// <summary>
    /// This class is used to save various data while the player is playing
    /// For example: quests, inventory, npc relationships, etc.
    /// </summary>
    public abstract class SaveState
    {
        /// <summary>
        /// Each save state will have a special behavior when the game starts
        /// For example: the inventory will be emptied
        /// </summary>
        public abstract void OnStartGame();
        
        /// <summary>
        /// For example useful to clear the inventory, etc.
        /// </summary>
        public abstract void OnEndGame();
    }
}