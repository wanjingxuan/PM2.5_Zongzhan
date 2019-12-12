using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PM2._5_Project
{
	class CRC
	{
		//校验CRC 16  
		#region  CRC16
		public static byte[] CRC16(byte[] data)
		{
			int len = data.Length;
			if (len > 0)
			{
				ushort crc = 0xFFFF;

				for (int i = 0; i < len; i++)
				{
					crc = (ushort)(crc ^ (data[i]));
					for (int j = 0; j < 8; j++)
					{
						crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
					}
				}
				byte hi = (byte)((crc & 0xFF00) >> 8);  //高位置
				byte lo = (byte)(crc & 0x00FF);         //低位置

				return new byte[] { hi, lo };
			}
			return new byte[] { 0, 0 };
		}

		/// <summary>
		/// 十六进制换算为十进制commonVariables.temStrsz[9].Substring(1)
		/// </summary>05 F8 13 95 27 F0 01 0B 6A 39 00 00 00 00 00 00 FF E8 00 00 00 00 00 00 00 00 00 00 00 00
		/// <param name="strColorValue"></param>
		/// <returns></returns>
		public static string GetHexadecimalValue(String strColorValue)
		{

			char[] nums = strColorValue.ToCharArray();
			int total = 0;
			try
			{
				for (int i = 0; i < nums.Length; i++)
				{
					String strNum = nums[i].ToString().ToUpper();
					switch (strNum)
					{
						case "A":
							strNum = "10";
							break;
						case "B":
							strNum = "11";
							break;
						case "C":
							strNum = "12";
							break;
						case "D":
							strNum = "13";
							break;
						case "E":
							strNum = "14";
							break;
						case "F":
							strNum = "15";
							break;
						case " ":
							strNum = "0";
							break;
						case "0":
							strNum = "0";
							break;
						default:
							break;
					}
					double power = Math.Pow(16, Convert.ToDouble(nums.Length - i - 1));
					total += Convert.ToInt32(strNum) * Convert.ToInt32(power);
				}

			}
			catch (System.Exception ex)
			{
				String strErorr = ex.ToString();
				//return "0";
			}

			if (total.ToString().Length < 4)
			{

				return "0" + total.ToString();
			}

			return total.ToString();
		}

		#endregion

		#region  ToCRC16
		public static string ToCRC16(string content)
		{
			return ToCRC16(content, Encoding.UTF8);
		}

		public static string ToCRC16(string content, bool isReverse)
		{
			return ToCRC16(content, Encoding.UTF8, isReverse);
		}

		public static string ToCRC16(string content, Encoding encoding)
		{
			return ByteToString(CRC16(encoding.GetBytes(content)), true);
		}

		public static string ToCRC16(string content, Encoding encoding, bool isReverse)
		{
			return ByteToString(CRC16(encoding.GetBytes(content)), isReverse);
		}

		public static string ToCRC16(byte[] data)
		{
			return ByteToString(CRC16(data), true);
		}

		public static string ToCRC16(byte[] data, bool isReverse)
		{
			return ByteToString(CRC16(data), isReverse);
		}
		#endregion

		#region  ToModbusCRC16
		public static string ToModbusCRC16(string s)
		{
			return ToModbusCRC16(s, true);
		}

		public static string ToModbusCRC16(string s, bool isReverse)
		{
			return ByteToString(CRC16(StringToHexByte(s)), isReverse);
		}

		public static string ToModbusCRC16(byte[] data)
		{
			return ToModbusCRC16(data, true);
		}

		public static string ToModbusCRC16(byte[] data, bool isReverse)
		{
			return ByteToString(CRC16(data), isReverse);
		}
		#endregion

		#region  ByteToString
		public static string ByteToString(byte[] arr, bool isReverse)
		{
			try
			{
				byte hi = arr[0], lo = arr[1];
				return Convert.ToString(isReverse ? hi + lo * 0x100 : hi * 0x100 + lo, 16).ToUpper().PadLeft(4, '0');
			}
			catch (Exception ex) { throw (ex); }
		}

		public static string ByteToString(byte[] arr)
		{
			try
			{
				return ByteToString(arr, true);
			}
			catch (Exception ex) { throw (ex); }
		}
		#endregion

		#region  StringToHexString
		public static string StringToHexString(string str)
		{
			StringBuilder s = new StringBuilder();
			foreach (short c in str.ToCharArray())
			{
				s.Append(c.ToString("X4"));
			}
			return s.ToString();
		}
		#endregion

		#region  StringToHexByte
		private static string ConvertChinese(string str)
		{
			StringBuilder s = new StringBuilder();
			foreach (short c in str.ToCharArray())
			{
				if (c <= 0 || c >= 127)
				{
					s.Append(c.ToString("X4"));
				}
				else
				{
					s.Append((char)c);
				}
			}
			return s.ToString();
		}

		private static string FilterChinese(string str)
		{
			StringBuilder s = new StringBuilder();
			foreach (short c in str.ToCharArray())
			{
				if (c > 0 && c < 127)
				{
					s.Append((char)c);
				}
			}
			return s.ToString();
		}

		/// <summary>
		/// 字符串转16进制字符数组
		/// </summary>
		/// <param name="hex"></param>
		/// <returns></returns>
		public static byte[] StringToHexByte(string str)
		{
			return StringToHexByte(str, false);
		}

		/// <summary>
		/// 字符串转16进制字符数组
		/// </summary>
		/// <param name="str"></param>
		/// <param name="isFilterChinese">是否过滤掉中文字符</param>
		/// <returns></returns>
		public static byte[] StringToHexByte(string str, bool isFilterChinese)
		{
			string hex = isFilterChinese ? FilterChinese(str) : ConvertChinese(str);

			//清除所有空格
			hex = hex.Replace(" ", "");
			//若字符个数为奇数，补一个0
			hex += hex.Length % 2 != 0 ? "0" : "";

			byte[] result = new byte[hex.Length / 2];
			for (int i = 0, c = result.Length; i < c; i++)
			{
				result[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
			}
			return result;
		}
		#endregion

		/// <summary> 
		/// 字节数组转16进制字符串 
		/// </summary> 
		/// <param name="bytes"></param> 
		/// <returns></returns> 
		public static string byteToHexStr(byte[] bytes)
		{
			string returnStr = "";
			if (bytes != null)
			{
				for (int i = 0; i < bytes.Length; i++)
				{
					returnStr += bytes[i].ToString("X2");
				}
			}
			return returnStr;
		}


        public static void write_Txt(object msg)
        {

            using (FileStream fs = new FileStream(System.Environment.CurrentDirectory + @"\Console.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine("{0}", msg.ToString() + "-------" + DateTime.Now, DateTime.Now);
                    sw.Flush();
                }
            }


        }
    }
}