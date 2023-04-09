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
	public partial class AllUserDAL
	{
        public static object Insert(AllUser allUser)
		{
				string sql ="INSERT INTO AllUser (userId, name, pwd, organizationId, organizationName, departmentName, Email, phone, role, available, remark)  output inserted.id VALUES (@userId, @name, @pwd, @organizationId, @organizationName, @departmentName, @Email, @phone, @role, @available, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@userId", ToDBValue(allUser.UserId)),
						new SqlParameter("@name", ToDBValue(allUser.Name)),
						new SqlParameter("@pwd", ToDBValue(allUser.Pwd)),
						new SqlParameter("@organizationId", ToDBValue(allUser.OrganizationId)),
						new SqlParameter("@organizationName", ToDBValue(allUser.OrganizationName)),
						new SqlParameter("@departmentName", ToDBValue(allUser.DepartmentName)),
						new SqlParameter("@Email", ToDBValue(allUser.Email)),
						new SqlParameter("@phone", ToDBValue(allUser.Phone)),
						new SqlParameter("@role", ToDBValue(allUser.Role)),
						new SqlParameter("@available", ToDBValue(allUser.Available)),
						new SqlParameter("@remark", ToDBValue(allUser.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM AllUser WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
				
        public static int Update(AllUser allUser)
        {
            string sql =
                @"UPDATE AllUser SET  userId = @userId 
                , name = @name 
                , pwd = @pwd 
                , organizationId = @organizationId 
                , organizationName = @organizationName 
                , departmentName = @departmentName 
                , Email = @Email 
                , phone = @phone 
                , role = @role 
                , available = @available 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", allUser.Id)
					,new SqlParameter("@userId", ToDBValue(allUser.UserId))
					,new SqlParameter("@name", ToDBValue(allUser.Name))
					,new SqlParameter("@pwd", ToDBValue(allUser.Pwd))
					,new SqlParameter("@organizationId", ToDBValue(allUser.OrganizationId))
					,new SqlParameter("@organizationName", ToDBValue(allUser.OrganizationName))
					,new SqlParameter("@departmentName", ToDBValue(allUser.DepartmentName))
					,new SqlParameter("@Email", ToDBValue(allUser.Email))
					,new SqlParameter("@phone", ToDBValue(allUser.Phone))
					,new SqlParameter("@role", ToDBValue(allUser.Role))
					,new SqlParameter("@available", ToDBValue(allUser.Available))
					,new SqlParameter("@remark", ToDBValue(allUser.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static AllUser GetById(int id)
        {
            string sql = "SELECT * FROM AllUser WHERE Id = @Id";
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
		
		public static AllUser ToModel(SqlDataReader reader)
		{
			AllUser allUser = new AllUser();

			allUser.Id = (int)ToModelValue(reader,"id");
			allUser.UserId = (string)ToModelValue(reader,"userId");
			allUser.Name = (string)ToModelValue(reader,"name");
			allUser.Pwd = (string)ToModelValue(reader,"pwd");
			allUser.OrganizationId = (string)ToModelValue(reader,"organizationId");
			allUser.OrganizationName = (string)ToModelValue(reader,"organizationName");
			allUser.DepartmentName = (string)ToModelValue(reader,"departmentName");
			allUser.Email = (string)ToModelValue(reader,"Email");
			allUser.Phone = (string)ToModelValue(reader,"phone");
			allUser.Role = (string)ToModelValue(reader,"role");
			allUser.Available = (string)ToModelValue(reader,"available");
			allUser.Remark = (string)ToModelValue(reader,"remark");
			return allUser;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM AllUser";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<AllUser> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM AllUser ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<AllUser> GetAll()
		{
			string sql = "SELECT * FROM AllUser";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<AllUser> ToModels(SqlDataReader reader)
		{
			var list = new List<AllUser>();
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
		//-----------------------------------------以下为非自动生成-----------------------------------------
		public static AllUser GetMaxUserId()
		{
			string sql = "select top(1)* from AllUser order by id desc";
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
		public static int DeleteByUserId(string userId)
		{
			string sql = "DELETE FROM AllUser WHERE userId = @userId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@userId", userId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
		public static string CreateUserId()
		{
			AllUser model = GetMaxUserId();
			StringBuilder sb = new StringBuilder();
			sb.Append("UID");
			sb.Append(DateTime.Now.ToString("yyyyMMdd"));
			if (model == null)
			{
				sb.Append("000001");
				return sb.ToString();
			}
			else
			{
				int newid = int.Parse(model.UserId.Substring(model.UserId.Length - 6)) + 1;
				string maxid = newid.ToString().PadLeft(6, '0');
				sb.Append(maxid);
				return sb.ToString();
			}
		}
		public static AllUser GetByEmail(string email)
		{
			string sql = "SELECT * FROM AllUser WHERE Email = @email";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@email", email)))
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
		public static AllUser GetByName(string name)
		{
			string sql = "SELECT * FROM AllUser WHERE name = @name";
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
		public static AllUser GetByNamePwd(string name,string pwd)
		{
			string sql = "SELECT * FROM AllUser WHERE name = @name and pwd = @pwd";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, 
				new SqlParameter("@name", name),
				new SqlParameter("@pwd", pwd)))
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
		public static AllUser GetByUserId(string userId)
		{
			string sql = "SELECT * FROM AllUser WHERE userId = @userId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@userId", userId)))
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
