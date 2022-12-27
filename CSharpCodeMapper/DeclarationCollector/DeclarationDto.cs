namespace DeclarationCollector
{
    public class DeclarationDto
    {
        public DeclarationDto(int lineNumber, string declarationType, string name, string type = "", string parameters = "")
        {
            LineNumber = lineNumber;
            Type = type;
            Name = name;
            Parameters = parameters;
            DeclarationType = declarationType;
        }

        public int LineNumber { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Parameters { get; set; }
        public string DeclarationType { get; set; }
    }
}