using System;
using System.Collections.Generic;
using System.IO;

namespace IFs.Data
{
    public interface IStorage: IContext, IDisposable
    {
        // synchronize changes to disk
        void Flush();

        // open a stream for reading the blob data stored under the variable name
        Stream GetVariableStream(string variableName);

        // save the content of a stream under the variable name as its blob data
        void PutVariableStream(string variableName, Stream payload);

        // get the content of the blob data stored under the variable name as a byte array.
        byte[] GetVariableData(string variableName);

        // save the content of a byte array under the variable name as its blob data.
        void PutVariableData(string variableName, byte[] payload);

        // get the blob data of all variables as key-value pairs.
        IEnumerable<KeyValuePair<string, byte[]>> GetAllVariableData();

    }

    public interface IStorageService : IDisposable
    {
        // create or update the skeleton storage with schema and metadata. When `truncate` is true, the storage will
        // be re-initialized with empty schema, metadata about variables, dimensions, and regressions will be cloned
        // from the provided `source` context into the truncated storage (if `source` is not specified, the storage
        // service SHOULD fallback to use a default source); When otherwise `truncate` is false, the schema of the
        // storage will be migrated to a compatible version, but the existing content of the storage will remain
        // intact as long as they are not involved in the schema migration.
        IStorage Open(bool truncate = false, IContext source = null);

        //jrs 2024/03/08
        void DisableMigration();
    }

}
