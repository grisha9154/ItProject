using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItProject.Data
{
    public static class Theme
    {
       public static List<string> ListOfTheme {
            get
            {
                return new List < string > { "ASP.NET Core", "Java Spring", "Ruby on Rails" };
            }
        }
        
    }
}
