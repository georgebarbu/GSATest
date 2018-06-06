using Dapper.Contrib.Extensions;

namespace Exercise.Web
{
    [Table("Strategy")]
    public sealed class StrategyDto
    {
        [Computed]
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }
    }
}