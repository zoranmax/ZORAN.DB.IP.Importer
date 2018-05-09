/*
   Copyright 2018 Zoran Maksimovic (zoran.maksimovich@gmail.com 
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

using System.Configuration;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace ZORAN.DB.IP.Importer
{
    internal class Program
    {
        private static readonly string ConnString = ConfigurationManager.ConnectionStrings["DB_IP"].ToString();
        private static readonly string DbIpCityFile = ConfigurationManager.AppSettings["db-ip-file-path"].ToString(CultureInfo.InvariantCulture);

        private static void Main(string[] args)
        {
            var csv = new CsvReader(new StreamReader(DbIpCityFile), true);
            csv.Configuration.HasHeaderRecord = false;
            var items = csv.GetRecords<DbIpCity>();
            var dt = DatabaseManager.GetDataTable();
            
            foreach (var item in items)
            {                
                var row = dt.NewRow();
                row["Ip_Start"] = item.IpStart;
                row["Ip_End"] = item.IpEnd;
                row["city"] = item.City;
                row["region"] = item.Region;
                row["country"] = item.Country;
                row["type"] = item.Type;
                dt.Rows.Add(row);
            }

            DatabaseManager.BulkInsert(ConnString, dt);
        }
    }
}
