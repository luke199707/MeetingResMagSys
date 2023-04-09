//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.DAL
{
	public partial class DataDictionaryDAL
	{
        public static object Insert(DataDictionary dataDictionary)
		{
				string sql ="INSERT INTO DataDictionary (parentId, name, code, remark)  output inserted.id VALUES (@parentId, @name, @code, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@parentId", ToDBValue(dataDictionary.ParentId)),
						new SqlParameter("@name", ToDBValue(dataDictionary.Name)),
						new SqlParameter("@code", ToDBValue(dataDictionary.Code)),
						new SqlParameter("@remark", ToDBValue(dataDictionary.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM DataDictionary WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(DataDictionary dataDictionary)
        {
            string sql =
                @"UPDATE DataDictionary SET  parentId = @parentId 
                , name = @name 
                , code = @code 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", dataDictionary.Id)
					,new SqlParameter("@parentId", ToDBValue(dataDictionary.ParentId))
					,new SqlParameter("@name", ToDBValue(dataDictionary.Name))
					,new SqlParameter("@code", ToDBValue(dataDictionary.Code))
					,new SqlParameter("@remark", ToDBValue(dataDictionary.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static DataDictionary GetById(int id)
        {
            string sql = "SELECT * FROM DataDictionary WHERE Id = @Id";
            using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
       		}
        }
		
		public static DataDictionary ToModel(SqlDataReader reader)
		{
			DataDictionary dataDictionary = new DataDictionary();

			dataDictionary.Id = (int)ToModelValue(reader,"id");
			dataDictionary.ParentId = (string)ToModelValue(reader,"parentId");
			dataDictionary.Name = (string)ToModelValue(reader,"name");
			dataDictionary.Code = (string)ToModelValue(reader,"code");
			dataDictionary.Remark = (string)ToModelValue(reader,"remark");
			return dataDictionary;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM DataDictionary";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<DataDictionary> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM DataDictionary ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<DataDictionary> GetAll()
		{
			string sql = "SELECT * FROM DataDictionary";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<DataDictionary> ToModels(SqlDataReader reader)
		{
			var list = new List<DataDictionary>();
			while(reader.Read())
			{
				list.Add(ToModel(reader));
			}	
			return list;
		}		
		
		protected static object ToDBValue(object value)
		{
			if(value==null)
			{
				return DBNull.Value;
			}
			else
			{
				return value;
			}
		}
		
		protected static object ToModelValue(SqlDataReader reader,string columnName)
		{
			if(reader.IsDBNull(reader.GetOrdinal(columnName)))
			{
				return null;
			}
			else
			{
				return reader[columnName];
			}
		}
		//----------------------------------------------非自动生成-----------------------------------------------------------
		public static List<DataDictionary> GetListById(int id)
		{
			string sql = "SELECT * FROM DataDictionary where id=@id";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@id", id)))
			{
				return ToModels(reader);
			}
		}
		public static List<DataDictionary> GetListByParentId(string parentId)
		{
			string sql = "SELECT * FROM DataDictionary where parentId=@parentId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@parentId", parentId)))
			{
				return ToModels(reader);
			}
		}

		public static int DeleteByCode(string code)
		{
			string likecode = code + "%";
			string sql = "DELETE FROM DataDictionary WHERE code like @code";
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@code", likecode));
		}
		public static DataDictionary GetByParentId(string parentId)
		{
			string sql = "SELECT top(1)* FROM DataDictionary WHERE parentId = @parentId order by code desc";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@parentId", parentId)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
			}
		}
		public static DataDictionary GetByCode(string code)
		{
			string likecode = code + "%";
			string sql = "SELECT top(1)* FROM DataDictionary where code like @code order by code desc";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@code", likecode)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
			}
		}
		public static DataDictionary GetByName(string name)
		{
			string sql = "SELECT * FROM DataDictionary WHERE name = @name  and parentId='1'";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@name", name)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
			}
		}
	}
}
