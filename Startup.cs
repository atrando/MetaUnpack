using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Text;

namespace MetaUnpackProject
{
    public class Startup
    {
        MySqlConnection connection = new MySqlConnection
        {
            ConnectionString = "server=127.0.0.1;user id=root;password=Tb@mptdda;port=3306;database=metapackproject"
        };
        public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public string GetAddresses(string jsonRequest)
        {
            if (jsonRequest != null)
            {
                Root getAddresses = JsonConvert.DeserializeObject<Root>(jsonRequest);
                StringBuilder sbGetAddressessQuery = new StringBuilder("SELECT * FROM tb_evls_address");
                var countAddresses = getAddresses.GetAddresses.COUNT;
                var country = getAddresses.GetAddresses.ADDRESS_COUNTRY;
                var city = getAddresses.GetAddresses.ADDRESS_CITY;

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

                OpenConnection();
                string result = "";
                MySqlCommand cmdGetAddress = new MySqlCommand(sbGetAddressessQuery.ToString(), connection);
                MySqlDataReader readerGetAddress = cmdGetAddress.ExecuteReader();
             
                while (readerGetAddress.Read())
                {
                    result = readerGetAddress[0].ToString();
                }

                connection.Close();
                return result;
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

                //Connect to Database and execute command
                OpenConnection();

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
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }

            return "There were some problems with your json request";
        }
        public string CompareAddressesQueryCreator(AddAddress item)
        {
            StringBuilder sbCompareAddresses = new StringBuilder("SELECT COUNT(*) FROM tb_evls_address WHERE ");
            sbCompareAddresses.Append("ADDRESS_REFERENCE_NR = '" + item.ADDRESS_REFERENCE_NR + "' AND ");
            sbCompareAddresses.Append("ADDRESS_TITLE = '" + item.ADDRESS_TITLE + "' AND ");
            sbCompareAddresses.Append("ADDRESS_NAME1 = '" + item.ADDRESS_NAME1 + "' AND ");
            sbCompareAddresses.Append("ADDRESS_NAME2 = '" + item.ADDRESS_NAME2 + "' AND ");
            sbCompareAddresses.Append("ADDRESS_NAME3 = '" + item.ADDRESS_NAME3 + "' AND ");
            sbCompareAddresses.Append("ADDRESS_ZIP = '" + item.ADDRESS_ZIP + "' AND ");
            sbCompareAddresses.Append("ADDRESS_CITY = '" + item.ADDRESS_CITY + "' AND ");
            sbCompareAddresses.Append("ADDRESS_STREET = '" + item.ADDRESS_STREET + "' AND ");
            sbCompareAddresses.Append("ADDRESS_HOME_NO = '" + item.ADDRESS_HOME_NO + "' AND ");
            sbCompareAddresses.Append("ADDRESS_HOME_EXT = '" + item.ADDRESS_HOME_EXT + "' AND ");
            sbCompareAddresses.Append("ADDRESS_DISTRICT = '" + item.ADDRESS_DISTRICT + "' AND ");
            sbCompareAddresses.Append("ADDRESS_TEL1 = '" + item.ADDRESS_TEL1 + "' AND ");
            sbCompareAddresses.Append("ADDRESS_MOBILE = '" + item.ADDRESS_MOBILE + "' AND ");
            sbCompareAddresses.Append("ADDRESS_EMAIL = '" + item.ADDRESS_EMAIL + "' AND ");
            sbCompareAddresses.Append("ADDRESS_REMARK = '" + item.ADDRESS_REMARK + "' AND ");
            sbCompareAddresses.Append("ADDRESS_PROVINCE = '" + item.ADDRESS_PROVINCE + "' AND ");
            sbCompareAddresses.Append("ADDRESS_BUILDING = '" + item.ADDRESS_BUILDING + "' AND ");
            sbCompareAddresses.Append("ADDRESS_FLOOR = '" + item.ADDRESS_FLOOR + "' AND ");
            sbCompareAddresses.Append("ADDRESS_AREA = '" + item.ADDRESS_AREA + "' AND ");
            sbCompareAddresses.Append("ADDRESS_DOOR_CODE = '" + item.ADDRESS_DOOR_CODE + "' AND ");
            sbCompareAddresses.Append("ADDRESS_DEPARTMENT = '" + item.ADDRESS_DEPARTMENT + "' AND ");
            sbCompareAddresses.Append("ADDRESS_COMPANY_NAME = '" + item.ADDRESS_COMPANY_NAME + "' AND ");
            sbCompareAddresses.Append("ADDRESS_COUNTRY = '" + item.ADDRESS_COUNTRY + "'");

            return sbCompareAddresses.ToString();
        }

