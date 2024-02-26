using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroDePessoas.Configurations
{
    public class Databases
    {
        public static string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["cadastroPessoas"].ConnectionString;
        }
    }
}