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
	public partial class OfficeAreaDAL
	{
        public static object Insert(OfficeArea officeArea)
		{
				string sql ="INSERT INTO OfficeArea (officeAreaId, name, superiorArea, address, phone, serviceDirector, introduction, organizationId, organizationName, remark)  output inserted.id VALUES (@officeAreaId, @name, @superiorArea, @address, @phone, @serviceDirector, @introduction, @organizationId, @organizationName, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@officeAreaId", ToDBValue(officeArea.OfficeAreaId)),
						new SqlParameter("@name", ToDBValue(officeArea.Name)),
						new SqlParameter("@superiorArea", ToDBValue(officeArea.SuperiorArea)),
						new SqlParameter("@address", ToDBValue(officeArea.Address)),
						new SqlParameter("@phone", ToDBValue(officeArea.Phone)),
						new SqlParameter("@serviceDirector", ToDBValue(officeArea.ServiceDirector)),
						new SqlParameter("@introduction", ToDBValue(officeArea.Introduction)),
						new SqlParameter("@organizationId", ToDBValue(officeArea.OrganizationId)),
						new SqlParameter("@organizationName", ToDBValue(officeArea.OrganizationName)),
						new SqlParameter("@remark", ToDBValue(officeArea.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM OfficeArea WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(OfficeArea officeArea)
        {
            string sql =
                @"UPDATE OfficeArea SET  officeAreaId = @officeAreaId 
                , name = @name 
                , superiorArea = @superiorArea 
                , address = @address 
                , phone = @phone 
                , serviceDirector = @serviceDirector 
                , introduction = @introduction 
                , organizationId = @organizationId 
                , organizationName = @organizationName 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", officeArea.Id)
					,new SqlParameter("@officeAreaId", ToDBValue(officeArea.OfficeAreaId))
					,new SqlParameter("@name", ToDBValue(officeArea.Name))
					,new SqlParameter("@superiorArea", ToDBValue(officeArea.SuperiorArea))
					,new SqlParameter("@address", ToDBValue(officeArea.Address))
					,new SqlParameter("@phone", ToDBValue(officeArea.Phone))
					,new SqlParameter("@serviceDirector", ToDBValue(officeArea.ServiceDirector))
					,new SqlParameter("@introduction", ToDBValue(officeArea.Introduction))
					,new SqlParameter("@organizationId", ToDBValue(officeArea.OrganizationId))
					,new SqlParameter("@organizationName", ToDBValue(officeArea.OrganizationName))
					,new SqlParameter("@remark", ToDBValue(officeArea.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static OfficeArea GetById(int id)
        {
            string sql = "SELECT * FROM OfficeArea WHERE Id = @Id";
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
		
		public static OfficeArea ToModel(SqlDataReader reader)
		{
			OfficeArea officeArea = new OfficeArea();

			officeArea.Id = (int)ToModelValue(reader,"id");
			officeArea.OfficeAreaId = (string)ToModelValue(reader,"officeAreaId");
			officeArea.Name = (string)ToModelValue(reader,"name");
			officeArea.SuperiorArea = (string)ToModelValue(reader,"superiorArea");
			officeArea.Address = (string)ToModelValue(reader,"address");
			officeArea.Phone = (string)ToModelValue(reader,"phone");
			officeArea.ServiceDirector = (string)ToModelValue(reader,"serviceDirector");
			officeArea.Introduction = (string)ToModelValue(reader,"introduction");
			officeArea.OrganizationId = (string)ToModelValue(reader,"organizationId");
			officeArea.OrganizationName = (string)ToModelValue(reader,"organizationName");
			officeArea.Remark = (string)ToModelValue(reader,"remark");
			return officeArea;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM OfficeArea";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<OfficeArea> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM OfficeArea ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<OfficeArea> GetAll()
		{
			string sql = "SELECT * FROM OfficeArea";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<OfficeArea> ToModels(SqlDataReader reader)
		{
			var list = new List<OfficeArea>();
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
		//------------------------------------------------------手动添加------------------------------------------------------
		public static OfficeArea GetMaxOfficeAreaId()
		{
			string sql = "select top(1)* from OfficeArea order by id desc";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
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
		public static string CreateOfficeAreaId()
		{
			OfficeArea model = GetMaxOfficeAreaId();
			StringBuilder sb = new StringBuilder();
			sb.Append("OAID");
			sb.Append(DateTime.Now.ToString("yyyyMMdd"));
			if (model == null)
			{
				sb.Append("0001");
				return sb.ToString();
			}
			else
			{
				int newid = int.Parse(model.OfficeAreaId.Substring(model.OfficeAreaId.Length - 4)) + 1;
				string maxid = newid.ToString().PadLeft(4, '0');
				sb.Append(maxid);
				return sb.ToString();
			}
		}
		public static int DeleteByOAId(string officeAreaId)
		{
			string sql = "DELETE FROM OfficeArea WHERE officeAreaId = @officeAreaId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@officeAreaId", officeAreaId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
		public static OfficeArea GetByOAId(string officeAreaId)
		{
			string sql = "SELECT * FROM OfficeArea WHERE officeAreaId = @officeAreaId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@officeAreaId", officeAreaId)))
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
