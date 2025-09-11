namespace SatisfactoryTree.Logic.Extraction
{
    public class RawPart(string? className, string? displayName, string? description, int stackSize, string? pingColor, string? fluidColor, string? resourceSinkPoints)
    {
        //Common properties
        public string? ClassName { get; set; } = className;
        public string? DisplayName { get; set; } = displayName;
        public string? Description { get; set; } = description;

        //Item properties
        public int StackSize { get; set; } = stackSize;
        public string? PingColor { get; set; } = pingColor;
        public string? FluidColor { get; set; } = fluidColor;
        public string? ResourceSinkPoints { get; set; } = resourceSinkPoints;
    }
}
