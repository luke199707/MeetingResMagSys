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
	public partial class RoleRightDAL
	{
        public static object Insert(RoleRight roleRight)
		{
				string sql ="INSERT INTO RoleRight (roleId, roleName, rightCode, organizationId, organizationName, remark)  output inserted.id VALUES (@roleId, @roleName, @rightCode, @organizationId, @organizationName, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@roleId", ToDBValue(roleRight.RoleId)),
						new SqlParameter("@roleName", ToDBValue(roleRight.RoleName)),
						new SqlParameter("@rightCode", ToDBValue(roleRight.RightCode)),
						new SqlParameter("@organizationId", ToDBValue(roleRight.OrganizationId)),
						new SqlParameter("@organizationName", ToDBValue(roleRight.OrganizationName)),
						new SqlParameter("@remark", ToDBValue(roleRight.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM RoleRight WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(RoleRight roleRight)
        {
            string sql =
                @"UPDATE RoleRight SET  roleId = @roleId 
                , roleName = @roleName 
                , rightCode = @rightCode 
                , organizationId = @organizationId 
                , organizationName = @organizationName 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", roleRight.Id)
					,new SqlParameter("@roleId", ToDBValue(roleRight.RoleId))
					,new SqlParameter("@roleName", ToDBValue(roleRight.RoleName))
					,new SqlParameter("@rightCode", ToDBValue(roleRight.RightCode))
					,new SqlParameter("@organizationId", ToDBValue(roleRight.OrganizationId))
					,new SqlParameter("@organizationName", ToDBValue(roleRight.OrganizationName))
					,new SqlParameter("@remark", ToDBValue(roleRight.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static RoleRight GetById(int id)
        {
            string sql = "SELECT * FROM RoleRight WHERE Id = @Id";
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
		
		public static RoleRight ToModel(SqlDataReader reader)
		{
			RoleRight roleRight = new RoleRight();

			roleRight.Id = (int)ToModelValue(reader,"id");
			roleRight.RoleId = (string)ToModelValue(reader,"roleId");
			roleRight.RoleName = (string)ToModelValue(reader,"roleName");
			roleRight.RightCode = (string)ToModelValue(reader,"rightCode");
			roleRight.OrganizationId = (string)ToModelValue(reader,"organizationId");
			roleRight.OrganizationName = (string)ToModelValue(reader,"organizationName");
			roleRight.Remark = (string)ToModelValue(reader,"remark");
			return roleRight;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM RoleRight";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<RoleRight> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM RoleRight ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<RoleRight> GetAll()
		{
			string sql = "SELECT * FROM RoleRight";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<RoleRight> ToModels(SqlDataReader reader)
		{
			var list = new List<RoleRight>();
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
		//---------------------------------------------------------添加的---------------------------------------------------------
		public static List<RoleRight> GetByRoleId(string roleId)
		{
			string sql = "SELECT * FROM RoleRight where roleId=@roleId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@roleId", roleId)))
			{
				return ToModels(reader);
			}
		}
		public static List<RoleRight> GetChildNode(string currentId, string roleName,string orgId)
		{
			string str = "0" + currentId + "0%";
			string sql = "select * from RoleRight where rightCode like @rightCode and roleName=@roleName and organizationId=@organizationId order by rightCode asc";
			SqlParameter[] sp = { new SqlParameter("@rightCode", str), new SqlParameter("@roleName", roleName),new SqlParameter("@organizationId", orgId) };
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, sp))
			{
				return ToModels(reader);
			}
		}
		public static List<RoleRight> GetChildNode(string currentId, string roleName)
		{
			string str = "0" + currentId + "0%";
			string sql = "select * from RoleRight where rightCode like @rightCode and roleName=@roleName order by rightCode asc";
			SqlParameter[] sp = { new SqlParameter("@rightCode", str), new SqlParameter("@roleName", roleName)};
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, sp))
			{
				return ToModels(reader);
			}
		}
		public static int DeleteByRoleId(string roleId)
		{
			string sql = "delete from RoleRight where roleId=@roleId";
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@roleId", roleId));
		}
		public static int DeleteByRightCode(string code)
		{
			string rightCode;
			if (code.Length == 2)
			{
				rightCode = "0" + code + "%";
			}
			else
			{
				rightCode = code;
			}
			string sql = "delete from RoleRight where rightCode like @code";
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@code", rightCode));
		}
	}
}
