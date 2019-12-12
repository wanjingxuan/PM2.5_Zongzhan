﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using ADOX;
using System.Data;

namespace PM2._5_Project
{
	class ConnectAccess
	{

		/// <summary>
		/// 数据库操作
		/// </summary>
		/// <param name="data">获取的数据</param>
		/// <param name="tableName">更新的表名</param>
		/// <param name="action">数据库操作动作</param>
		public bool SaveData(string[] data, string tableName, string action, string dataFileAddress)
		{

			int i = 0;//如果i大于0 bool返回值为true，读取数据库成功

			string sql = null;

			try
			{

				OleDbConnection mycon = new OleDbConnection(dataFileAddress);
				OleDbDataReader myReader = null;
				OleDbCommand mycom = null;

				mycon.Open();


				switch (action)
				{

					case "add":

						sql = "INSERT INTO " + tableName + " VALUES ('" + data[0] + "', '" + data[1] + "', '" + data[2] + "', '" + data[3] + "', '" + data[4] + "', '" + data[5] + "', '" + data[6] + "', '" + data[7] + "')";

				    break;
				
				}
				data = null;
				mycom = new OleDbCommand(sql, mycon);
                i = mycom.ExecuteNonQuery();
				if (i > 0)
				{
					mycon.Close();
					return true;

				}
				mycon.Close();
			}
			catch (Exception e)
			{



			}
			return false;
		}

		/// <summary>
		/// 获取数据填充到DataTable
		/// </summary>
		/// <param name="sqlCommand"></param>
		/// <returns></returns>
		public DataTable GetData(string sqlCommand, string address)
		{


			try
			{
				OleDbConnection mycon = mycon = new OleDbConnection(address);
				OleDbCommand myCommand = new OleDbCommand(sqlCommand, mycon);
				OleDbDataAdapter myDataAdap = new OleDbDataAdapter(myCommand);
				////

				DataTable table = new DataTable();
				table.Locale = System.Globalization.CultureInfo.InvariantCulture;
				myDataAdap.Fill(table);
				mycon.Close();
				return table;
			}
			catch (Exception ex)
			{

				return null;
			}

		}
		/// <summary>
		/// 16进制字符串转换成16进制byte
		/// </summary>
		/// <param name="arr">16进制byte</param>
		/// <param name="brr">16进制字符串数组</param>
		/// <returns></returns>
		public byte[] ToByte( string arrAy)
		{
			
			string[] brr = arrAy.Split(' ');
			byte[] arr = new byte[brr.Length];
			for (int i = 0; i < brr.Length; i++)
			{


				arr[i] = Convert.ToByte(("0x" + brr[i]), 16); //截取字符串，拼接成16进制，转换成二进制数                                                                                                             // 字，存储到byte数组中




			}




			return arr;

		}
		//CRC 校验
		#region
        private static readonly byte[] _auchCRCHi = new byte[]//crc高位表
        {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
        };
        private static readonly byte[] _auchCRCLo = new byte[]//crc低位表
        {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 
            0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 
            0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 
            0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4, 
            0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3, 
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 
            0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 
            0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 
            0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED, 
            0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26, 
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 
            0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 
            0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 
            0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 
            0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5, 
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 
            0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 
            0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 
            0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B, 
            0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C, 
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 
            0x43, 0x83, 0x41, 0x81, 0x80, 0x40
        };
        #endregion
        public static ushort CRC16(Byte[] buffer, int Sset, int Eset)
        {
            byte crcHi = 0xff;  // 高位初始化
            byte crcLo = 0xff;  // 低位初始化
            for (int i = Sset; i <= Eset; i++)
            {
                int crcIndex = crcHi ^ buffer[i]; //查找crc表值
                crcHi = (byte)(crcLo ^ _auchCRCHi[crcIndex]);
                crcLo = _auchCRCLo[crcIndex];
            }
            return (ushort)(crcHi << 8 | crcLo);
        }

		public uint crc16_modbus(byte[] modbusdata, uint length)
        {
            uint i, j;
            uint crc16 = 0xffff;
            for ( i = 0; i < length; i++)
            {
                crc16 ^= modbusdata[i];
                for (j = 0; j < 8; j++)
                {
                    if ((crc16 & 0x01) == 1)
                    {
                        crc16 = (crc16 >> 1) ^ 0xA001;
                    }
                    else
                    {
                        crc16 = crc16 >> 1;
                    }
                }
            }
            return crc16;
        }

