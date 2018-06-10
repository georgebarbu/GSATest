namespace Exercise.Web
{
    public interface IStrategyImporter
    {
        bool ImportStrategy(string connectionString);
    }
}