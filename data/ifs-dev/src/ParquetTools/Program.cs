
using Parquet;
using System.Text;
using System.Text.Json;

if (Environment.GetCommandLineArgs().Length == 3)
{
    string srcFile = Environment.GetCommandLineArgs()[1];
    string outFile = Environment.GetCommandLineArgs()[2];

    using (var fs = new FileStream(srcFile, FileMode.Open, FileAccess.Read))
    {
        using (var sw = new StreamWriter(outFile))
        {
            Parquet.ParquetReader reader = new Parquet.ParquetReader(fs);
            Parquet.Data.DataSet ds = reader.Read();
            foreach (var rec in ds)
            {
                if (rec.Get<Int16>(0) < 2029)
                {
                    for (int i = 0; i < rec.Length - 1; i++)
                        sw.Write($"{rec.Get<Int16>(i)}-");
                    sw.WriteLine($"{rec.Get<Single>(rec.Length - 1)}-{srcFile}");
                }
            }
        }
    }
}
else
{
    Console.WriteLine("executable <sourcefile> <targetfile>");
    
}