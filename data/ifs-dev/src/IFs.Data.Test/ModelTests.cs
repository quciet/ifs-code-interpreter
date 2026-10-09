using System;
using System.Linq;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Xunit;
using IFs.Data;

namespace IFs.Test
{

    [Collection("IFs.Test.Data")]
    public class ModelTestSuite
    {

        [ImportsAsset("CPOP")]
        public readonly IDictionary<string, float?> CPOP;

        [ImportsAsset("CGDPPCP")]
        public readonly IDictionary<string, float?> CGDPPCP;

        [Fact]
        public void TestStorage_migration() {
            using (var db = new StorageContext()) {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }
        }
    }
}
