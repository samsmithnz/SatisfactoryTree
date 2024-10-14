using SatisfactoryTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryTree.DataAccess
{
    public class FileContent
    {
        // Load the json file
        public static NewContent LoadJsonFile()
        {
            return new();
        }
    }
}
