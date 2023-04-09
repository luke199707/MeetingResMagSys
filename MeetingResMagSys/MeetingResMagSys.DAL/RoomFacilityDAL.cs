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
	public partial class RoomFacilityDAL
	{
        public static object Insert(RoomFacility roomFacility)
		{
				string sql ="INSERT INTO RoomFacility (facilityId, name, introduction, organizationId, remark)  output inserted.id VALUES (@facilityId, @name, @introduction, @organizationId, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@facilityId", ToDBValue(roomFacility.FacilityId)),
						new SqlParameter("@name", ToDBValue(roomFacility.Name)),
						new SqlParameter("@introduction", ToDBValue(roomFacility.Introduction)),
						new SqlParameter("@organizationId", ToDBValue(roomFacility.OrganizationId)),
						new SqlParameter("@remark", ToDBValue(roomFacility.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM RoomFacility WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(RoomFacility roomFacility)
        {
            string sql =
                @"UPDATE RoomFacility SET  facilityId = @facilityId 
                , name = @name 
                , introduction = @introduction 
                , organizationId = @organizationId 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", roomFacility.Id)
					,new SqlParameter("@facilityId", ToDBValue(roomFacility.FacilityId))
					,new SqlParameter("@name", ToDBValue(roomFacility.Name))
					,new SqlParameter("@introduction", ToDBValue(roomFacility.Introduction))
					,new SqlParameter("@organizationId", ToDBValue(roomFacility.OrganizationId))
					,new SqlParameter("@remark", ToDBValue(roomFacility.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static RoomFacility GetById(int id)
        {
            string sql = "SELECT * FROM RoomFacility WHERE Id = @Id";
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
		
		public static RoomFacility ToModel(SqlDataReader reader)
		{
			RoomFacility roomFacility = new RoomFacility();

			roomFacility.Id = (int)ToModelValue(reader,"id");
			roomFacility.FacilityId = (string)ToModelValue(reader,"facilityId");
			roomFacility.Name = (string)ToModelValue(reader,"name");
			roomFacility.Introduction = (string)ToModelValue(reader,"introduction");
			roomFacility.OrganizationId = (string)ToModelValue(reader,"organizationId");
			roomFacility.Remark = (string)ToModelValue(reader,"remark");
			return roomFacility;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM RoomFacility";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<RoomFacility> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM RoomFacility ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<RoomFacility> GetAll()
		{
			string sql = "SELECT * FROM RoomFacility";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<RoomFacility> ToModels(SqlDataReader reader)
		{
			var list = new List<RoomFacility>();
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
		public static RoomFacility GetMaxFacilityId()
		{
			string sql = "select top(1)* from RoomFacility order by id desc";
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
		public static string CreateFacilityId()
		{
			RoomFacility model = GetMaxFacilityId();
			StringBuilder sb = new StringBuilder();
			sb.Append("RFID");
			sb.Append(DateTime.Now.ToString("yyyyMMdd"));
			if (model == null)
			{
				sb.Append("0001");
				return sb.ToString();
			}
			else
			{
				int newid = int.Parse(model.FacilityId.Substring(model.FacilityId.Length - 4)) + 1;
				string maxid = newid.ToString().PadLeft(4, '0');
				sb.Append(maxid);
				return sb.ToString();
			}
		}
		public static RoomFacility GetByFacilityId(string facilityId)
		{
			string sql = "SELECT * FROM RoomFacility WHERE facilityId = @facilityId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@facilityId", facilityId)))
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
		public static int DeleteByFacilityId(string facilityId)
		{
			string sql = "DELETE FROM RoomFacility WHERE facilityId = @facilityId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@facilityId", facilityId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
		public static List<RoomFacility> GetListByOrgId(string organizationId)
		{
			string sql = "SELECT * FROM RoomFacility where organizationId=@organizationId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@organizationId", organizationId)))
			{
				return ToModels(reader);
			}
		}
	}
}