        public string InsertDataQueryCreator(AddAddress item)
        {

            string[] tableColumns = {"ADDRESS_REFERENCE_NR", "ADDRESS_TITLE", "ADDRESS_NAME1", "ADDRESS_NAME2", "ADDRESS_NAME3", "ADDRESS_ZIP", "ADDRESS_CITY",
            "ADDRESS_STREET", "ADDRESS_HOME_NO", "ADDRESS_HOME_EXT", "ADDRESS_DISTRICT", "ADDRESS_TEL1", "ADDRESS_MOBILE", "ADDRESS_EMAIL", "ADDRESS_REMARK",
            "ADDRESS_PROVINCE", "ADDRESS_BUILDING", "ADDRESS_FLOOR", "ADDRESS_AREA", "ADDRESS_DOOR_CODE", "ADDRESS_DEPARTMENT", "ADDRESS_COMPANY_NAME", "ADDRESS_COUNTRY"};

            StringBuilder sbAddAddressessQuery = new StringBuilder("INSERT INTO tb_evls_address (");
            foreach (string column in tableColumns)
            {
                if (!column.Equals("ADDRESS_COUNTRY"))
                {
                    sbAddAddressessQuery.Append(column + ",\n ");
                }
                else
                    sbAddAddressessQuery.Append(column + ") ");
            }
            sbAddAddressessQuery.Append("VALUES (");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_REFERENCE_NR + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_TITLE + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_NAME1 + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_NAME2 + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_NAME3 + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_ZIP + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_CITY + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_STREET + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_HOME_NO + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_HOME_EXT + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_DISTRICT + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_TEL1 + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_MOBILE + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_EMAIL + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_REMARK + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_PROVINCE + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_BUILDING + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_FLOOR + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_AREA + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_DOOR_CODE + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_DEPARTMENT + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_COMPANY_NAME + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_COUNTRY + "')");

            return sbAddAddressessQuery.ToString();
        }
        public string AddAddress(string jsonRequest)
        {
            if (jsonRequest != null)
            {
                Root addAddressList = JsonConvert.DeserializeObject<Root>(jsonRequest);
                StringBuilder sbJsonResponse = new StringBuilder("{\n \t[\n");
                int requestNumber = 0;
                StringBuilder sbErrorMessage = new StringBuilder("\t\"AddressNr"+requestNumber+"\":\n \t\t{\n \t\t\t\"STATUS\":\"ERROR\",\n");
                bool isError = false;
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
                    OpenConnection();
                    string isAddressAlreadyInDatabase = "";
                    MySqlCommand cmdCompareAddressess = new MySqlCommand(CompareAddressesQueryCreator(item), connection);
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
                        OpenConnection();
                        MySqlCommand cmdInsertData = new MySqlCommand(InsertDataQueryCreator(item), connection);
                        MySqlDataReader readerInsert = cmdInsertData.ExecuteReader();
                        connection.Close();

                        //ZMODYFIKOWAC RESPONSE O DODATKOWE POLA STATUS I MESSAGES
                        AddAddressResponse createResponse = new AddAddressResponse(item);
                        var respone = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        sbJsonResponse.Append(respone.ToString());
                    }
                    else
                    {
                        sbErrorMessage.Append("\t\t}\n");
                        sbJsonResponse.Append(sbErrorMessage.ToString());
                        sbErrorMessage = new StringBuilder("\t\"AddressNr"+requestNumber+"\":\n \t\t{\n \t\t\t\"STATUS\":\"ERROR\",\n");
                    }
                }

                if (connection != null && connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }

                return sbJsonResponse.ToString()+"\t]\n}";
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
                else if (jsonString.Contains("GetAddresses"))
                {
                    await context.Response.WriteAsync(GetAddresses(jsonString));
                }

            });
        }
    }
}
