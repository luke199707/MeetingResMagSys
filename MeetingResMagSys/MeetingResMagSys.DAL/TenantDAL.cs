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
	public partial class TenantDAL
	{
        public static object Insert(Tenant tenant)
		{
				string sql ="INSERT INTO Tenant (userId, organizationId, remark)  output inserted.id VALUES (@userId, @organizationId, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@userId", ToDBValue(tenant.UserId)),
						new SqlParameter("@organizationId", ToDBValue(tenant.OrganizationId)),
						new SqlParameter("@remark", ToDBValue(tenant.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM Tenant WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(Tenant tenant)
        {
            string sql =
                @"UPDATE Tenant SET  userId = @userId 
                , organizationId = @organizationId 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", tenant.Id)
					,new SqlParameter("@userId", ToDBValue(tenant.UserId))
					,new SqlParameter("@organizationId", ToDBValue(tenant.OrganizationId))
					,new SqlParameter("@remark", ToDBValue(tenant.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static Tenant GetById(int id)
        {
            string sql = "SELECT * FROM Tenant WHERE Id = @Id";
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
		
		public static Tenant ToModel(SqlDataReader reader)
		{
			Tenant tenant = new Tenant();

			tenant.Id = (int)ToModelValue(reader,"id");
			tenant.UserId = (string)ToModelValue(reader,"userId");
			tenant.OrganizationId = (string)ToModelValue(reader,"organizationId");
			tenant.Remark = (string)ToModelValue(reader,"remark");
			return tenant;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM Tenant";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<Tenant> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM Tenant ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<Tenant> GetAll()
		{
			string sql = "SELECT * FROM Tenant";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<Tenant> ToModels(SqlDataReader reader)
		{
			var list = new List<Tenant>();
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
	}
}
