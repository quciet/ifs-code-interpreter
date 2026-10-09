using System;
using System.Data;
using System.Data.Jet;


namespace ConsoleApp.Dapper
{
    public class Program
    {
        public static void Main()
        {
            using (var connection = new SqlConnection("Data Source=IFsVar.mdb")) {
            	var orderDetails = connection.Query("SELECT AggName FROM AggRules;").ToList();
            }
        }
    }
}
