namespace Data
{
    public interface IQuest
    {
        // can this quest be finished?
        public bool AreConditionsMet();

        // this is the description shown to the player
        public string GetDescription();

        // this ID is not shown to the player, it's only used to identify the quest
        public string GetId();

        // this is used to bring the player to the end screen
        public bool IsFinalQuest();
    }
}