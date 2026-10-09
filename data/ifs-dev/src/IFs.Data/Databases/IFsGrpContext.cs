using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace IFs.Data
{

    internal partial class IFsGrpContext: IDisposable 
    {

        public IFsGrpContext()
        {
            this.IFsGrp = new SortedList<int, IDimension>();
            this.Reload();
        }

        public IEnumerable<IDimension> All 
            => IFsGrp.Select(d => d.Value);

        private void Reload() {
            this.IFsGrp.Clear();
            string connectionString = AppConfig.ConnectionStrings["IFs.grp"].ConnectionString;
            //jrs 2019/03/29
            //if (Environment.CurrentDirectory.Contains("Code.Ifs-Translation"))
            {
                connectionString = connectionString.Replace("Source=", "Source=../");
            }

            using (var reader = File.OpenText(AppConfig.GetFilePathFromConnectionString(connectionString))) {
                string line;
                try {
                    line = reader.ReadLine();
                    int nDims = int.Parse(line.Trim());
                    for (int i = 2; i <= nDims; i++) {
                        line = reader.ReadLine();
                        if (line == null) {
                            throw new ArgumentException("Invalid IFs.grp: unexpected EOF.");
                        }
                        string[] tokens = line.Split(',');
                        if (tokens.Length < 1) {
                            throw new ArgumentException("Invalid IFs.grp: insufficient values.");
                        }
                        var dim = new DimensionBind();
                        dim.Id = i;
                        dim.Buckets = new SortedList<short, string>();
                        int nVals = int.Parse(tokens[0]);
                        if (tokens.Length != nVals + 1) {
                            throw new ArgumentException("Invalid IFs.grp: invalid number of values.");
                        }
                        for (short j = 1; j < nVals + 1; j++) {
                            string val = tokens[j].Trim();
                            // skip tailing ALL
                            if ((j == nVals) && (val == "ALL")) {
                                nVals -= 1;
                                break;
                            }
                            dim.Buckets.Add(j, val);
                        }
                        if (dim.Buckets.Count != nVals) {
                            throw new ArgumentException("Invalid IFs.grp: invald number of values.");
                        }
                        this.IFsGrp.Add(i, dim);
                    }
                } catch (Exception ex) {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public void Dispose()
        {
            this.IFsGrp.Clear();
        }

    }

}
