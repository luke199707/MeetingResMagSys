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
	public partial class MeetingRoomBanDepDAL
	{
        public static object Insert(MeetingRoomBanDep meetingRoomBanDep)
		{
				string sql ="INSERT INTO MeetingRoomBanDep (roomId, departmentId)  output inserted.id VALUES (@roomId, @departmentId)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@roomId", ToDBValue(meetingRoomBanDep.RoomId)),
						new SqlParameter("@departmentId", ToDBValue(meetingRoomBanDep.DepartmentId)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM MeetingRoomBanDep WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(MeetingRoomBanDep meetingRoomBanDep)
        {
            string sql =
                @"UPDATE MeetingRoomBanDep SET  roomId = @roomId 
                , departmentId = @departmentId 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", meetingRoomBanDep.Id)
					,new SqlParameter("@roomId", ToDBValue(meetingRoomBanDep.RoomId))
					,new SqlParameter("@departmentId", ToDBValue(meetingRoomBanDep.DepartmentId))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static MeetingRoomBanDep GetById(int id)
        {
            string sql = "SELECT * FROM MeetingRoomBanDep WHERE Id = @Id";
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
		
		public static MeetingRoomBanDep ToModel(SqlDataReader reader)
		{
			MeetingRoomBanDep meetingRoomBanDep = new MeetingRoomBanDep();

			meetingRoomBanDep.Id = (int)ToModelValue(reader,"id");
			meetingRoomBanDep.RoomId = (string)ToModelValue(reader,"roomId");
			meetingRoomBanDep.DepartmentId = (string)ToModelValue(reader,"departmentId");
			return meetingRoomBanDep;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM MeetingRoomBanDep";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<MeetingRoomBanDep> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingRoomBanDep ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<MeetingRoomBanDep> GetAll()
		{
			string sql = "SELECT * FROM MeetingRoomBanDep";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<MeetingRoomBanDep> ToModels(SqlDataReader reader)
		{
			var list = new List<MeetingRoomBanDep>();
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
		public static List<MeetingRoomBanDep> GetListByRoomId(string roomId)
		{
			string sql = "SELECT * FROM MeetingRoomBanDep where roomId=@roomId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@roomId", roomId)))
			{
				return ToModels(reader);
			}
		}
		public static int DeleteByRoomId(string roomId)
		{
			string sql = "DELETE FROM MeetingRoomBanDep WHERE roomId = @roomId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@roomId", roomId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
		public static int DeleteByDepId(string departmentId)
		{
			string sql = "DELETE FROM MeetingRoomBanDep WHERE departmentId = @departmentId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@departmentId", departmentId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
		public static MeetingRoomBanDep GetByRoomIdDepId(string roomId, string departmentId)
		{
			string sql = "SELECT * FROM MeetingRoomBanDep WHERE roomId = @roomId and departmentId=@departmentId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@roomId", roomId), new SqlParameter("@departmentId", departmentId)))
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
