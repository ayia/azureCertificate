namespace Azurtesting.Models
{
    public class Entry
    {
        public string? Titel { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Url { get; set; }
        public Scenario? MainScenario { get; set; }

        public Scenario? AltrentiveScenario { get; set; }
    }
}
