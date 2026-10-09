using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IFs.Data
{

    internal static class SqliteExtensions {

        public static SqliteParameter AddParameter(this SqliteCommand stmt, string name, SqliteType type) {
            var param = stmt.CreateParameter();
            param.ParameterName = $"${name}";
            param.SqliteType = type;
            return stmt.Parameters.Add(param);
        }

    }

    internal partial class BasecaseContext {

        private SqliteConnection conn {
            get {
                return this.Database.GetDbConnection() as SqliteConnection;
            }
        }

        public void BulkInsert(IEnumerable<PopBasecase> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [PopBaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [PopulationMales],
                        [PopulationFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $PopulationMales,
                        $PopulationFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("PopulationMales", SqliteType.Real);
                    stmt.AddParameter("PopulationFemales", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.TimeId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.CohortId;
                        stmt.Parameters[3].Value = item.PopulationMales;
                        stmt.Parameters[4].Value = item.PopulationFemales;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<FerBasecase> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [FerBaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [Fertility]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $Fertility
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("Fertility", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.TimeId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.CohortId;
                        stmt.Parameters[3].Value = item.Fertility;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<MorBasecase> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [MorBaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [MortalityMales],
                        [MortalityFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $MortalityMales,
                        $MortalityFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("MortalityMales", SqliteType.Real);
                    stmt.AddParameter("MortalityFemales", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.TimeId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.CohortId;
                        stmt.Parameters[3].Value = item.MortalityMales;
                        stmt.Parameters[4].Value = item.MortalityFemales;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<MorDetBaseCase> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [MorDetBaseCase] (
                        [TimeId],
                        [RegionId], 
                        [CohortId], 
                        [MortPerMalesComDis],
                        [MortPerFeMalesComDis],
                        [MortPerMalesNonComDis],
                        [MortPerFeMalesNonComDis],
                        [MortPerMalesInjuries],
                        [MortPerFeMalesInjuries],
                        [MaligNeoPlasMales],
                        [MaligNeoPlasFemales],
                        [CardioVascMales],
                        [CardioVascFemales],
                        [DigestiveMales],
                        [DigestiveFemales],
                        [RespiratoryMales],
                        [RespiratoryFemales],
                        [OtherG2Males],
                        [OtherG2Females],
                        [RoadTrafficAccMales],
                        [RoadTrafficAccFemales],
                        [OtherunintInjMales],
                        [OtherunintInjFemales],
                        [IntenInjMales],
                        [IntenInjFemales],
                        [DiabetesMales],
                        [DiabetesFemales],
                        [AIDSMales],
                        [AIDSFemales],
                        [ComDisNoAIDSMales],
                        [ComDisNoAIDSFemales],
                        [DiarrheaMales],
                        [DiarrheaFemales],
                        [MalariaMales],
                        [MalariaFemales],
                        [RespInfMales],
                        [RespInfFemales],
                        [MentalHealthMales],
                        [MentalHealthFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $MortPerMalesComDis,
                        $MortPerFeMalesComDis,
                        $MortPerMalesNonComDis,
                        $MortPerFeMalesNonComDis,
                        $MortPerMalesInjuries,
                        $MortPerFeMalesInjuries,
                        $MaligNeoPlasMales,
                        $MaligNeoPlasFemales,
                        $CardioVascMales,
                        $CardioVascFemales,
                        $DigestiveMales,
                        $DigestiveFemales,
                        $RespiratoryMales,
                        $RespiratoryFemales,
                        $OtherG2Males,
                        $OtherG2Females,
                        $RoadTrafficAccMales,
                        $RoadTrafficAccFemales,
                        $OtherunintInjMales,
                        $OtherunintInjFemales,
                        $IntenInjMales,
                        $IntenInjFemales,
                        $DiabetesMales,
                        $DiabetesFemales,
                        $AIDSMales,
                        $AIDSFemales,
                        $ComDisNoAIDSMales,
                        $ComDisNoAIDSFemales,
                        $DiarrheaMales,
                        $DiarrheaFemales,
                        $MalariaMales,
                        $MalariaFemales,
                        $RespInfMales,
                        $RespInfFemales,
                        $MentalHealthMales,
                        $MentalHealthFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer); 
                    stmt.AddParameter("CohortId", SqliteType.Integer); 
                    stmt.AddParameter("MortPerMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerFeMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerMalesNonComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerFeMalesNonComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerMalesInjuries", SqliteType.Real);
                    stmt.AddParameter("MortPerFeMalesInjuries", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasMales", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasFemales", SqliteType.Real);
                    stmt.AddParameter("CardioVascMales", SqliteType.Real);
                    stmt.AddParameter("CardioVascFemales", SqliteType.Real);
                    stmt.AddParameter("DigestiveMales", SqliteType.Real);
                    stmt.AddParameter("DigestiveFemales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryMales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryFemales", SqliteType.Real);
                    stmt.AddParameter("OtherG2Males", SqliteType.Real);
                    stmt.AddParameter("OtherG2Females", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccMales", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccFemales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjMales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjFemales", SqliteType.Real);
                    stmt.AddParameter("IntenInjMales", SqliteType.Real);
                    stmt.AddParameter("IntenInjFemales", SqliteType.Real);
                    stmt.AddParameter("DiabetesMales", SqliteType.Real);
                    stmt.AddParameter("DiabetesFemales", SqliteType.Real);
                    stmt.AddParameter("AIDSMales", SqliteType.Real);
                    stmt.AddParameter("AIDSFemales", SqliteType.Real);
                    stmt.AddParameter("ComDisNoAIDSMales", SqliteType.Real);
                    stmt.AddParameter("ComDisNoAIDSFemales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaMales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaFemales", SqliteType.Real);
                    stmt.AddParameter("MalariaMales", SqliteType.Real);
                    stmt.AddParameter("MalariaFemales", SqliteType.Real);
                    stmt.AddParameter("RespInfMales", SqliteType.Real);
                    stmt.AddParameter("RespInfFemales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthMales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthFemales", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.TimeId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.CohortId;
                        stmt.Parameters[3].Value = item.MortPerMalesComDis;
                        stmt.Parameters[4].Value = item.MortPerFeMalesComDis;
                        stmt.Parameters[5].Value = item.MortPerMalesNonComDis;
                        stmt.Parameters[6].Value = item.MortPerFeMalesNonComDis;
                        stmt.Parameters[7].Value = item.MortPerMalesInjuries;
                        stmt.Parameters[8].Value = item.MortPerFeMalesInjuries;
                        stmt.Parameters[9].Value = item.MaligNeoPlasMales;
                        stmt.Parameters[10].Value = item.MaligNeoPlasFemales;
                        stmt.Parameters[11].Value = item.CardioVascMales;
                        stmt.Parameters[12].Value = item.CardioVascFemales;
                        stmt.Parameters[13].Value = item.DigestiveMales;
                        stmt.Parameters[14].Value = item.DigestiveFemales;
                        stmt.Parameters[15].Value = item.RespiratoryMales;
                        stmt.Parameters[16].Value = item.RespiratoryFemales;
                        stmt.Parameters[17].Value = item.OtherG2Males;
                        stmt.Parameters[18].Value = item.OtherG2Females;
                        stmt.Parameters[19].Value = item.RoadTrafficAccMales;
                        stmt.Parameters[20].Value = item.RoadTrafficAccFemales;
                        stmt.Parameters[21].Value = item.OtherunintInjMales;
                        stmt.Parameters[22].Value = item.OtherunintInjFemales;
                        stmt.Parameters[23].Value = item.IntenInjMales;
                        stmt.Parameters[24].Value = item.IntenInjFemales;
                        stmt.Parameters[25].Value = item.DiabetesMales;
                        stmt.Parameters[26].Value = item.DiabetesFemales;
                        stmt.Parameters[27].Value = item.AIDSMales;
                        stmt.Parameters[28].Value = item.AIDSFemales;
                        stmt.Parameters[29].Value = item.ComDisNoAIDSMales;
                        stmt.Parameters[30].Value = item.ComDisNoAIDSFemales;
                        stmt.Parameters[31].Value = item.DiarrheaMales;
                        stmt.Parameters[32].Value = item.DiarrheaFemales;
                        stmt.Parameters[33].Value = item.MalariaMales;
                        stmt.Parameters[34].Value = item.MalariaFemales;
                        stmt.Parameters[35].Value = item.RespInfMales;
                        stmt.Parameters[36].Value = item.RespInfFemales;
                        stmt.Parameters[37].Value = item.MentalHealthMales;
                        stmt.Parameters[38].Value = item.MentalHealthFemales;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<MorbDetBaseCase> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [MorbDetBaseCase] (
                        [TimeId],
                        [RegionId], 
                        [CohortId], 
                        [MorbPerMalesComDis],
                        [MorbPerFeMalesComDis],
                        [MorbPerMalesNonComDis],
                        [MorbPerFeMalesNonComDis],
                        [MorbPerMalesInjuries],
                        [MorbPerFeMalesInjuries],
                        [MaligNeoPlasMales],
                        [MaligNeoPlasFemales],
                        [CardioVascMales],
                        [CardioVascFemales],
                        [DigestiveMales],
                        [DigestiveFemales],
                        [RespiratoryMales],
                        [RespiratoryFemales],
                        [OtherG2Males],
                        [OtherG2Females],
                        [RoadTrafficAccMales],
                        [RoadTrafficAccFemales],
                        [OtherunintInjMales],
                        [OtherunintInjFemales],
                        [IntenInjMales],
                        [IntenInjFemales],
                        [DiabetesMales],
                        [DiabetesFemales],
                        [AIDSMales],
                        [AIDSFemales],
                        [ComDisNoAIDSMales],
                        [ComDisNoAIDSFemales],
                        [DiarrheaMales],
                        [DiarrheaFemales],
                        [MalariaMales],
                        [MalariaFemales],
                        [RespInfMales],
                        [RespInfFemales],
                        [MentalHealthMales],
                        [MentalHealthFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $MorbPerMalesComDis,
                        $MorbPerFeMalesComDis,
                        $MorbPerMalesNonComDis,
                        $MorbPerFeMalesNonComDis,
                        $MorbPerMalesInjuries,
                        $MorbPerFeMalesInjuries,
                        $MaligNeoPlasMales,
                        $MaligNeoPlasFemales,
                        $CardioVascMales,
                        $CardioVascFemales,
                        $DigestiveMales,
                        $DigestiveFemales,
                        $RespiratoryMales,
                        $RespiratoryFemales,
                        $OtherG2Males,
                        $OtherG2Females,
                        $RoadTrafficAccMales,
                        $RoadTrafficAccFemales,
                        $OtherunintInjMales,
                        $OtherunintInjFemales,
                        $IntenInjMales,
                        $IntenInjFemales,
                        $DiabetesMales,
                        $DiabetesFemales,
                        $AIDSMales,
                        $AIDSFemales,
                        $ComDisNoAIDSMales,
                        $ComDisNoAIDSFemales,
                        $DiarrheaMales,
                        $DiarrheaFemales,
                        $MalariaMales,
                        $MalariaFemales,
                        $RespInfMales,
                        $RespInfFemales,
                        $MentalHealthMales,
                        $MentalHealthFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer); 
                    stmt.AddParameter("CohortId", SqliteType.Integer); 
                    stmt.AddParameter("MorbPerMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MorbPerFeMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MorbPerMalesNonComDis", SqliteType.Real);
                    stmt.AddParameter("MorbPerFeMalesNonComDis", SqliteType.Real);
                    stmt.AddParameter("MorbPerMalesInjuries", SqliteType.Real);
                    stmt.AddParameter("MorbPerFeMalesInjuries", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasMales", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasFemales", SqliteType.Real);
                    stmt.AddParameter("CardioVascMales", SqliteType.Real);
                    stmt.AddParameter("CardioVascFemales", SqliteType.Real);
                    stmt.AddParameter("DigestiveMales", SqliteType.Real);
                    stmt.AddParameter("DigestiveFemales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryMales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryFemales", SqliteType.Real);
                    stmt.AddParameter("OtherG2Males", SqliteType.Real);
                    stmt.AddParameter("OtherG2Females", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccMales", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccFemales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjMales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjFemales", SqliteType.Real);
                    stmt.AddParameter("IntenInjMales", SqliteType.Real);
                    stmt.AddParameter("IntenInjFemales", SqliteType.Real);
                    stmt.AddParameter("DiabetesMales", SqliteType.Real);
                    stmt.AddParameter("DiabetesFemales", SqliteType.Real);
                    stmt.AddParameter("AIDSMales", SqliteType.Real);
                    stmt.AddParameter("AIDSFemales", SqliteType.Real);
                    stmt.AddParameter("ComDisNoAIDSMales", SqliteType.Real);
                    stmt.AddParameter("ComDisNoAIDSFemales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaMales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaFemales", SqliteType.Real);
                    stmt.AddParameter("MalariaMales", SqliteType.Real);
                    stmt.AddParameter("MalariaFemales", SqliteType.Real);
                    stmt.AddParameter("RespInfMales", SqliteType.Real);
                    stmt.AddParameter("RespInfFemales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthMales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthFemales", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.TimeId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.CohortId;
                        stmt.Parameters[3].Value = item.MorbPerMalesComDis;
                        stmt.Parameters[4].Value = item.MorbPerFeMalesComDis;
                        stmt.Parameters[5].Value = item.MorbPerMalesNonComDis;
                        stmt.Parameters[6].Value = item.MorbPerFeMalesNonComDis;
                        stmt.Parameters[7].Value = item.MorbPerMalesInjuries;
                        stmt.Parameters[8].Value = item.MorbPerFeMalesInjuries;
                        stmt.Parameters[9].Value = item.MaligNeoPlasMales;
                        stmt.Parameters[10].Value = item.MaligNeoPlasFemales;
                        stmt.Parameters[11].Value = item.CardioVascMales;
                        stmt.Parameters[12].Value = item.CardioVascFemales;
                        stmt.Parameters[13].Value = item.DigestiveMales;
                        stmt.Parameters[14].Value = item.DigestiveFemales;
                        stmt.Parameters[15].Value = item.RespiratoryMales;
                        stmt.Parameters[16].Value = item.RespiratoryFemales;
                        stmt.Parameters[17].Value = item.OtherG2Males;
                        stmt.Parameters[18].Value = item.OtherG2Females;
                        stmt.Parameters[19].Value = item.RoadTrafficAccMales;
                        stmt.Parameters[20].Value = item.RoadTrafficAccFemales;
                        stmt.Parameters[21].Value = item.OtherunintInjMales;
                        stmt.Parameters[22].Value = item.OtherunintInjFemales;
                        stmt.Parameters[23].Value = item.IntenInjMales;
                        stmt.Parameters[24].Value = item.IntenInjFemales;
                        stmt.Parameters[25].Value = item.DiabetesMales;
                        stmt.Parameters[26].Value = item.DiabetesFemales;
                        stmt.Parameters[27].Value = item.AIDSMales;
                        stmt.Parameters[28].Value = item.AIDSFemales;
                        stmt.Parameters[29].Value = item.ComDisNoAIDSMales;
                        stmt.Parameters[30].Value = item.ComDisNoAIDSFemales;
                        stmt.Parameters[31].Value = item.DiarrheaMales;
                        stmt.Parameters[32].Value = item.DiarrheaFemales;
                        stmt.Parameters[33].Value = item.MalariaMales;
                        stmt.Parameters[34].Value = item.MalariaFemales;
                        stmt.Parameters[35].Value = item.RespInfMales;
                        stmt.Parameters[36].Value = item.RespInfFemales;
                        stmt.Parameters[37].Value = item.MentalHealthMales;
                        stmt.Parameters[38].Value = item.MentalHealthFemales;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<MorDetFactors> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [MorDetFactors] (
                        [CohortId], 
                        [RegionId], 
                        [MortPerMalesComDis],
                        [MortPerFeMalesComDis],
                        [MaligNeoPlasMales],
                        [MaligNeoPlasFemales],
                        [CardioVascMales],
                        [CardioVascFemales],
                        [DigestiveMales],
                        [DigestiveFemales],
                        [RespiratoryMales],
                        [RespiratoryFemales],
                        [OtherG2Males],
                        [OtherG2Females],
                        [RoadTrafficAccMales],
                        [RoadTrafficAccFemales],
                        [OtherunintInjMales],
                        [OtherunintInjFemales],
                        [IntenInjMales],
                        [IntenInjFemales],
                        [DiabetesMales],
                        [DiabetesFemales],
                        [DiarrheaMales],
                        [DiarrheaFemales],
                        [MalariaMales],
                        [MalariaFemales],
                        [RespInfMales],
                        [RespInfFemales],
                        [MentalHealthMales],
                        [MentalHealthFemales]
                    ) VALUES (
                        $CohortId, 
                        $RegionId, 
                        $MortPerMalesComDis,
                        $MortPerFeMalesComDis,
                        $MaligNeoPlasMales,
                        $MaligNeoPlasFemales,
                        $CardioVascMales,
                        $CardioVascFemales,
                        $DigestiveMales,
                        $DigestiveFemales,
                        $RespiratoryMales,
                        $RespiratoryFemales,
                        $OtherG2Males,
                        $OtherG2Females,
                        $RoadTrafficAccMales,
                        $RoadTrafficAccFemales,
                        $OtherunintInjMales,
                        $OtherunintInjFemales,
                        $IntenInjMales,
                        $IntenInjFemales,
                        $DiabetesMales,
                        $DiabetesFemales,
                        $DiarrheaMales,
                        $DiarrheaFemales,
                        $MalariaMales,
                        $MalariaFemales,
                        $RespInfMales,
                        $RespInfFemales,
                        $MentalHealthMales,
                        $MentalHealthFemales
                    );";
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("MortPerMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerFeMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasMales", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasFemales", SqliteType.Real);
                    stmt.AddParameter("CardioVascMales", SqliteType.Real);
                    stmt.AddParameter("CardioVascFemales", SqliteType.Real);
                    stmt.AddParameter("DigestiveMales", SqliteType.Real);
                    stmt.AddParameter("DigestiveFemales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryMales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryFemales", SqliteType.Real);
                    stmt.AddParameter("OtherG2Males", SqliteType.Real);
                    stmt.AddParameter("OtherG2Females", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccMales", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccFemales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjMales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjFemales", SqliteType.Real);
                    stmt.AddParameter("IntenInjMales", SqliteType.Real);
                    stmt.AddParameter("IntenInjFemales", SqliteType.Real);
                    stmt.AddParameter("DiabetesMales", SqliteType.Real);
                    stmt.AddParameter("DiabetesFemales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaMales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaFemales", SqliteType.Real);
                    stmt.AddParameter("MalariaMales", SqliteType.Real);
                    stmt.AddParameter("MalariaFemales", SqliteType.Real);
                    stmt.AddParameter("RespInfMales", SqliteType.Real);
                    stmt.AddParameter("RespInfFemales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthMales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthFemales", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.CohortId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.MortPerMalesComDis;
                        stmt.Parameters[3].Value = item.MortPerFeMalesComDis;
                        stmt.Parameters[4].Value = item.MaligNeoPlasMales;
                        stmt.Parameters[5].Value = item.MaligNeoPlasFemales;
                        stmt.Parameters[6].Value = item.CardioVascMales;
                        stmt.Parameters[7].Value = item.CardioVascFemales;
                        stmt.Parameters[8].Value = item.DigestiveMales;
                        stmt.Parameters[9].Value = item.DigestiveFemales;
                        stmt.Parameters[10].Value = item.RespiratoryMales;
                        stmt.Parameters[11].Value = item.RespiratoryFemales;
                        stmt.Parameters[12].Value = item.OtherG2Males;
                        stmt.Parameters[13].Value = item.OtherG2Females;
                        stmt.Parameters[14].Value = item.RoadTrafficAccMales;
                        stmt.Parameters[15].Value = item.RoadTrafficAccFemales;
                        stmt.Parameters[16].Value = item.OtherunintInjMales;
                        stmt.Parameters[17].Value = item.OtherunintInjFemales;
                        stmt.Parameters[18].Value = item.IntenInjMales;
                        stmt.Parameters[19].Value = item.IntenInjFemales;
                        stmt.Parameters[20].Value = item.DiabetesMales;
                        stmt.Parameters[21].Value = item.DiabetesFemales;
                        stmt.Parameters[22].Value = item.DiarrheaMales;
                        stmt.Parameters[23].Value = item.DiarrheaFemales;
                        stmt.Parameters[24].Value = item.MalariaMales;
                        stmt.Parameters[25].Value = item.MalariaFemales;
                        stmt.Parameters[26].Value = item.RespInfMales;
                        stmt.Parameters[27].Value = item.RespInfFemales;
                        stmt.Parameters[28].Value = item.MentalHealthMales;
                        stmt.Parameters[29].Value = item.MentalHealthFemales;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<MorDetNormFactors> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [MorDetNormFactors] (
                        [CohortId],
                        [RegionId],
                        [Males],
                        [Females]
                    ) VALUES (
                        $CohortId, 
                        $RegionId, 
                        $Males,
                        $Females
                    );";
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("Males", SqliteType.Real);
                    stmt.AddParameter("Females", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.CohortId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.Males;
                        stmt.Parameters[3].Value = item.Females;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<StuntingByAge> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [StuntingByAge] (
                        [Years],
                        [NCohorts],
                        [Region],
                        [StuntingMales],
                        [StuntingFemales]
                    ) VALUES (
                        $Years,
                        $NCohorts,
                        $Region,
                        $StuntingMales,
                        $StuntingFemales
                    );";
                    stmt.AddParameter("Years", SqliteType.Integer);
                    stmt.AddParameter("NCohorts", SqliteType.Integer);
                    stmt.AddParameter("Region", SqliteType.Real);
                    stmt.AddParameter("StuntingMales", SqliteType.Real);
                    stmt.AddParameter("StuntingFemales", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.Years;
                        stmt.Parameters[1].Value = item.NCohorts;
                        stmt.Parameters[2].Value = item.Region;
                        stmt.Parameters[3].Value = item.StuntingMales;
                        stmt.Parameters[4].Value = item.StuntingFemales;                        
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<EdnGradsBaseCase> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [EdnGradsBaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [PriGradMales],
                        [PriGradFemales],
                        [SecGradMales],
                        [SecGradFemales],
                        [TerGradMales],
                        [TerGradFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId,
                        $CohortId,
                        $PriGradMales,
                        $PriGradFemales,
                        $SecGradMales,
                        $SecGradFemales,
                        $TerGradMales,
                        $TerGradFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("PriGradMales", SqliteType.Real);
                    stmt.AddParameter("PriGradFemales", SqliteType.Real);
                    stmt.AddParameter("SecGradMales", SqliteType.Real);
                    stmt.AddParameter("SecGradFemales", SqliteType.Real);
                    stmt.AddParameter("TerGradMales", SqliteType.Real);
                    stmt.AddParameter("TerGradFemales", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.TimeId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.CohortId;
                        stmt.Parameters[3].Value = item.PriGradMales;
                        stmt.Parameters[4].Value = item.PriGradFemales;                        
                        stmt.Parameters[5].Value = item.SecGradMales;
                        stmt.Parameters[6].Value = item.SecGradFemales;                        
                        stmt.Parameters[7].Value = item.TerGradMales;
                        stmt.Parameters[8].Value = item.TerGradFemales;                        
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<EdnYears25BaseCase> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [EdnYears25BaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [EdnYears25Males],
                        [EdnYears25Females],
                        [EdnYears25Total]
                    ) VALUES (
                        $TimeId,
                        $RegionId,
                        $CohortId,
                        $EdnYears25Males,
                        $EdnYears25Females,
                        $EdnYears25Total
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("EdnYears25Males", SqliteType.Real);
                    stmt.AddParameter("EdnYears25Females", SqliteType.Real);
                    stmt.AddParameter("EdnYears25Total", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.TimeId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.CohortId;
                        stmt.Parameters[3].Value = item.EdnYears25Males;
                        stmt.Parameters[4].Value = item.EdnYears25Females;
                        stmt.Parameters[5].Value = item.EdnYears25Total;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<EdnYears15BaseCase> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [EdnYears15BaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [EdnYears15Males],
                        [EdnYears15Females],
                        [EdnYears15Total]
                    ) VALUES (
                        $TimeId,
                        $RegionId,
                        $CohortId,
                        $EdnYears15Males,
                        $EdnYears15Females,
                        $EdnYears15Total
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("EdnYears15Males", SqliteType.Real);
                    stmt.AddParameter("EdnYears15Females", SqliteType.Real);
                    stmt.AddParameter("EdnYears15Total", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.TimeId;
                        stmt.Parameters[1].Value = item.RegionId;
                        stmt.Parameters[2].Value = item.CohortId;
                        stmt.Parameters[3].Value = item.EdnYears15Males;
                        stmt.Parameters[4].Value = item.EdnYears15Females;
                        if (item.EdnYears15Total != null) {
                            stmt.Parameters[5].Value = item.EdnYears15Total;
                        } else {
                            stmt.Parameters[5].Value = DBNull.Value;
                        }
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsert(IEnumerable<HLPROXIMATEDRIVERS> bulk) {
            using (var tx = this.conn.BeginTransaction()) {
                using (var stmt = this.conn.CreateCommand()) {
                    stmt.CommandText = @"INSERT INTO [HLPROXIMATEDRIVERS] (
                        [VarName],
                        [Year],
                        [Region],
                        [Dim2],
                        [Dim3],
                        [DistalFormVal]
                    ) VALUES (
                        $VarName,
                        $Year,
                        $Region,
                        $Dim2,
                        $Dim3,
                        $DistalFormVal
                    );";
                    stmt.AddParameter("VarName", SqliteType.Integer);
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Region", SqliteType.Integer);
                    stmt.AddParameter("Dim2", SqliteType.Integer);
                    stmt.AddParameter("Dim3", SqliteType.Integer);
                    stmt.AddParameter("DistalFormVal", SqliteType.Real);
                    stmt.Prepare();
                    foreach (var item in bulk) {
                        stmt.Parameters[0].Value = item.VarName;
                        stmt.Parameters[1].Value = item.Year;
                        stmt.Parameters[2].Value = item.Region;
                        stmt.Parameters[3].Value = item.Dim2;
                        stmt.Parameters[4].Value = item.Dim3;
                        stmt.Parameters[5].Value = item.DistalFormVal;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertPop(Double[,,] bulk, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [pop_base_case] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [PopulationMales],
                        [PopulationFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $PopulationMales,
                        $PopulationFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("PopulationMales", SqliteType.Real);
                    stmt.AddParameter("PopulationFemales", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = bulk[i,R,1];
                            stmt.Parameters[4].Value = bulk[i,R,2];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkDeletePop(int FirstYear, int LastYear, Boolean HistoricMode)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    if (HistoricMode)
                    {
                        stmt.CommandText = @"DELETE FROM [pop_base_case] WHERE [TimeId] >= $TimeId AND [TimeId] < $TimeId2";
                    }
                    else
                    {
                        stmt.CommandText = @"DELETE FROM [pop_base_case] WHERE [TimeId] >= $TimeId";
                    }

                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    if (HistoricMode) stmt.AddParameter("TimeId2", SqliteType.Integer);
                    stmt.Prepare();
                    stmt.Parameters[0].Value = FirstYear;
                    if (HistoricMode) stmt.Parameters[1].Value = LastYear;

                    stmt.ExecuteNonQuery();
                    tx.Commit();
                }
            }
        }

        public void BulkInsertFer(Double[,] bulk, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [fer_base_case] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [Fertility]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $Fertility
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("Fertility", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = bulk[i,R];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMor(Double[,,] bulk, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [mor_base_case] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [MortalityMales],
                        [MortalityFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $MortalityMales,
                        $MortalityFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("MortalityMales", SqliteType.Real);
                    stmt.AddParameter("MortalityFemales", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = bulk[i, R, 1];
                            stmt.Parameters[4].Value = bulk[i, R, 2];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMor(Double[,,] bulk, Double[,,] bulkP, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [mor_base_case] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [MortalityMales],
                        [MortalityFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $MortalityMales,
                        $MortalityFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("MortalityMales", SqliteType.Real);
                    stmt.AddParameter("MortalityFemales", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            if (bulkP[i, R, 1] > 0)
                            {
                                stmt.Parameters[3].Value = bulk[i, R, 1] / bulkP[i, R, 1] * 1000;
                            }
                            else {
                                stmt.Parameters[3].Value = DBNull.Value;
                            }
                            if (bulkP[i, R, 2] > 0)
                            {
                                stmt.Parameters[4].Value = bulk[i, R, 2] / bulkP[i, R, 2] * 1000;
                            }
                            else {
                                stmt.Parameters[4].Value = DBNull.Value;
                            }
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertEdGrads(Single[,,] bulk, Single[,,] bulk2, Single[,,] bulk3, Double[,,] bulkP, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [EdnGradsBaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [PriGradMales],
                        [PriGradFemales],
                        [SecGradMales],
                        [SecGradFemales],
                        [TerGradMales],
                        [TerGradFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId,
                        $CohortId,
                        $PriGradMales,
                        $PriGradFemales,
                        $SecGradMales,
                        $SecGradFemales,
                        $TerGradMales,
                        $TerGradFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("PriGradMales", SqliteType.Real);
                    stmt.AddParameter("PriGradFemales", SqliteType.Real);
                    stmt.AddParameter("SecGradMales", SqliteType.Real);
                    stmt.AddParameter("SecGradFemales", SqliteType.Real);
                    stmt.AddParameter("TerGradMales", SqliteType.Real);
                    stmt.AddParameter("TerGradFemales", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = (bulk[i, R, 1] > 100 ? 100 : bulk[i, R, 1]) * bulkP[i, R, 1]/100;
                            stmt.Parameters[4].Value = (bulk[i, R, 2] > 100 ? 100 : bulk[i, R, 2]) * bulkP[i, R, 2] / 100;
                            stmt.Parameters[5].Value = (i == 3 ? 0 : bulk2[i, R, 1]) * bulkP[i, R, 1] / 100;
                            stmt.Parameters[6].Value = (i == 3 ? 0 : bulk2[i, R, 2]) * bulkP[i, R, 2] / 100;
                            stmt.Parameters[7].Value = (i == 4 ? 0 : bulk3[i, R, 1]) * bulkP[i, R, 1] / 100;
                            stmt.Parameters[8].Value = (i == 4 ? 0 : bulk3[i, R, 2]) * bulkP[i, R, 2] / 100;
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertEdYrs15(Single[,,] bulk, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [EdnYears15BaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [EdnYears15Males],
                        [EdnYears15Females]
                    ) VALUES (
                        $TimeId,
                        $RegionId,
                        $CohortId,
                        $EdnYears15Males,
                        $EdnYears15Females
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("EdnYears15Males", SqliteType.Real);
                    stmt.AddParameter("EdnYears15Females", SqliteType.Real);
                    //stmt.AddParameter("EdnYears15Total", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = bulk[i, R, 1];
                            stmt.Parameters[4].Value = bulk[i, R, 2];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertEdYrs25(Single[,,] bulk, int NCohorts, int NR, int TimeId) //mti 2024/03/25 new proc
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [EdnYears25BaseCase] (
                        [TimeId],
                        [RegionId],
                        [CohortId],
                        [EdnYears25Males],
                        [EdnYears25Females]
                    ) VALUES (
                        $TimeId,
                        $RegionId,
                        $CohortId,
                        $EdnYears25Males,
                        $EdnYears25Females
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("EdnYears25Males", SqliteType.Real);
                    stmt.AddParameter("EdnYears25Females", SqliteType.Real);
                    //stmt.AddParameter("EdnYears15Total", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = bulk[i, R, 1];
                            stmt.Parameters[4].Value = bulk[i, R, 2];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }
        public void BulkInsertMorDet(Double[,,] bulk, Double[,,] bulkC, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [mor_det_base_case] (
                        [TimeId],
                        [RegionId], 
                        [CohortId], 
                        [MortPerMalesComDis],
                        [MortPerFeMalesComDis],
                        [MortPerMalesNonComDis],
                        [MortPerFeMalesNonComDis],
                        [MortPerMalesInjuries],
                        [MortPerFeMalesInjuries],
                        [MaligNeoPlasMales],
                        [MaligNeoPlasFemales],
                        [CardioVascMales],
                        [CardioVascFemales],
                        [DigestiveMales],
                        [DigestiveFemales],
                        [RespiratoryMales],
                        [RespiratoryFemales],
                        [OtherG2Males],
                        [OtherG2Females],
                        [RoadTrafficAccMales],
                        [RoadTrafficAccFemales],
                        [OtherunintInjMales],
                        [OtherunintInjFemales],
                        [IntenInjMales],
                        [IntenInjFemales],
                        [DiabetesMales],
                        [DiabetesFemales],
                        [AIDSMales],
                        [AIDSFemales],
                        [ComDisNoAIDSMales],
                        [ComDisNoAIDSFemales],
                        [DiarrheaMales],
                        [DiarrheaFemales],
                        [MalariaMales],
                        [MalariaFemales],
                        [RespInfMales],
                        [RespInfFemales],
                        [MentalHealthMales],
                        [MentalHealthFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $MortPerMalesComDis,
                        $MortPerFeMalesComDis,
                        $MortPerMalesNonComDis,
                        $MortPerFeMalesNonComDis,
                        $MortPerMalesInjuries,
                        $MortPerFeMalesInjuries,
                        $MaligNeoPlasMales,
                        $MaligNeoPlasFemales,
                        $CardioVascMales,
                        $CardioVascFemales,
                        $DigestiveMales,
                        $DigestiveFemales,
                        $RespiratoryMales,
                        $RespiratoryFemales,
                        $OtherG2Males,
                        $OtherG2Females,
                        $RoadTrafficAccMales,
                        $RoadTrafficAccFemales,
                        $OtherunintInjMales,
                        $OtherunintInjFemales,
                        $IntenInjMales,
                        $IntenInjFemales,
                        $DiabetesMales,
                        $DiabetesFemales,
                        $AIDSMales,
                        $AIDSFemales,
                        $ComDisNoAIDSMales,
                        $ComDisNoAIDSFemales,
                        $DiarrheaMales,
                        $DiarrheaFemales,
                        $MalariaMales,
                        $MalariaFemales,
                        $RespInfMales,
                        $RespInfFemales,
                        $MentalHealthMales,
                        $MentalHealthFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("MortPerMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerFeMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerMalesNonComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerFeMalesNonComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerMalesInjuries", SqliteType.Real);
                    stmt.AddParameter("MortPerFeMalesInjuries", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasMales", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasFemales", SqliteType.Real);
                    stmt.AddParameter("CardioVascMales", SqliteType.Real);
                    stmt.AddParameter("CardioVascFemales", SqliteType.Real);
                    stmt.AddParameter("DigestiveMales", SqliteType.Real);
                    stmt.AddParameter("DigestiveFemales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryMales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryFemales", SqliteType.Real);
                    stmt.AddParameter("OtherG2Males", SqliteType.Real);
                    stmt.AddParameter("OtherG2Females", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccMales", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccFemales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjMales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjFemales", SqliteType.Real);
                    stmt.AddParameter("IntenInjMales", SqliteType.Real);
                    stmt.AddParameter("IntenInjFemales", SqliteType.Real);
                    stmt.AddParameter("DiabetesMales", SqliteType.Real);
                    stmt.AddParameter("DiabetesFemales", SqliteType.Real);
                    stmt.AddParameter("AIDSMales", SqliteType.Real);
                    stmt.AddParameter("AIDSFemales", SqliteType.Real);
                    stmt.AddParameter("ComDisNoAIDSMales", SqliteType.Real);
                    stmt.AddParameter("ComDisNoAIDSFemales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaMales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaFemales", SqliteType.Real);
                    stmt.AddParameter("MalariaMales", SqliteType.Real);
                    stmt.AddParameter("MalariaFemales", SqliteType.Real);
                    stmt.AddParameter("RespInfMales", SqliteType.Real);
                    stmt.AddParameter("RespInfFemales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthMales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthFemales", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = bulk[i, R, 1] + bulk[i, R, 21] + bulk[i, R, 23] + bulk[i, R, 25] + bulk[i, R, 27];
                            stmt.Parameters[4].Value = bulk[i, R, 2] + bulk[i, R, 22] + bulk[i, R, 24] + bulk[i, R, 26] + bulk[i, R, 28];
                            stmt.Parameters[5].Value = bulkC[i, R, 3];
                            stmt.Parameters[6].Value = bulkC[i, R, 4];
                            stmt.Parameters[7].Value = bulkC[i, R, 5];
                            stmt.Parameters[8].Value = bulkC[i, R, 6];
                            stmt.Parameters[9].Value = bulk[i, R, 3];
                            stmt.Parameters[10].Value = bulk[i, R, 4];
                            stmt.Parameters[11].Value = bulk[i, R, 5];
                            stmt.Parameters[12].Value = bulk[i, R, 6];
                            stmt.Parameters[13].Value = bulk[i, R, 7];
                            stmt.Parameters[14].Value = bulk[i, R, 8];
                            stmt.Parameters[15].Value = bulk[i, R, 9];
                            stmt.Parameters[16].Value = bulk[i, R, 10];
                            stmt.Parameters[17].Value = bulk[i, R, 11];
                            stmt.Parameters[18].Value = bulk[i, R, 12];
                            stmt.Parameters[19].Value = bulk[i, R, 13];
                            stmt.Parameters[20].Value = bulk[i, R, 14];
                            stmt.Parameters[21].Value = bulk[i, R, 15];
                            stmt.Parameters[22].Value = bulk[i, R, 16];
                            stmt.Parameters[23].Value = bulk[i, R, 17];
                            stmt.Parameters[24].Value = bulk[i, R, 18];
                            stmt.Parameters[25].Value = bulk[i, R, 19];
                            stmt.Parameters[26].Value = bulk[i, R, 20];
                            stmt.Parameters[27].Value = bulk[i, R, 21];
                            stmt.Parameters[28].Value = bulk[i, R, 22];
                            stmt.Parameters[29].Value = bulk[i, R, 1];
                            stmt.Parameters[30].Value = bulk[i, R, 2];
                            stmt.Parameters[31].Value = bulk[i, R, 23];
                            stmt.Parameters[32].Value = bulk[i, R, 24];
                            stmt.Parameters[33].Value = bulk[i, R, 25];
                            stmt.Parameters[34].Value = bulk[i, R, 26];
                            stmt.Parameters[35].Value = bulk[i, R, 27];
                            stmt.Parameters[36].Value = bulk[i, R, 28];
                            stmt.Parameters[37].Value = bulk[i, R, 29];
                            stmt.Parameters[38].Value = bulk[i, R, 30];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMorbDet(Double[,,] bulk, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [morb_det_base_case] (
                        [TimeId],
                        [RegionId], 
                        [CohortId], 
                        [MorbPerMalesComDis],
                        [MorbPerFeMalesComDis],
                        [MorbPerMalesNonComDis],
                        [MorbPerFeMalesNonComDis],
                        [MorbPerMalesInjuries],
                        [MorbPerFeMalesInjuries],
                        [MaligNeoPlasMales],
                        [MaligNeoPlasFemales],
                        [CardioVascMales],
                        [CardioVascFemales],
                        [DigestiveMales],
                        [DigestiveFemales],
                        [RespiratoryMales],
                        [RespiratoryFemales],
                        [OtherG2Males],
                        [OtherG2Females],
                        [RoadTrafficAccMales],
                        [RoadTrafficAccFemales],
                        [OtherunintInjMales],
                        [OtherunintInjFemales],
                        [IntenInjMales],
                        [IntenInjFemales],
                        [DiabetesMales],
                        [DiabetesFemales],
                        [AIDSMales],
                        [AIDSFemales],
                        [ComDisNoAIDSMales],
                        [ComDisNoAIDSFemales],
                        [DiarrheaMales],
                        [DiarrheaFemales],
                        [MalariaMales],
                        [MalariaFemales],
                        [RespInfMales],
                        [RespInfFemales],
                        [MentalHealthMales],
                        [MentalHealthFemales]
                    ) VALUES (
                        $TimeId,
                        $RegionId, 
                        $CohortId, 
                        $MorbPerMalesComDis,
                        $MorbPerFeMalesComDis,
                        $MorbPerMalesNonComDis,
                        $MorbPerFeMalesNonComDis,
                        $MorbPerMalesInjuries,
                        $MorbPerFeMalesInjuries,
                        $MaligNeoPlasMales,
                        $MaligNeoPlasFemales,
                        $CardioVascMales,
                        $CardioVascFemales,
                        $DigestiveMales,
                        $DigestiveFemales,
                        $RespiratoryMales,
                        $RespiratoryFemales,
                        $OtherG2Males,
                        $OtherG2Females,
                        $RoadTrafficAccMales,
                        $RoadTrafficAccFemales,
                        $OtherunintInjMales,
                        $OtherunintInjFemales,
                        $IntenInjMales,
                        $IntenInjFemales,
                        $DiabetesMales,
                        $DiabetesFemales,
                        $AIDSMales,
                        $AIDSFemales,
                        $ComDisNoAIDSMales,
                        $ComDisNoAIDSFemales,
                        $DiarrheaMales,
                        $DiarrheaFemales,
                        $MalariaMales,
                        $MalariaFemales,
                        $RespInfMales,
                        $RespInfFemales,
                        $MentalHealthMales,
                        $MentalHealthFemales
                    );";
                    stmt.AddParameter("TimeId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("MorbPerMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MorbPerFeMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MorbPerMalesNonComDis", SqliteType.Real);
                    stmt.AddParameter("MorbPerFeMalesNonComDis", SqliteType.Real);
                    stmt.AddParameter("MorbPerMalesInjuries", SqliteType.Real);
                    stmt.AddParameter("MorbPerFeMalesInjuries", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasMales", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasFemales", SqliteType.Real);
                    stmt.AddParameter("CardioVascMales", SqliteType.Real);
                    stmt.AddParameter("CardioVascFemales", SqliteType.Real);
                    stmt.AddParameter("DigestiveMales", SqliteType.Real);
                    stmt.AddParameter("DigestiveFemales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryMales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryFemales", SqliteType.Real);
                    stmt.AddParameter("OtherG2Males", SqliteType.Real);
                    stmt.AddParameter("OtherG2Females", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccMales", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccFemales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjMales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjFemales", SqliteType.Real);
                    stmt.AddParameter("IntenInjMales", SqliteType.Real);
                    stmt.AddParameter("IntenInjFemales", SqliteType.Real);
                    stmt.AddParameter("DiabetesMales", SqliteType.Real);
                    stmt.AddParameter("DiabetesFemales", SqliteType.Real);
                    stmt.AddParameter("AIDSMales", SqliteType.Real);
                    stmt.AddParameter("AIDSFemales", SqliteType.Real);
                    stmt.AddParameter("ComDisNoAIDSMales", SqliteType.Real);
                    stmt.AddParameter("ComDisNoAIDSFemales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaMales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaFemales", SqliteType.Real);
                    stmt.AddParameter("MalariaMales", SqliteType.Real);
                    stmt.AddParameter("MalariaFemales", SqliteType.Real);
                    stmt.AddParameter("RespInfMales", SqliteType.Real);
                    stmt.AddParameter("RespInfFemales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthMales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthFemales", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = i;
                            stmt.Parameters[3].Value = bulk[i, R, 1] + bulk[i, R, 21] + bulk[i, R, 23] + bulk[i, R, 25] + bulk[i, R, 27];
                            stmt.Parameters[4].Value = bulk[i, R, 2] + bulk[i, R, 22] + bulk[i, R, 24] + bulk[i, R, 26] + bulk[i, R, 28];
                            stmt.Parameters[5].Value = bulk[i, R, 3] + bulk[i, R, 5] + bulk[i, R, 7] + bulk[i, R, 9] + bulk[i, R, 11] + bulk[i, R, 19] + bulk[i, R, 29];
                            stmt.Parameters[6].Value = bulk[i, R, 4] + bulk[i, R, 6] + bulk[i, R, 8] + bulk[i, R, 10] + bulk[i, R, 12] + bulk[i, R, 20] + bulk[i, R, 30];
                            stmt.Parameters[7].Value = bulk[i, R, 13] + bulk[i, R, 15] + bulk[i, R, 17];
                            stmt.Parameters[8].Value = bulk[i, R, 14] + bulk[i, R, 16] + bulk[i, R, 18];
                            stmt.Parameters[9].Value = bulk[i, R, 3];
                            stmt.Parameters[10].Value = bulk[i, R, 4];
                            stmt.Parameters[11].Value = bulk[i, R, 5];
                            stmt.Parameters[12].Value = bulk[i, R, 6];
                            stmt.Parameters[13].Value = bulk[i, R, 7];
                            stmt.Parameters[14].Value = bulk[i, R, 8];
                            stmt.Parameters[15].Value = bulk[i, R, 9];
                            stmt.Parameters[16].Value = bulk[i, R, 10];
                            stmt.Parameters[17].Value = bulk[i, R, 11];
                            stmt.Parameters[18].Value = bulk[i, R, 12];
                            stmt.Parameters[19].Value = bulk[i, R, 13];
                            stmt.Parameters[20].Value = bulk[i, R, 14];
                            stmt.Parameters[21].Value = bulk[i, R, 15];
                            stmt.Parameters[22].Value = bulk[i, R, 16];
                            stmt.Parameters[23].Value = bulk[i, R, 17];
                            stmt.Parameters[24].Value = bulk[i, R, 18];
                            stmt.Parameters[25].Value = bulk[i, R, 19];
                            stmt.Parameters[26].Value = bulk[i, R, 20];
                            stmt.Parameters[27].Value = bulk[i, R, 21];
                            stmt.Parameters[28].Value = bulk[i, R, 22];
                            stmt.Parameters[29].Value = bulk[i, R, 1];
                            stmt.Parameters[30].Value = bulk[i, R, 2];
                            stmt.Parameters[31].Value = bulk[i, R, 23];
                            stmt.Parameters[32].Value = bulk[i, R, 24];
                            stmt.Parameters[33].Value = bulk[i, R, 25];
                            stmt.Parameters[34].Value = bulk[i, R, 26];
                            stmt.Parameters[35].Value = bulk[i, R, 27];
                            stmt.Parameters[36].Value = bulk[i, R, 28];
                            stmt.Parameters[37].Value = bulk[i, R, 29];
                            stmt.Parameters[38].Value = bulk[i, R, 30];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertStunt(Double[,,] bulk, int NCohorts, int NR, int TimeId)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [StuntingByAge] (
                        [Years],
                        [NCohorts],
                        [Region],
                        [StuntingMales],
                        [StuntingFemales]
                    ) VALUES (
                        $Years,
                        $NCohorts,
                        $Region,
                        $StuntingMales,
                        $StuntingFemales
                    );";
                    stmt.AddParameter("Years", SqliteType.Integer);
                    stmt.AddParameter("NCohorts", SqliteType.Integer);
                    stmt.AddParameter("Region", SqliteType.Integer);
                    stmt.AddParameter("StuntingMales", SqliteType.Real);
                    stmt.AddParameter("StuntingFemales", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = TimeId;
                            stmt.Parameters[1].Value = i;
                            stmt.Parameters[2].Value = R;
                            stmt.Parameters[3].Value = bulk[i, R, 1];
                            stmt.Parameters[4].Value = bulk[i, R, 2];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMorDetNormFactors(Double[,,] bulk, int NCohorts, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [mor_det_norm_factors] (
                        [CohortId],
                        [RegionId],
                        [Males],
                        [Females]
                    ) VALUES (
                        $CohortId, 
                        $RegionId, 
                        $Males,
                        $Females
                    );";
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("Males", SqliteType.Real);
                    stmt.AddParameter("Females", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = i;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = bulk[i, R, 1];
                            stmt.Parameters[3].Value = bulk[i, R, 2];
                            stmt.ExecuteNonQuery();
                        }
                    }                    
                    tx.Commit();
                }
            }
        }

        public void BulkInsertMorDetFactors(Double[,,] bulk, int NCohorts, int NR)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [mor_det_factors] (
                        [CohortId], 
                        [RegionId], 
                        [MortPerMalesComDis],
                        [MortPerFeMalesComDis],
                        [MaligNeoPlasMales],
                        [MaligNeoPlasFemales],
                        [CardioVascMales],
                        [CardioVascFemales],
                        [DigestiveMales],
                        [DigestiveFemales],
                        [RespiratoryMales],
                        [RespiratoryFemales],
                        [OtherG2Males],
                        [OtherG2Females],
                        [RoadTrafficAccMales],
                        [RoadTrafficAccFemales],
                        [OtherunintInjMales],
                        [OtherunintInjFemales],
                        [IntenInjMales],
                        [IntenInjFemales],
                        [DiabetesMales],
                        [DiabetesFemales],
                        [DiarrheaMales],
                        [DiarrheaFemales],
                        [MalariaMales],
                        [MalariaFemales],
                        [RespInfMales],
                        [RespInfFemales],
                        [MentalHealthMales],
                        [MentalHealthFemales]
                    ) VALUES (
                        $CohortId, 
                        $RegionId, 
                        $MortPerMalesComDis,
                        $MortPerFeMalesComDis,
                        $MaligNeoPlasMales,
                        $MaligNeoPlasFemales,
                        $CardioVascMales,
                        $CardioVascFemales,
                        $DigestiveMales,
                        $DigestiveFemales,
                        $RespiratoryMales,
                        $RespiratoryFemales,
                        $OtherG2Males,
                        $OtherG2Females,
                        $RoadTrafficAccMales,
                        $RoadTrafficAccFemales,
                        $OtherunintInjMales,
                        $OtherunintInjFemales,
                        $IntenInjMales,
                        $IntenInjFemales,
                        $DiabetesMales,
                        $DiabetesFemales,
                        $DiarrheaMales,
                        $DiarrheaFemales,
                        $MalariaMales,
                        $MalariaFemales,
                        $RespInfMales,
                        $RespInfFemales,
                        $MentalHealthMales,
                        $MentalHealthFemales
                    );";
                    stmt.AddParameter("CohortId", SqliteType.Integer);
                    stmt.AddParameter("RegionId", SqliteType.Integer);
                    stmt.AddParameter("MortPerMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MortPerFeMalesComDis", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasMales", SqliteType.Real);
                    stmt.AddParameter("MaligNeoPlasFemales", SqliteType.Real);
                    stmt.AddParameter("CardioVascMales", SqliteType.Real);
                    stmt.AddParameter("CardioVascFemales", SqliteType.Real);
                    stmt.AddParameter("DigestiveMales", SqliteType.Real);
                    stmt.AddParameter("DigestiveFemales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryMales", SqliteType.Real);
                    stmt.AddParameter("RespiratoryFemales", SqliteType.Real);
                    stmt.AddParameter("OtherG2Males", SqliteType.Real);
                    stmt.AddParameter("OtherG2Females", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccMales", SqliteType.Real);
                    stmt.AddParameter("RoadTrafficAccFemales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjMales", SqliteType.Real);
                    stmt.AddParameter("OtherunintInjFemales", SqliteType.Real);
                    stmt.AddParameter("IntenInjMales", SqliteType.Real);
                    stmt.AddParameter("IntenInjFemales", SqliteType.Real);
                    stmt.AddParameter("DiabetesMales", SqliteType.Real);
                    stmt.AddParameter("DiabetesFemales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaMales", SqliteType.Real);
                    stmt.AddParameter("DiarrheaFemales", SqliteType.Real);
                    stmt.AddParameter("MalariaMales", SqliteType.Real);
                    stmt.AddParameter("MalariaFemales", SqliteType.Real);
                    stmt.AddParameter("RespInfMales", SqliteType.Real);
                    stmt.AddParameter("RespInfFemales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthMales", SqliteType.Real);
                    stmt.AddParameter("MentalHealthFemales", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 0; i <= NCohorts; i++)
                        {
                            stmt.Parameters[0].Value = i;
                            stmt.Parameters[1].Value = R;
                            stmt.Parameters[2].Value = bulk[i, R, 1];
                            stmt.Parameters[3].Value = bulk[i, R, 2];
                            stmt.Parameters[4].Value = bulk[i, R, 3];
                            stmt.Parameters[5].Value = bulk[i, R, 4];
                            stmt.Parameters[6].Value = bulk[i, R, 5];
                            stmt.Parameters[7].Value = bulk[i, R, 6];
                            stmt.Parameters[8].Value = bulk[i, R, 7];
                            stmt.Parameters[9].Value = bulk[i, R, 8];
                            stmt.Parameters[10].Value = bulk[i, R, 9];
                            stmt.Parameters[11].Value = bulk[i, R, 10];
                            stmt.Parameters[12].Value = bulk[i, R, 11];
                            stmt.Parameters[13].Value = bulk[i, R, 12];
                            stmt.Parameters[14].Value = bulk[i, R, 13];
                            stmt.Parameters[15].Value = bulk[i, R, 14];
                            stmt.Parameters[16].Value = bulk[i, R, 15];
                            stmt.Parameters[17].Value = bulk[i, R, 16];
                            stmt.Parameters[18].Value = bulk[i, R, 17];
                            stmt.Parameters[19].Value = bulk[i, R, 18];
                            stmt.Parameters[20].Value = bulk[i, R, 19];
                            stmt.Parameters[21].Value = bulk[i, R, 20];
                            stmt.Parameters[22].Value = bulk[i, R, 23];
                            stmt.Parameters[23].Value = bulk[i, R, 24];
                            stmt.Parameters[24].Value = bulk[i, R, 25];
                            stmt.Parameters[25].Value = bulk[i, R, 26];
                            stmt.Parameters[26].Value = bulk[i, R, 27];
                            stmt.Parameters[27].Value = bulk[i, R, 28];
                            stmt.Parameters[28].Value = bulk[i, R, 29];
                            stmt.Parameters[29].Value = bulk[i, R, 30];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertHLProxDrivers(Single[] bulk, int NR, int TimeId, string VarName)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [HLPROXIMATEDRIVERS] (
                        [VarName],
                        [Year],
                        [Region],
                        [Dim2],
                        [Dim3],
                        [DistalFormVal]
                    ) VALUES (
                        $VarName,
                        $Year,
                        $Region,
                        $Dim2,
                        $Dim3,
                        $DistalFormVal
                    );";
                    stmt.AddParameter("VarName", SqliteType.Integer);
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Region", SqliteType.Integer);
                    stmt.AddParameter("Dim2", SqliteType.Integer);
                    stmt.AddParameter("Dim3", SqliteType.Integer);
                    stmt.AddParameter("DistalFormVal", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        stmt.Parameters[0].Value = VarName;
                        stmt.Parameters[1].Value = TimeId;
                        stmt.Parameters[2].Value = R;
                        stmt.Parameters[3].Value = 0;
                        stmt.Parameters[4].Value = 0;
                        stmt.Parameters[5].Value = bulk[R];
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertHLProxDrivers(Single[,] bulk, int NR, int TimeId, string VarName, int Dim2)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [HLPROXIMATEDRIVERS] (
                        [VarName],
                        [Year],
                        [Region],
                        [Dim2],
                        [Dim3],
                        [DistalFormVal]
                    ) VALUES (
                        $VarName,
                        $Year,
                        $Region,
                        $Dim2,
                        $Dim3,
                        $DistalFormVal
                    );";
                    stmt.AddParameter("VarName", SqliteType.Integer);
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Region", SqliteType.Integer);
                    stmt.AddParameter("Dim2", SqliteType.Integer);
                    stmt.AddParameter("Dim3", SqliteType.Integer);
                    stmt.AddParameter("DistalFormVal", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        for (int i = 1; i <= Dim2; i++)
                        {
                            stmt.Parameters[0].Value = VarName;
                            stmt.Parameters[1].Value = TimeId;
                            stmt.Parameters[2].Value = R;
                            stmt.Parameters[3].Value = i;
                            stmt.Parameters[4].Value = i;
                            stmt.Parameters[5].Value = bulk[R, i];
                            stmt.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertHLProxDriversT(Single[,] bulk, Double[,] bulkP, int NR, int TimeId, string VarName)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [HLPROXIMATEDRIVERS] (
                        [VarName],
                        [Year],
                        [Region],
                        [Dim2],
                        [Dim3],
                        [DistalFormVal]
                    ) VALUES (
                        $VarName,
                        $Year,
                        $Region,
                        $Dim2,
                        $Dim3,
                        $DistalFormVal
                    );";
                    stmt.AddParameter("VarName", SqliteType.Integer);
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Region", SqliteType.Integer);
                    stmt.AddParameter("Dim2", SqliteType.Integer);
                    stmt.AddParameter("Dim3", SqliteType.Integer);
                    stmt.AddParameter("DistalFormVal", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        stmt.Parameters[0].Value = VarName;
                        stmt.Parameters[1].Value = TimeId;
                        stmt.Parameters[2].Value = R;
                        stmt.Parameters[3].Value = 3;
                        stmt.Parameters[4].Value = 0;
                        stmt.Parameters[5].Value = (bulk[R, 1]* bulkP[R, 1]+ bulk[R, 2] * bulkP[R, 2])/ (bulkP[R, 1] + bulkP[R, 2]);
                        stmt.ExecuteNonQuery();
                }
                    tx.Commit();
                }
            }
        }

        public void BulkInsertHLProxDrivers(Single bulk, int NR, int TimeId, string VarName)
        {
            using (var tx = this.conn.BeginTransaction())
            {
                using (var stmt = this.conn.CreateCommand())
                {
                    stmt.CommandText = @"INSERT INTO [HLPROXIMATEDRIVERS] (
                        [VarName],
                        [Year],
                        [Region],
                        [Dim2],
                        [Dim3],
                        [DistalFormVal]
                    ) VALUES (
                        $VarName,
                        $Year,
                        $Region,
                        $Dim2,
                        $Dim3,
                        $DistalFormVal
                    );";
                    stmt.AddParameter("VarName", SqliteType.Integer);
                    stmt.AddParameter("Year", SqliteType.Integer);
                    stmt.AddParameter("Region", SqliteType.Integer);
                    stmt.AddParameter("Dim2", SqliteType.Integer);
                    stmt.AddParameter("Dim3", SqliteType.Integer);
                    stmt.AddParameter("DistalFormVal", SqliteType.Real);
                    stmt.Prepare();
                    for (int R = 1; R <= NR; R++)
                    {
                        stmt.Parameters[0].Value = VarName;
                        stmt.Parameters[1].Value = TimeId;
                        stmt.Parameters[2].Value = R;
                        stmt.Parameters[3].Value = 0;
                        stmt.Parameters[4].Value = 0;
                        stmt.Parameters[5].Value = bulk;
                        stmt.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }

    }

}
