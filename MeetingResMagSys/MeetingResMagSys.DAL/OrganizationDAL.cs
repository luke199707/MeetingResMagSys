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
	public partial class OrganizationDAL
	{
        public static object Insert(Organization organization)
		{
				string sql ="INSERT INTO Organization (organizationId, name, introduction, logo, reseStart, reseEnd, timeUnit, signIn, sameTimeAttend, time, remark)  output inserted.id VALUES (@organizationId, @name, @introduction, @logo, @reseStart, @reseEnd, @timeUnit, @signIn, @sameTimeAttend, @time, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@organizationId", ToDBValue(organization.OrganizationId)),
						new SqlParameter("@name", ToDBValue(organization.Name)),
						new SqlParameter("@introduction", ToDBValue(organization.Introduction)),
						new SqlParameter("@logo", ToDBValue(organization.Logo)),
						new SqlParameter("@reseStart", ToDBValue(organization.ReseStart)),
						new SqlParameter("@reseEnd", ToDBValue(organization.ReseEnd)),
						new SqlParameter("@timeUnit", ToDBValue(organization.TimeUnit)),
						new SqlParameter("@signIn", ToDBValue(organization.SignIn)),
						new SqlParameter("@sameTimeAttend", ToDBValue(organization.SameTimeAttend)),
						new SqlParameter("@time", ToDBValue(organization.Time)),
						new SqlParameter("@remark", ToDBValue(organization.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM Organization WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(Organization organization)
        {
            string sql =
                @"UPDATE Organization SET  organizationId = @organizationId 
                , name = @name 
                , introduction = @introduction 
                , logo = @logo 
                , reseStart = @reseStart 
                , reseEnd = @reseEnd 
                , timeUnit = @timeUnit 
                , signIn = @signIn 
                , sameTimeAttend = @sameTimeAttend 
                , time = @time 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", organization.Id)
					,new SqlParameter("@organizationId", ToDBValue(organization.OrganizationId))
					,new SqlParameter("@name", ToDBValue(organization.Name))
					,new SqlParameter("@introduction", ToDBValue(organization.Introduction))
					,new SqlParameter("@logo", ToDBValue(organization.Logo))
					,new SqlParameter("@reseStart", ToDBValue(organization.ReseStart))
					,new SqlParameter("@reseEnd", ToDBValue(organization.ReseEnd))
					,new SqlParameter("@timeUnit", ToDBValue(organization.TimeUnit))
					,new SqlParameter("@signIn", ToDBValue(organization.SignIn))
					,new SqlParameter("@sameTimeAttend", ToDBValue(organization.SameTimeAttend))
					,new SqlParameter("@time", ToDBValue(organization.Time))
					,new SqlParameter("@remark", ToDBValue(organization.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static Organization GetById(int id)
        {
            string sql = "SELECT * FROM Organization WHERE Id = @Id";
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
		
		public static Organization ToModel(SqlDataReader reader)
		{
			Organization organization = new Organization();

			organization.Id = (int)ToModelValue(reader,"id");
			organization.OrganizationId = (string)ToModelValue(reader,"organizationId");
			organization.Name = (string)ToModelValue(reader,"name");
			organization.Introduction = (string)ToModelValue(reader,"introduction");
			organization.Logo = (string)ToModelValue(reader,"logo");
			organization.ReseStart = (string)ToModelValue(reader,"reseStart");
			organization.ReseEnd = (string)ToModelValue(reader,"reseEnd");
			organization.TimeUnit = (string)ToModelValue(reader,"timeUnit");
			organization.SignIn = (string)ToModelValue(reader,"signIn");
			organization.SameTimeAttend = (string)ToModelValue(reader,"sameTimeAttend");
			organization.Time = (DateTime?)ToModelValue(reader,"time");
			organization.Remark = (string)ToModelValue(reader,"remark");
			return organization;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM Organization";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<Organization> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM Organization ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<Organization> GetAll()
		{
			string sql = "SELECT * FROM Organization";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<Organization> ToModels(SqlDataReader reader)
		{
			var list = new List<Organization>();
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
		public static Organization GetMaxOrgId()
		{
			string sql = "select top(1)* from Organization order by id desc";
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
		public static Organization GetByName(string name)
		{
			string sql = "SELECT * FROM Organization WHERE name = @name";
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
		public static Organization GetByIdName(string organizationId, string name)
		{
			string sql = "SELECT * FROM Organization WHERE organizationId=@organizationId and name = @name";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@organizationId", organizationId),
				new SqlParameter("@name", name)))
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
		public static Organization GetByOrganizationId(string organizationId)
		{
			string sql = "SELECT * FROM Organization WHERE organizationId=@organizationId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@organizationId", organizationId)))
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
		public static string GetNameByORGId(string organizationId)
		{
			string sql = "SELECT name FROM Organization WHERE organizationId=@organizationId";
			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@organizationId", organizationId)
			};
			string name = (string)SqlHelper.ExecuteScalar(CommandType.Text, sql, para);
			if (name != null)
			{
				return name;
			}
			else
			{
				return null;
			}
		}
		public static string CreateOrgId()
		{
			Organization model = GetMaxOrgId();
			StringBuilder sb = new StringBuilder();
			sb.Append("ORG");
			sb.Append(DateTime.Now.ToString("yyyyMMdd"));
			if (model == null)
			{
				sb.Append("0001");
				return sb.ToString();
			}
			else
			{
				int newid = int.Parse(model.OrganizationId.Substring(model.OrganizationId.Length - 4)) + 1;
				string maxid = newid.ToString();
				for (int i = maxid.Length; i < 4; i++)
				{
					maxid = "0" + maxid;
				}
				sb.Append(maxid);
				return sb.ToString();
			}
		}
	}
}