		/// <summary>
		/// 创建access数据库
		/// </summary>
		/// <param name="filePath">数据库文件的全路径，如 D:\\NewDb.mdb</param>a
		public bool CreateAccessDb(string filePath)
		{
			ADOX.Catalog catalog = new Catalog();
			if (!File.Exists(filePath))
			{
				try
				{
					catalog.Create("provider=microsoft.ace.oledb.12.0;Data Source=" + filePath + ";Jet OLEDB:Engine Type=5");

				}
				catch (System.Exception ex)
				{
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// 创建实验数据表
		/// </summary>
		/// <param name="testName">实验名称</param>
		/// <param name="testType">实验类型</param>
		public void CreateAccessDataBase(string testName)
		{
			//int effcyc = int.Parse(EFFTableSet.SYS520CycleNum); 
			// 创建存储数据文件的文件夹
			if (!Directory.Exists(System.IO.Directory.GetCurrentDirectory() + "\\DATABASE"))
			{
				Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\DATABASE");
			}

			//  创建实验数据库
			CreateAccessDb(System.IO.Directory.GetCurrentDirectory() + "\\DATABASE" + "\\" + testName + ".mdb");
			//  存储实验数据的文件地址
			GlobalParameter.testFileAddress = "provider=microsoft.ace.oledb.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + "\\DATABASE" + "\\"  + testName + ".mdb";
			//  创建实验数据库表单DP,DHP,EFF 中各个实验需要的数据表

			ADOX.Column[] EFFDataTable = {
                                new ADOX.Column(){Name="Time",Type=DataTypeEnum.adWChar,DefinedSize=50},
                                new ADOX.Column(){Name="MixT",Type=DataTypeEnum.adWChar,DefinedSize=50},
                                new ADOX.Column(){Name="AirT",Type=DataTypeEnum.adWChar,DefinedSize=50},
                                new ADOX.Column(){Name="PB",Type=DataTypeEnum.adWChar,DefinedSize=50},
								  new ADOX.Column(){Name="Ean581F",Type=DataTypeEnum.adWChar,DefinedSize=50},
                                new ADOX.Column(){Name="DiluteAirF",Type=DataTypeEnum.adWChar,DefinedSize=50},
                                new ADOX.Column(){Name="StandardPm",Type=DataTypeEnum.adWChar,DefinedSize=50},
								  new ADOX.Column(){Name="DetectionPm",Type=DataTypeEnum.adWChar,DefinedSize=50},
                                };
		   CreateAccessTable(System.IO.Directory.GetCurrentDirectory() + "\\DATABASE" + "\\"  + testName + ".mdb", "Data", EFFDataTable);

			}

		/// <summary>
        /// 在access数据库中创建表
        /// </summary>
        /// <param name="filePath">数据库表文件全路径如D:\\NewDb.mdb 没有则创建 </param> 
        /// <param name="tableName">表名</param>
        /// <param name="colums">ADOX.Column对象数组</param>
        public static void CreateAccessTable(string filePath, string tableName, params ADOX.Column[] colums)
        {
            ADOX.Catalog catalog = new Catalog();
            //数据库文件不存在则创建
            if (!File.Exists(filePath))
            {
                try
                {
                    catalog.Create("Provider=Microsoft.ACE.OLEDB.12.0;;Data Source=" + filePath + ";Jet OLEDB:Engine Type=5");
                }
                catch (System.Exception ex)
                {

                }
            }
            ADODB.Connection cn = new ADODB.Connection();
            cn.Open("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath, null, null, -1);
            catalog.ActiveConnection = cn;
            ADOX.Table table = new ADOX.Table();
            table.Name = tableName;
            foreach (var column in colums)
            {
                table.Columns.Append(column);
            }
            // column.ParentCatalog = catalog; 
            //column.Properties["AutoIncrement"].Value = true; //设置自动增长
            //table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null); //定义主键
            catalog.Tables.Append(table);
            cn.Close();
        }

		}

	}
