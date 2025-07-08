namespace ORAA.Request
{
    public class AddAdmin
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Difficulty { get; set; }
        public int Duration { get; set; }
        public string Materials { get; set; }
        public string CrystalsNeeded { get; set; }
        public string JewelryRecommended { get; set; }
        public string StepsOfRitual { get; set; }
        public string MoonPhase { get; set; }
        public string SeasonalAlignment { get; set; }
        public string ChakraFocus { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
