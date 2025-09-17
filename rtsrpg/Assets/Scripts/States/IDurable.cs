namespace States
{
    public interface IDurable
    {
        int MaxHealth { get; }
        int CurrentHealth { get; set; }
    }
}