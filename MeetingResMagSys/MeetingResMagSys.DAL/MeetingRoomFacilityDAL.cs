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
	public partial class MeetingRoomFacilityDAL
	{
        public static object Insert(MeetingRoomFacility meetingRoomFacility)
		{
				string sql ="INSERT INTO MeetingRoomFacility (roomId, facilityId)  output inserted.id VALUES (@roomId, @facilityId)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@roomId", ToDBValue(meetingRoomFacility.RoomId)),
						new SqlParameter("@facilityId", ToDBValue(meetingRoomFacility.FacilityId)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM MeetingRoomFacility WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(MeetingRoomFacility meetingRoomFacility)
        {
            string sql =
                @"UPDATE MeetingRoomFacility SET  roomId = @roomId 
                , facilityId = @facilityId 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", meetingRoomFacility.Id)
					,new SqlParameter("@roomId", ToDBValue(meetingRoomFacility.RoomId))
					,new SqlParameter("@facilityId", ToDBValue(meetingRoomFacility.FacilityId))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static MeetingRoomFacility GetById(int id)
        {
            string sql = "SELECT * FROM MeetingRoomFacility WHERE Id = @Id";
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
		
		public static MeetingRoomFacility ToModel(SqlDataReader reader)
		{
			MeetingRoomFacility meetingRoomFacility = new MeetingRoomFacility();

			meetingRoomFacility.Id = (int)ToModelValue(reader,"id");
			meetingRoomFacility.RoomId = (string)ToModelValue(reader,"roomId");
			meetingRoomFacility.FacilityId = (string)ToModelValue(reader,"facilityId");
			return meetingRoomFacility;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM MeetingRoomFacility";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<MeetingRoomFacility> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingRoomFacility ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<MeetingRoomFacility> GetAll()
		{
			string sql = "SELECT * FROM MeetingRoomFacility";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<MeetingRoomFacility> ToModels(SqlDataReader reader)
		{
			var list = new List<MeetingRoomFacility>();
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
		//----------------------------------------非自动生成----------------------------------------
		public static List<MeetingRoomFacility> GetListByRoomId(string roomId)
		{
			string sql = "SELECT * FROM MeetingRoomFacility where roomId=@roomId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@roomId",roomId)))
			{
				return ToModels(reader);
			}
		}
		public static int DeleteByRoomId(string roomId)
		{
			string sql = "DELETE FROM MeetingRoomFacility WHERE roomId = @roomId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@roomId", roomId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
		public static MeetingRoomFacility GetByRoomIdFacilityId(string roomId, string facilityId)
		{
			string sql = "SELECT * FROM MeetingRoomFacility WHERE roomId = @roomId and facilityId=@facilityId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, 
				new SqlParameter("@roomId", roomId), new SqlParameter("@facilityId", facilityId)))
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
