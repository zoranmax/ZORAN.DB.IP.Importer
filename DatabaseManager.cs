/*
   Copyright 2012 Zoran Maksimovic (zoran.maksimovich@gmail.com 
   http://www.agile-code.com)

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System.Data;
using System.Data.SqlClient;

namespace ZORAN.DB.IP.Importer
{
    public class InsertIntoDatabase
    {
        /// <summary>
        /// Returns a DataTable representing the entry in the DB-IP database.
        /// </summary>
        /// <returns>System.Data.DataTable object</returns>
        public static DataTable GetDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("Ip_Start", typeof (string)));
            dt.Columns.Add(new DataColumn("Ip_End", typeof (string)));
            dt.Columns.Add(new DataColumn("city", typeof (string)));
            dt.Columns.Add(new DataColumn("region", typeof (string)));
            dt.Columns.Add(new DataColumn("country", typeof (string)));
            dt.Columns.Add(new DataColumn("Type", typeof (int)));
            return dt;
        }

        /// <summary>
        /// Inserts the data first into the staging table and then replaces the data into the destination table..
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="dt"></param>
        public static void BulkInsert(string connString, DataTable dt)
        {
            string tableName = "dbo.dbip_city_stage";
            using (var cn = new SqlConnection(connString))
            {
                cn.Open();

                TruncateTable(cn, tableName);

                using (var bulkCopy = new SqlBulkCopy(cn))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.WriteToServer(dt);
                }

                MoveDataFromStageToLive(cn);
                TruncateTable(cn, tableName);
                cn.Close();
            }
        }

        private static void TruncateTable(SqlConnection connection, string tablename)
        {
            SqlCommand comm = connection.CreateCommand();
            comm.CommandText = "TRUNCATE TABLE " + tablename;
            comm.ExecuteNonQuery();
        }

        private static void MoveDataFromStageToLive(SqlConnection connection)
        {
            using (SqlTransaction tx = connection.BeginTransaction())
            {
                var command = connection.CreateCommand();
                command.CommandText = "truncate table dbip_city \n insert into dbip_city select * From dbip_city_stage";
                command.CommandTimeout = 0;
                command.Transaction = tx;
                command.ExecuteNonQuery();
                tx.Commit();
            }
        }
    }
}