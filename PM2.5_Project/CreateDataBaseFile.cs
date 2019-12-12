using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PM2._5_Project
{
	class CreateDataBaseFile
	{
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
			GlobalParameter.testFileAddress = "provider=microsoft.ace.oledb.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + "\\DATABASE" + "\\" + testName + ".mdb";
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
			CreateAccessTable(System.IO.Directory.GetCurrentDirectory() + "\\DATABASE" + "\\" + testName + ".mdb", "Data", EFFDataTable);

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
			GlobalParameter.testFileAddress = "provider=microsoft.ace.oledb.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + "\\DATABASE" + "\\" + testName + ".mdb";
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
			CreateAccessTable(System.IO.Directory.GetCurrentDirectory() + "\\DATABASE" + "\\" + testName + ".mdb", "Data", EFFDataTable);

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
