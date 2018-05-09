ZORAN.DB.IP.Importer
====================

ZORAN.DB.IP.Importer is a tool that imports into Microsoft SQL Server the freely available geolocation
database offered by www.db-ip.com. 

Documentation 
====================

For more information please check the documentation https://github.com/zoranmax/ZORAN.DB.IP.Importer/wiki
or here http://www.agile-code.com/blog/import-db-ip-com-database-data-to-microsoft-sql-server-with-zoran-db-ip-importer-tool/

Downloads
====================

Download the compiled application 
- <a href="http://bit.ly/Wzjifg">ZORAN.DB-IP-Importer_v1.0.0.zip</a>
- <a href="https://www.dropbox.com/s/8bnswxx13qt2w3r/ZORAN.DB-IP.Importer_v1.1.0.0.zip">ZORAN.DB-IP-Importer_v1.1.0.zip</a>

How to run the application?
===========================
1. Download the free database from this location http://www.db-ip.com/db/download/city. The database comes in the CSV format.
2. Change the Connection String in the ZORAN.DB-IP-Importer.exe.config file to point to the Microsoft SQL Server database. This is done by changing the value of the connection string named “DB_IP”
3. Once again, in the file ZORAN.DB-IP-Importer.exe.config, enter the path to the CSV file by changing the value of the AppSettings value “db-ip-file-path”
4. Run the two provided scripts dbip_city.sql and dbip_city_stage.sql against your database. This will create two tables (staging and the “live” table). Obviously this should be done only once , the first time you use this application.
5. Run the application.

Yes, but how does it work?
===========================
In order to avoid any complex management of the inserts and updates of the already existing data, I’ve chosen the approach of the staging table and the “live” table.

The importing process operations are as follows:
1. Read the CSV file (for the time being the file is fully loaded in memory. With the latest CSV files this might be an issue for some as the file has grown significantly). 
2. Truncate the data from the “Staging” table
3. Load the data from the CSV file to the “Staging” table
4. In a transaction, move all the data from the “staging” table to the “live” table.

Licence 
====================
http://www.apache.org/licenses/LICENSE-2.0
