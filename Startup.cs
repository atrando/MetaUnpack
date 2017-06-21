using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Text;
using System.Globalization;

namespace MetaUnpackProject
{
    public class Startup
    {
        MySqlConnection connection = new MySqlConnection
        {
            ConnectionString = "server=127.0.0.1;user id=root;password=Tb@mptdda;port=3306;database=metapackproject"
        };
        public void OpenConnection(MySqlConnection connect)
        {
            try
            {
                connect.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        QueryHelper queryHelper = new QueryHelper();

        public string GetAddresses(string jsonRequest)
        {
            if (jsonRequest != null)
            {
                string countAddresses;
                string country;
                string city;
                if (jsonRequest.Contains("CSV"))
                {
                    Root getAddressesCSV = JsonConvert.DeserializeObject<Root>(jsonRequest);
                    countAddresses = getAddressesCSV.GetAddressesCSV.COUNT;
                    country = getAddressesCSV.GetAddressesCSV.ADDRESS_COUNTRY;
                    city = getAddressesCSV.GetAddressesCSV.ADDRESS_CITY;
                }
                else
                {
                    Root getAddresses = JsonConvert.DeserializeObject<Root>(jsonRequest);
                    countAddresses = getAddresses.GetAddresses.COUNT;
                    country = getAddresses.GetAddresses.ADDRESS_COUNTRY;
                    city = getAddresses.GetAddresses.ADDRESS_CITY;
                }

                StringBuilder sbGetAddressessQuery = new StringBuilder("SELECT * FROM tb_evls_address");
                StringBuilder getAddressResponse = new StringBuilder();

                if (city != null && !city.Equals(""))
                {
                    sbGetAddressessQuery.Append(" WHERE ADDRESS_CITY = '" + city + "'");
                    if (country != null && !country.Equals(""))
                    {
                        sbGetAddressessQuery.Append(" AND ADDRESS_COUNTRY = '" + country + "'");
                    }
                }
                else if (country != null && !country.Equals(""))
                {
                    sbGetAddressessQuery.Append(" WHERE ADDRESS_COUNTRY = '" + country + "'");
                }

                if (!countAddresses.Equals(""))
                {
                    sbGetAddressessQuery.Append(" ORDER BY RAND() LIMIT " + countAddresses);
                }
                else
                {
                    sbGetAddressessQuery.Append(" ORDER BY RAND() LIMIT 1");
                }

                OpenConnection(connection);
                MySqlCommand cmdGetAddress = new MySqlCommand(sbGetAddressessQuery.ToString(), connection);
                MySqlDataReader readerGetAddress = cmdGetAddress.ExecuteReader();

                if (jsonRequest.Contains("CSV"))
                {
                    string path = @"C:\Users\atran\Desktop\MetaUnpackProject\getAddresses.csv";
                    queryHelper.SqlDataReaderToCSV(readerGetAddress, path);
                    string content = queryHelper.ConvertFileToBase64(path);
                    getAddressResponse.Append("{\n \tContent:\"" + content + "\"");
                }
                else
                {
                    while (readerGetAddress.Read() && readerGetAddress.HasRows)
                    {
                        GetAddressResponse responeObject = new GetAddressResponse(readerGetAddress);
                        var respone = JsonConvert.SerializeObject(responeObject, Formatting.Indented);
                        getAddressResponse.Append(respone.ToString());
                    }
                }

                connection.Close();
                return getAddressResponse.ToString();
            }
            return "";
        }

        public string ImportAddresses(string jsonRequest)
        {
            if (jsonRequest != null)
            {
                Root importRequest = JsonConvert.DeserializeObject<Root>(jsonRequest);
                StringBuilder importAddressResponse = new StringBuilder();
                var conString = importRequest.ImportAddress.CONNECTION_STRING;
                var ADDRESS_COUNTRY = importRequest.ImportAddress.COUNTRY;

                if (conString != null && !conString.Equals(""))
                {
                    MySqlConnection importConnection = new MySqlConnection
                    {
                        ConnectionString = conString
                    };

                    string databaseName = conString.Substring(conString.LastIndexOf('=') + 1);
                    StringBuilder sbImportData = new StringBuilder("INSERT INTO tb_evls_address\nSELECT * FROM " + databaseName + ".tb_import_address ");
                    StringBuilder sbGetAddressesToResponse = new StringBuilder("SELECT * FROM " + databaseName + ".tb_import_address ");

                    if (!ADDRESS_COUNTRY.Equals(""))
                    {
                        sbGetAddressesToResponse.Append("WHERE ADDRESS_COUNTRY = '" + ADDRESS_COUNTRY + "'");
                        sbImportData.Append("WHERE ADDRESS_COUNTRY = '" + ADDRESS_COUNTRY + "'");
                    }

                    OpenConnection(connection);
                    OpenConnection(importConnection);
                    MySqlCommand cmdSelectData = new MySqlCommand(sbImportData.ToString(), connection);
                    try
                    {
                        MySqlDataReader readerSelect = cmdSelectData.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }

                    MySqlCommand cmdGetResponse = new MySqlCommand(sbGetAddressesToResponse.ToString(), importConnection);
                    MySqlDataReader readerGetResponse = cmdGetResponse.ExecuteReader();

                    if (readerGetResponse.HasRows)
                    {
                        while (readerGetResponse.Read())
                        {
                            ImportAddressesResponse responeObject = new ImportAddressesResponse(readerGetResponse);
                            var respone = JsonConvert.SerializeObject(responeObject, Formatting.Indented);
                            importAddressResponse.Append(respone.ToString());
                        }
                    }
                    else
                    {
                        importAddressResponse.Append("No addresses imported, check if there are some addresses in import Database");
                    }
                    connection.Close();
                    importConnection.Close();
                }
                else
                {
                    return "There was a problem with coonection string , make sure that its correct and try again\nExample of correct connection string: \"server=server;user id=root;password=pa$$word;port=port;database=databaseName\"";
                }
                return importAddressResponse.ToString();
            }
            return "";
        }

         public string ExportAddresses(string jsonRequest)
        {
            if (jsonRequest != null)
            {
                Root exportAddress = JsonConvert.DeserializeObject<Root>(jsonRequest);
                StringBuilder exportAddressesResponse = new StringBuilder();
                var conString = exportAddress.ExportAddress.CONNECTION_STRING;
                var ADDRESS_COUNTRY = exportAddress.ExportAddress.COUNTRY;

                if (conString != null && !conString.Equals(""))
                {
                    MySqlConnection importConnection = new MySqlConnection
                    {
                        ConnectionString = conString
                    };

                    string instanceName = conString.Substring(conString.LastIndexOf('=') + 1);
                    StringBuilder sbImportData = new StringBuilder("INSERT INTO tb_evls_address\nSELECT * FROM " + instanceName + ".tb_import_address ");
                    StringBuilder sbGetAddressesToResponse = new StringBuilder("SELECT * FROM " + instanceName + ".tb_import_address ");

                    if (!ADDRESS_COUNTRY.Equals(""))
                    {
                        sbGetAddressesToResponse.Append("WHERE ADDRESS_COUNTRY = '" + ADDRESS_COUNTRY + "'");
                        sbImportData.Append("WHERE ADDRESS_COUNTRY = '" + ADDRESS_COUNTRY + "'");
                    }

                    OpenConnection(connection);
                    OpenConnection(importConnection);
                    MySqlCommand cmdSelectData = new MySqlCommand(sbImportData.ToString(), connection);
                    try
                    {
                        MySqlDataReader readerSelect = cmdSelectData.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }

                    MySqlCommand cmdGetResponse = new MySqlCommand(sbGetAddressesToResponse.ToString(), importConnection);
                    MySqlDataReader readerGetResponse = cmdGetResponse.ExecuteReader();

                    if (readerGetResponse.HasRows)
                    {
                        while (readerGetResponse.Read())
                        {
                            ImportAddressesResponse responeObject = new ImportAddressesResponse(readerGetResponse);
                            var respone = JsonConvert.SerializeObject(responeObject, Formatting.Indented);
                            exportAddressesResponse.Append(respone.ToString());
                        }
                    }
                    else
                    {
                        exportAddressesResponse.Append("No addresses imported, check if there are some addresses in import Database");
                    }
                    connection.Close();
                    importConnection.Close();
                }
                else
                {
                    return "There was a problem with coonection string , make sure that its correct and try again\nExample of correct connection string: \"server=server;user id=root;password=pa$$word;port=port;database=databaseName\"";
                }
                return exportAddressesResponse.ToString();
            }
            return "";
        }
        public string GetAddressCount(string jsonRequest)
        {
            if (jsonRequest != null)
            {
                Root getAddressCount = JsonConvert.DeserializeObject<Root>(jsonRequest);
                var ADDRESS_COUNTRY = getAddressCount.GetAddressCount.ADDRESS_COUNTRY;
                string addressCount = "";

                StringBuilder sbGetCountQuery = new StringBuilder("SELECT COUNT(*) FROM tb_evls_address");
                if (!ADDRESS_COUNTRY.Equals("") && ADDRESS_COUNTRY.Length == 2)
                {
                    sbGetCountQuery.Append(" WHERE ADDRESS_COUNTRY = '" + ADDRESS_COUNTRY + "'");
                }

                OpenConnection(connection);

                MySqlCommand cmdGetCount = new MySqlCommand(sbGetCountQuery.ToString(), connection);
                MySqlDataReader reader = cmdGetCount.ExecuteReader();

                while (reader.Read())
                {
                    addressCount = reader["COUNT(*)"].ToString();
                }

                connection.Close();

                string getAddressCountResponse = "{\n   \t\"AddressCount\":{\n  \t\t\"MESSAGE:Success\",\n \t\t\"COUNT\":" + addressCount + ",\n\t}\n}";
                return getAddressCountResponse;
            }
            return "There were some problems with your json request";
        }
        public string AddAddress(string jsonRequest)
        {
            if (jsonRequest != null)
            {
                bool isError = false;
                int requestNumber = 0;
                Root addAddressList = JsonConvert.DeserializeObject<Root>(jsonRequest);
                StringBuilder sbJsonResponse = new StringBuilder("{\n \t[\n");
                StringBuilder sbErrorMessage = new StringBuilder("\t\"AddressNr" + requestNumber + "\":\n \t\t{\n \t\t\t\"STATUS\":\"ERROR\",\n");

                //==========================================CHECK MANDATORY FIELDS========================================================
                foreach (var item in addAddressList.AddAddress)
                {
                    requestNumber++;
                    isError = false;
                    if (item.ADDRESS_REFERENCE_NR.Equals("") || item.ADDRESS_NAME1.Equals(""))
                    {
                        isError = true;
                        sbErrorMessage.Append("\t\t\t\"MESSAGE\":\"Missing parameter \"ADDRESS_REFERENCE_NR\" or \"ADDRESS_NAME1\",\n");
                    }

                    if (!item.ADDRESS_COUNTRY.Equals(""))
                    {
                        try
                        {
                            RegionInfo info = new RegionInfo(item.ADDRESS_COUNTRY);
                        }
                        catch (Exception ex)
                        {
                            return ex.Message + "\n[ADDRESS_COUNTRY] code is not correct";
                        }

                        if (!item.ADDRESS_COUNTRY.Equals("IE"))
                        {
                            if (item.ADDRESS_ZIP.Equals(""))
                            {
                                isError = true;
                                sbErrorMessage.Append("\t\t\t\"MESSAGE\":\"Missing parameter \"ADDRESS_ZIP\",\n");
                            }
                        }
                    }
                    else
                    {
                        isError = true;
                        sbErrorMessage.Append("\t\t\t\"MESSAGE\":\"Missing parameter \"ADDRESS_COUNTRY\",\n");
                    }

                    //SPRAWDZENIE CZY TAKI ADRES JUZ ISTNIEJE
                    OpenConnection(connection);
                    string isAddressAlreadyInDatabase = "";
                    MySqlCommand cmdCompareAddressess = new MySqlCommand(queryHelper.CompareAddressesQueryCreator(item), connection);
                    MySqlDataReader readerCompare = cmdCompareAddressess.ExecuteReader();
                    
                    while (readerCompare.Read())
                    {
                        isAddressAlreadyInDatabase = readerCompare["COUNT(*)"].ToString();
                    }

                    connection.Close();

                    if (!isAddressAlreadyInDatabase.Equals("0") && isAddressAlreadyInDatabase != null)
                    {
                        isError = true;
                        sbErrorMessage.Append("\t\t\t\"MESSAGE\":\"There is already exacly the same address in your database\",\n");
                    }

                    //Jeśli nie wykryto błednych wpisów
                    if (!isError)
                    {
                        OpenConnection(connection);
                        MySqlCommand cmdInsertData = new MySqlCommand(queryHelper.InsertDataQueryCreator(item), connection);
                        MySqlDataReader readerInsert = cmdInsertData.ExecuteReader();
                        connection.Close();

                        AddAddressResponse createResponse = new AddAddressResponse(item);
                        var respone = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        sbJsonResponse.Append(respone.ToString());
                    }
                    else
                    {
                        sbErrorMessage.Append("\t\t}\n");
                        sbJsonResponse.Append(sbErrorMessage.ToString());
                        sbErrorMessage = new StringBuilder("\t\"AddressNr" + requestNumber + "\":\n \t\t{\n \t\t\t\"STATUS\":\"ERROR\",\n");
                    }
                }

                return sbJsonResponse.ToString() + "\t]\n}";
            }
            return "There were some problems with your json request";
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Run(async (context) =>
            {
                string jsonString = new StreamReader(context.Request.Body).ReadToEnd();
                if (jsonString.Contains("AddAddress"))
                {
                    await context.Response.WriteAsync(AddAddress(jsonString));
                }
                else if (jsonString.Contains("GetAddressCount"))
                {
                    await context.Response.WriteAsync(GetAddressCount(jsonString));
                }
                else if (jsonString.Contains("GetAddresses") || jsonString.Contains("GetAddressesCSV"))
                {
                    await context.Response.WriteAsync(GetAddresses(jsonString));
                }
                else if (jsonString.Contains("ImportAddress"))
                {
                    await context.Response.WriteAsync(ImportAddresses(jsonString));
                }
            });
        }
    }
}