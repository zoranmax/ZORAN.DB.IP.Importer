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

using CsvHelper.Configuration;

namespace ZORAN.DB.IP.Importer
{
    public class DbIpCity
    {
        [CsvField(Index = 0)]
        public string IpStart { get; set; }

        [CsvField(Index = 1)]
        public string IpEnd { get; set; }
         
        [CsvField(Index = 2)]
        public string Country { get; set; }

        [CsvField(Index = 3)]
        public string Region { get; set; }

        [CsvField(Index = 4)]
        public string City { get; set; }

        [CsvField(Ignore=true)]
        public int Type
        {
            get
            {
                if (IpStart.Contains(":"))
                    return 6;
                else
                    return 4;
            }
        }
    }
}