namespace Data
{
    // each quest condition type needs to define if it's fulfilled
    public interface IQuestCondition
    {
        public bool IsFulfilled();
    }
}