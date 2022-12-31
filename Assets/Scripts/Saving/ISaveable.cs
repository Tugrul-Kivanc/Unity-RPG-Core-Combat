namespace RPG.SavingSystem
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}