using SatisfactoryTree.Console;
using SatisfactoryTree.Console.NewModels;
using SatisfactoryTree.Console.OldModels;

namespace SatisfactoryTree.WinForm
{
    public partial class Form1 : Form
    {
        private FinalData data;
        private Dictionary<string, string> parts;
        //private HashSet<ComboItem> parts;
        private List<Recipe> recipes;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Processor processor = new();
            processor.GetContentFiles();
            if (processor != null)
            {
                string inputFile = processor.InputFile;
                string outputFile = processor.OutputFile;
                data = await Processor.ProcessFileOldModel(inputFile, outputFile);

                parts = new();
                foreach (NewRecipe recipe in data.newRecipes)
                {
                    foreach (Ingredient item in recipe.ingredients)
                    {
                        parts.Add(new ComboItem(item.part, );
                    }
                }
            }
        }
    }
}
