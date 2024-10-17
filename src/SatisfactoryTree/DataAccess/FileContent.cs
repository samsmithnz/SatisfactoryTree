using Newtonsoft.Json;
using SatisfactoryTree.Models;

namespace SatisfactoryTree.DataAccess
{
    public class FileContent
    {
        // Load the json file
        public static NewContent LoadJsonContent()
        {
            DirectoryInfo? currentDir = new(Directory.GetCurrentDirectory());
            DirectoryInfo? parentDir = currentDir.Parent?.Parent?.Parent?.Parent?.Parent;
            if (parentDir == null)
            {
                throw new Exception("Parent directory structure is not as expected.");
            }
            string projectContentPath = Path.Combine(parentDir.FullName, "content");
            string path = Path.Combine(projectContentPath, "output.json");
            string jsonString = File.ReadAllText(path);
            NewContent newContent = JsonConvert.DeserializeObject<NewContent>(jsonString);
            return newContent;
        }
    }
}
