using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Common
{
    public class AppConfiguration : IDisposable, IAppConfiguration
    {

        private IDictionary<string, object> AppConfigItems { get; set; }

        private IList<AppConfigurationItem> appConfigurationItems;
        public AppConfiguration()
        {
            AppConfigItems = new Dictionary<string, object>();
            //loadAppConfiguration();
        }

        public AppConfiguration(string assemblyName)
        {
            AppConfigItems = new Dictionary<string, object>();
            appConfigurationItems = new List<AppConfigurationItem>();
            loadAppConfiguration(assemblyName);
        }


        public AppConfigurationItem this[string configurationItemName]
        {
            get
            {
                var value = appConfigurationItems.Where(x => x.Name == configurationItemName).FirstOrDefault();
                return value;
            }
        }

        public IEnumerable<AppConfigurationItem> Items
        {
            get
            {
                return appConfigurationItems;
            }
        }

       



        private dynamic loadAppConfiguration(string assemblyName)
        {
            JObject returnValue = null;

            using (HttpClient client = new HttpClient())
            {
                //var url = "http://localhost/ApplicationConfiguration.WebAPI/api/ApplicationConfiguration/GetApplicationConfigurationByAssemblyName?assemblyName=" + assemblyName;

                var url = "http://192.168.1.149:2112/api/ApplicationConfiguration/GetApplicationConfigurationByAssemblyName?assemblyName=" + assemblyName;

                //var response = client.GetAsync(url).Result;

                using (var response = client.GetAsync(url))
                {
                    response.Wait();

                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var responseContent = result.Content;

                        string json = responseContent.ReadAsStringAsync().Result;

                        returnValue = JObject.Parse(json);

                        returnValue["application"]["applicationConfigurationItems"].ToList().ForEach(x =>
                        {
                            AppConfigItems.Add(x["configurationItem"]["name"].ToString(), (string)x["value"]);
                            appConfigurationItems.Add(new AppConfigurationItem() { Name = x["configurationItem"]["name"].ToString(), Value = Decrypt(x["value"].ToString()) });
                        });

                        returnValue["application"]["applicationCustomConfigurationItems"].ToList().ForEach(x =>
                        {
                            AppConfigItems.Add(x["name"].ToString(), (string)x["value"]);
                            appConfigurationItems.Add(new AppConfigurationItem() { Name = x["name"].ToString(), Value = Decrypt(x["value"].ToString()) });
                        });
                    }
                }
            }

            return returnValue;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


        public void Dispose()
        {
            appConfigurationItems = null;
            AppConfigItems = null;
        }
    }
}
