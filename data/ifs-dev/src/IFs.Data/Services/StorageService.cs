using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace IFs.Data
{

    public class StorageService: IStorageService
    {
        protected readonly IContextService ContextSvc;

        private bool migrated;

        public StorageService(IContextService contextSvc = null) {
            Debug.WriteLine("initializing storage");
            this.ContextSvc = contextSvc;
            this.migrated = false;
            Debug.WriteLine("initialized storage");
        }

        //jrs 2019/12/13
        private string _sWorkingFileName;

        public StorageService(IContextService contextSvc = null, string sWorkingFileName = "")
        {
            Debug.WriteLine("initializing storage");
            this.ContextSvc = contextSvc;
            this.migrated = false;
            this._sWorkingFileName = sWorkingFileName;
            Debug.WriteLine("initialized storage");
        }

        public IStorage Open(bool truncate, IContext source = null) {

            //jrs 2019/12/13
            StorageContext db;
            if (_sWorkingFileName == "")
                { db = new StorageContext(); }
            else
                { db = new StorageContext(_sWorkingFileName); }
                
            if (truncate) {
                if (source != null) {
                    Truncate(db, source);
                } else if (this.ContextSvc != null) {
                    Debug.WriteLine("source is not specified: initialize with a temporary context.");
                    using (var temp = this.ContextSvc.ForRebuild()) {
                        Truncate(db, temp);
                    }
                } else {
                    throw new NotSupportedException("No context present for initializing the storage.");
                }
                migrated = true;
            } else if (!migrated) {
                //Migrate(db);
                migrated = true;
            }
            return db.Enter();
        }

        private void Truncate(StorageContext db, IContext source) {
            Debug.WriteLine("constructing database");
            db.Database.EnsureDeleted();
            db.Database.Migrate();
            // initialize with metadata
            db.ChangeTracker.AutoDetectChangesEnabled = false;
            Debug.Indent();
            using (var tx = db.Database.BeginTransaction()) {
                // dimensions metadata
                int nDim = 0;
                foreach (var d in source.Dimensions) {
                    db.PutDimension(d);
                    ++nDim;
                }
                Debug.WriteLine("- dimensions: {0}", nDim);
                // variables metadata
                int nVar = 0;
                foreach (var v in source.Variables) {
                    db.PutVariable(v);
                    ++nVar;
                }
                Debug.WriteLine("- variables: {0}", nVar);
                // regressions metadata
                int nReg = 0;
                foreach (var r in source.Regressions) {
                    db.PutRegression(r);
                    ++nReg;
                }
                Debug.WriteLine("- regressions: {0}", nReg);
                // flush changes to database
                db.SaveChanges(true);
                tx.Commit();

            }
            Debug.Unindent();
            // db.ChangeTracker.AutoDetectChangesEnabled = true;
            Debug.WriteLine("constructed database");
        }

        private void Migrate(StorageContext db) {
            //jrs 2019/06/08 added this line to avoid error of not finding older version of System.ComponentModel.Annotations
            FunctionsAssemblyResolver.RedirectAssembly();
            Debug.WriteLine("migrating database");
            db.Database.Migrate();
            // TODO: metadata verification logic
            Debug.WriteLine("migrated database");
        }

        public void Dispose() {
            Debug.WriteLine("disposing storage");
            // NOP
            Debug.WriteLine("disposed storage");
        }

        //jrs 2024/03/08
        public void DisableMigration() {
            this.migrated = true;
        }

    }

}
