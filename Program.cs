using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

internal class Program
{
	private static byte[] l = new byte[10240];

	private static Random random = new Random();

	private static long id = random.Next();

	private static void Main(string[] args)
	{
		id = random.Next();
		ServicePointManager.Expect100Continue = true;
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		Console.WriteLine("此工具由木子李提供，使用了3.5GPT接口实现，可以联合上下文提问！");
		Console.WriteLine("联系方式：wx LKZ4251 ");
		Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine("*              *      *    ************\r\n*              *    *                *     \r\n*              *  *                *      \r\n*              **                *            \r\n*              *  *            *             \r\n*              *    *        *             \r\n***********    *      *    ************ ");
		Console.ForegroundColor = ConsoleColor.White;
		while (true)
		{
			Console.WriteLine("请输入需要请问的问题:");
			string content = Console.ReadLine();
			Console.WriteLine("下面是答案");
			Console.ForegroundColor = ConsoleColor.Cyan;
			HTTPRequestGPT1(content);
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();
		}
	}

	private static void HTTPRequestGPT1(string content)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri("https://api.aichatos.cloud/api/generateStream"));
		httpWebRequest.Method = "POST";
		httpWebRequest.Accept = "application/json, text/plain, */*";
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate, br");
		httpWebRequest.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
		httpWebRequest.Headers.Add("Origin", "https://dev.yqcloud.top");
		httpWebRequest.Headers.Add("Aec-ch-ua", "\"Chromium\";v=\"112\", \"Google Chrome\";v=\"112\", \"Not: A - Brand\";v=\"99\"");
		httpWebRequest.Headers.Add("Sec-Ch-Ua", "\"Not.A/Brand\";v=\"8\", \"Chromium\";v=\"114\", \"Google Chrome\";v=\"114\"");
		httpWebRequest.Headers.Add("Sec-Ch-Ua-Mobile", "?0");
		httpWebRequest.Headers.Add("Sec-ch-ua-platform", "\"Windows\"");
		httpWebRequest.Headers.Add("Sec-Fetch-Dest", "empty");
		httpWebRequest.Headers.Add("Sec-Fetch-Mode", "cors");
		httpWebRequest.Headers.Add("Sec-Fetch-Site", "cross-site");
		httpWebRequest.Referer = "https://dev.yqcloud.top/";
		httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36";
		httpWebRequest.Host = "api.aichatos.cloud";
		Stream requestStream = httpWebRequest.GetRequestStream();
		string text = " {\"prompt\":\"\",\"userId\":\"#/chat/" + id + "\",\"network\":true,\"system\":\"\",\"withoutContext\":false,\"stream\":false}";
		JObject val = JObject.Parse(text);
		val["prompt"] = JToken.op_Implicit(content);
		text = ((object)val).ToString();
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		requestStream.Write(bytes, 0, bytes.Length);
		requestStream.Dispose();
		WebResponse response = httpWebRequest.GetResponse();
		Stream responseStream = response.GetResponseStream();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		do
		{
			num2 = responseStream.Read(l, num, 10240 - num);
			num += num2;
			if (num - num3 >= 3)
			{
				Console.Write(Encoding.UTF8.GetString(l, num3, num - num3));
				num3 += num - num3;
			}
		}
		while (num2 != 0);
	}
}
