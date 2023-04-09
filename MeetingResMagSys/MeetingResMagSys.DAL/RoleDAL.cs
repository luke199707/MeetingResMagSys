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
	public partial class RoleDAL
	{
        public static object Insert(Role role)
		{
				string sql ="INSERT INTO Role (roleId, roleName, organizationId, defaultRole, remark)  output inserted.id VALUES (@roleId, @roleName, @organizationId, @defaultRole, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@roleId", ToDBValue(role.RoleId)),
						new SqlParameter("@roleName", ToDBValue(role.RoleName)),
						new SqlParameter("@organizationId", ToDBValue(role.OrganizationId)),
						new SqlParameter("@defaultRole", ToDBValue(role.DefaultRole)),
						new SqlParameter("@remark", ToDBValue(role.Remark)),
					};
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM Role WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(Role role)
        {
            string sql =
                @"UPDATE Role SET  roleId = @roleId 
                , roleName = @roleName 
                , organizationId = @organizationId 
                , defaultRole = @defaultRole 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", role.Id)
					,new SqlParameter("@roleId", ToDBValue(role.RoleId))
					,new SqlParameter("@roleName", ToDBValue(role.RoleName))
					,new SqlParameter("@organizationId", ToDBValue(role.OrganizationId))
					,new SqlParameter("@defaultRole", ToDBValue(role.DefaultRole))
					,new SqlParameter("@remark", ToDBValue(role.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static Role GetById(int id)
        {
            string sql = "SELECT * FROM Role WHERE Id = @Id";
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
		
		public static Role ToModel(SqlDataReader reader)
		{
			Role role = new Role();

			role.Id = (int)ToModelValue(reader,"id");
			role.RoleId = (string)ToModelValue(reader,"roleId");
			role.RoleName = (string)ToModelValue(reader,"roleName");
			role.OrganizationId = (string)ToModelValue(reader,"organizationId");
			role.DefaultRole = (string)ToModelValue(reader,"defaultRole");
			role.Remark = (string)ToModelValue(reader,"remark");
			return role;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM Role";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<Role> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM Role ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<Role> GetAll()
		{
			string sql = "SELECT * FROM Role";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<Role> ToModels(SqlDataReader reader)
		{
			var list = new List<Role>();
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
		//---------------------------------------手动添加---------------------------------------
		public static Role GetMaxRoleId()
		{
			string sql = "select top(1)* from Role order by id desc";
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
		public static string CreateRoleId()
		{
			Role model = GetMaxRoleId();
			StringBuilder sb = new StringBuilder();
			sb.Append("R");
			sb.Append(DateTime.Now.ToString("yyyyMMdd"));
			if (model == null)
			{
				sb.Append("0001");
				return sb.ToString();
			}
			else
			{
				int newid = int.Parse(model.RoleId.Substring(model.RoleId.Length - 4)) + 1;
				string maxid = newid.ToString().PadLeft(4, '0');
				sb.Append(maxid);
				return sb.ToString();
			}
		}
		public static Role GetByRoleId(string roleId)
		{
			string sql = "SELECT * FROM Role WHERE roleId = @roleId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@roleId", roleId)))
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
		public static int DeleteByRoleId(string roleId)
		{
			string sql = "DELETE FROM Role WHERE roleId = @roleId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@roleId", roleId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
	}
}
