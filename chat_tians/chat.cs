using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace chat_tians
{
    public class chat
    {
        ClientChat clientChat;
        string apiUrl = "http://43.153.60.218:9099/weatherforecast/";
        //string apiUrl = "http://localhost:9099/weatherforecast/";
        public chat()
        {
            clientChat = new ClientChat();
            clientChat.IP = GetLocalIpAddress();
            clientChat.Token = "";
        }

        public string BeginChat(string message)
        {
            string result = "";
            clientChat.Time = DateTime.Now;
            clientChat.Content = message;

            using (HttpClient client = new HttpClient())
            {

                // 发送POST请求
                HttpResponseMessage response = client.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(clientChat), Encoding.UTF8, "application/json")).Result;

                // 处理响应
                string responseBody = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    // 解析响应
                    //Console.WriteLine("API Response:");
                    //Console.WriteLine(responseBody);
                    result = responseBody;
                }
                else
                {
                    // 处理错误
                    //Console.WriteLine("API Request failed:");
                    //Console.WriteLine(responseBody);
                    result = "API Request failed";
                }

                response.Dispose();
            }

            return result;
        }




        public string GetLocalIpAddress()
        {
            string ipAddress = string.Empty;

            // 获取所有网络接口
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                // 排除非活动接口和回环接口
                if (networkInterface.OperationalStatus != OperationalStatus.Up ||
                    networkInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }

                // 获取 IP 地址信息
                IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                foreach (UnicastIPAddressInformation ipInfo in ipProperties.UnicastAddresses)
                {
                    // 仅获取 IPv4 地址
                    if (ipInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipAddress = ipInfo.Address.ToString();
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    break;
                }
            }

            return ipAddress;
        }

    }


    public class ClientChat
    {
        public DateTime Time { get; set; }

        public string IP { get; set; }

        public string Content { get; set; }

        public string Token { get; set; }
    }
}
