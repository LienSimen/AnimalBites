namespace AnimalBites.model;

public class BiteData
{
    public DateTime? BiteDate { get; set; }
    public string Species { get; set; } = "Unknown Species";
    public string Breed { get; set; } = "Unknown Breed";
    public string Gender { get; set; } = "Unknown";
    public string Color { get; set; } = "Unknown";
    public string BiteArea { get; set; } = "Unknown";
    public string VictimZip { get; set; } = "Unknown";
    public DateTime? QuarantineDate { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public int DaysInQuarantine => (ReleaseDate.HasValue && QuarantineDate.HasValue)
        ? (ReleaseDate.Value - QuarantineDate.Value).Days
        : 0;
    
    public BiteData(string csv)
    {
        string[] values = csv.Split(',');

        // '??' didnt work here. We needed to check for null, empty string and white space, and put them all to "unknown" not just null.
        BiteDate = DateTime.TryParse(values[0], out var biteDate) ? biteDate : null;
        Species = string.IsNullOrWhiteSpace(values[1]) ? "Unknown Species" : values[1];
        Breed = string.IsNullOrWhiteSpace(values[2]) ? "Unknown Breed" : values[2];
        Gender = string.IsNullOrWhiteSpace(values[3]) ? "Unknown" : values[3];

        Color = NormalizeColor(values[4]);
        VictimZip = string.IsNullOrWhiteSpace(values[7]) ? "Unknown" : values[7];
        BiteArea = string.IsNullOrWhiteSpace(values[9]) ? "Unknown" : values[9];
        QuarantineDate = DateTime.TryParse(values[10], out var quarantineDate) ? quarantineDate : null;
        ReleaseDate = DateTime.TryParse(values[13], out var releaseDate) ? releaseDate : null;
    }
    private string NormalizeColor(string color)
    {
        if (string.IsNullOrWhiteSpace(color)) return "Unknown";

        color = color.ToLowerInvariant()
                     .Trim()
                     .Trim('"')
                     .Replace("/", " ")
                     .Replace("-", " ")
                     .Replace("  ", " ");

        Dictionary<string, string> colorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        // what am i doing with my life
        {
            {"blk", "Black"}, {"black", "Black"}, {"blac", "Black"}, {"blak", "Black"},{"bk", "Black"},
            {"brn", "Brown"}, {"brown", "Brown"}, {"browm", "Brown"}, {"brwn", "Brown"}, {"bronw", "Brown"}, {"br", "Brown"}, {"bwn", "Brown"}, {"blkc", "Black"}, {"brm", "Brown"}, {"brin", "Brown"},
            {"gry", "Gray"}, {"grey", "Gray"}, {"gray", "Gray"}, {"grau", "Gray"}, {"gr", "Gray"}, {"tn", "Tan"}, {"bl", "Black"}, {"blc", "Black"}, {"wte", "White"},
            {"wht", "White"}, {"whit", "White"}, {"whti", "White"}, {"wh", "White"},  {"wt", "White"}, {"b", "Black"},
            {"tan", "Tan"}, {"tann", "Tan"}, {"tanish", "Tan"}, {"lgt", "Light"}, {"lght", "Light"}, {"ta", "Tan"}, {"tab", "Tabby"}, {"silv", "Silver"}, {"w", "White"}, {"blu", "Blue"}, {"whtie", "White"},
            {"org", "Orange"}, {"orange", "Orange"}, {"orgwht", "Orange & White"}, {"lig.", "Light"}, {"cho.wht.", "Chocolate White"},
            {"red", "Red"}, {"reddish", "Red"}, {"wgt", "White"}, {"blacf", "Black"}, {"silvr", "Silver"}, {"blck" , "Black"}, {"lt", "Light"},
            {"choc", "Chocolate"}, {"chocolate", "Chocolate"}, {"choco", "Chocolate"}, {"brw", "Brown"}, {"brindkle", "Brindle"}, {"blakc", "Black"},
            {"brindle", "Brindle"}, {"brindl", "Brindle"}, {"brnd", "Brindle"}, {"wheaton", "Wheat"}, {"strip", "Striped"},
            {"buff", "Buff"}, {"cream", "Cream"}, {"gold", "Golden"}, {"golden", "Golden"}, {"silve", "Silver"}, {"whi", "White"}, {"blk.wht", "Black White"},
            {"fawn", "Fawn"}, {"faw", "Fawn"}, {"re", "Red"}, {"drk", "Dark"}, {"rd", "Red"}, {"chol", "Chocolate"}, {"brow", "Brown"}, {"bri", "Bright"},
            {"silver", "Silver"}, {"slvr", "Silver"}, {"bla", "Black"}, {"bro", "Brown"}, {"brown0", "Brown"}, {"brownishre", "Brown"}, {"wht.Brn", "White Brown"},
            {"yellow", "Yellow"}, {"yello", "Yellow"}, {"brndl", "Brindle"}, {"grat", "Gray"}, {"brindel", "Brindle"}, {"blackj", "Black"},
            {"rust", "Rust"}, {"copper", "Copper"}, {"sable", "Sable"}, {"brdle", "Brindle"}, {"brownish", "Brown"}, {"grey&white", "Grey White"}, {"gray & white", "Gray White"}, 
            {"tri", "Tri-color"}, {"tricolor", "Tri-color"},{"or", "Orange"}, {"tabbt", "Tabby"}, {"brendel", "Brindle"}, {"siv", "Silver"},
            {"merle", "Merle"}, {"blue", "Blue"}, {"beige", "Beige"}, {"lt.", "Light"}, {"blond", "Blonde"}, {"whte", "White"}, {"slivr", "Silver"}, {"whiite", "White"},
            {"multi", "Multi-color"}, {"multicolor", "Multi-color"}, {"yellowish", "Yellow"}, {"gra", "Gray"}, {"brind", "Brindle"}
        };

        // Split words and normalize them
        string[] words = color.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            if (colorMap.TryGetValue(words[i], out string standardized))
            {
                words[i] = standardized;
            }
        }

        // Convert to Title Case
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Join(" ", words));
    }


}

