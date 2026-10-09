using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace IFs.Data 
{

    // stream-based serialization / deserialization for variable data
    internal class VariableStream: Stream 
    {
        public static VariableStream ForRead(SqliteConnection conn, string variableName) {
            using (var cmd = conn.CreateCommand()) {
                cmd.CommandText = @"SELECT ifnull(length(vb.Data), 0) 
                    FROM ifs_var AS v LEFT JOIN ifs_var_blob AS vb ON (v.Name = vb.VariableName) 
                    WHERE v.Name = $name";
                cmd.Parameters.AddWithValue("$name", variableName);
                using (var reader = cmd.ExecuteReader()) {
                    if (!reader.Read()) {
                        throw new ArgumentOutOfRangeException(variableName, $"No such variable {variableName}.");
                    }
                    // if Data is NULL, don't even borther to allocate a VariableData stream
                    if (reader.GetInt64(0) <= 0L) {
                        return null;
                    }
                }
            }
            return new VariableStream(conn, variableName, false);
        }

        public static VariableStream ForWrite(SqliteConnection conn, string variableName, long size) {
            using (var cmd = conn.CreateCommand()) {
                cmd.CommandText = @"REPLACE INTO ifs_var_blob(VariableName, Data) VALUES($name, zeroblob($size))";
                cmd.Parameters.AddWithValue("$name", variableName);
                cmd.Parameters.AddWithValue("$size", size);
                cmd.ExecuteNonQuery();
            }
            return new VariableStream(conn, variableName, true);
        }

        public static void Nullify(SqliteConnection conn, string variableName) {
            using (var cmd = conn.CreateCommand()) {
                cmd.CommandText = @"REPLACE INTO ifs_var_blob(VariableName, Data) VALUES($name, NULL)";
                cmd.Parameters.AddWithValue("$name", variableName);
                cmd.ExecuteNonQuery();
            }
        }

        private static long getRowId(SqliteConnection conn, string variableName) {
            using (var cmd = conn.CreateCommand()) {
                cmd.CommandText = @"SELECT rowid FROM ifs_var_blob WHERE VariableName = $name";
                cmd.Parameters.AddWithValue("$name", variableName);
                using (var reader = cmd.ExecuteReader()) {
                    if (!reader.Read()) {
                        throw new ArgumentOutOfRangeException(variableName, $"No such variable {variableName}.");
                    }
                    return reader.GetInt64(0);
                }
            }
        }

        private bool _is_opened = false;
        private readonly sqlite3 _db;
        private readonly sqlite3_blob _blob;
        private readonly int _length;
        private readonly bool _writeable;
        int _cursor;

        VariableStream(SqliteConnection conn, string variableName, bool writeable)
            : base()
        {
            long rowid = getRowId(conn, variableName);
            this._db = conn.Handle;
            // get blob handle
            int rc = raw.sqlite3_blob_open(this._db, "main", "ifs_var_blob", "Data", rowid, (writeable) ? 1 : 0, out this._blob);
            SqliteException.ThrowExceptionForRC(rc, this._db);
            // get blob length in bytes
            this._is_opened = true;
            this._length = raw.sqlite3_blob_bytes(this._blob);
            this._writeable = writeable;
        }

        ~VariableStream() {
            if (this._is_opened) {
                this.Close();
            }
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => _writeable;

        public override long Length => _length;

        public override long Position
        {
            get => _cursor;
            set => Seek(value, SeekOrigin.Begin);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null) {
                throw new ArgumentNullException("buffer", "Buffer is null.");
            }
            // buffer overflow
            if (offset + count > buffer.Length) {
                throw new ArgumentOutOfRangeException("buffer", "Insufficient buffer size.");
            }
            int n = (this._length - this._cursor);
            if (n <= 0) {
                count = 0;
            } else if (n <= count) {
                count = n;
            }
            if (count > 0) {
            	//gvdb 2024/04/05
        		// update for new sqlite lib syntax
                //int rc = raw.sqlite3_blob_read(this._blob, buffer, offset, count, this._cursor);
                int rc = raw.sqlite3_blob_read(this._blob, buffer, offset /*, count, this._cursor*/);
                SqliteException.ThrowExceptionForRC(rc, this._db);
                this.Seek(count, SeekOrigin.Current);
            }
            return count;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!this._writeable) {
                throw new NotSupportedException("Can't write.");
            }
            if (buffer == null) {
                throw new ArgumentNullException("buffer", "Buffer is null.");
            }
            // buffer overflow
            if (offset + count > buffer.Length) {
                throw new ArgumentOutOfRangeException("buffer", "Insiffucient buffer size.");
            }
            if (count > 0) {
            	//gvdb 2024/04/05
        		// update for new sqlite lib syntax
                //int rc = raw.sqlite3_blob_write(_blob, buffer, offset, count, this._cursor);
                int rc = raw.sqlite3_blob_write(_blob, buffer, offset/*, count, this._cursor*/);
                SqliteException.ThrowExceptionForRC(rc, this._db);
                this.Seek(count, SeekOrigin.Current);
            }
        }

        public override void Flush()
        {
            // NOP
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long pos;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    pos = (int) offset;
                    break;
                case SeekOrigin.Current:
                    pos = this._cursor + (int) offset;
                    break;
                case SeekOrigin.End:
                    pos = this._length + (int) offset;
                    break;
                default:
                    throw new ArgumentException("Invalid seek origin: " + origin, nameof(origin));
            }
            // underflow
            if (pos < 0) {
                throw new ArgumentOutOfRangeException("offset", "Seek underflow.");
            }
            // overflow
            if (pos > this._length) {
                throw new ArgumentOutOfRangeException("offset", "Seek overflow.");
            }
            return this._cursor = (int) pos;
        }

        public override void SetLength(long value)
            => throw new NotSupportedException("Can't resize.");

        public override void Close()
        {
            int rc = raw.sqlite3_blob_close(this._blob);
            SqliteException.ThrowExceptionForRC(rc, this._db);
            this._is_opened = false;
        }

    }

}
