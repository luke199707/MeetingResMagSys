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
	public partial class MeetingRoomTypeETDAL
	{
        public static object Insert(MeetingRoomTypeET meetingRoomTypeET)
		{
				string sql ="INSERT INTO MeetingRoomTypeET (organizationId, RoomTypeId, cname, value)  output inserted.id VALUES (@organizationId, @RoomTypeId, @cname, @value)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@organizationId", ToDBValue(meetingRoomTypeET.OrganizationId)),
						new SqlParameter("@RoomTypeId", ToDBValue(meetingRoomTypeET.RoomTypeId)),
						new SqlParameter("@cname", ToDBValue(meetingRoomTypeET.Cname)),
						new SqlParameter("@value", ToDBValue(meetingRoomTypeET.Value)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM MeetingRoomTypeET WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(MeetingRoomTypeET meetingRoomTypeET)
        {
            string sql =
                @"UPDATE MeetingRoomTypeET SET  organizationId = @organizationId 
                , RoomTypeId = @RoomTypeId 
                , cname = @cname 
                , value = @value 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", meetingRoomTypeET.Id)
					,new SqlParameter("@organizationId", ToDBValue(meetingRoomTypeET.OrganizationId))
					,new SqlParameter("@RoomTypeId", ToDBValue(meetingRoomTypeET.RoomTypeId))
					,new SqlParameter("@cname", ToDBValue(meetingRoomTypeET.Cname))
					,new SqlParameter("@value", ToDBValue(meetingRoomTypeET.Value))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static MeetingRoomTypeET GetById(int id)
        {
            string sql = "SELECT * FROM MeetingRoomTypeET WHERE Id = @Id";
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
		
		public static MeetingRoomTypeET ToModel(SqlDataReader reader)
		{
			MeetingRoomTypeET meetingRoomTypeET = new MeetingRoomTypeET();

			meetingRoomTypeET.Id = (int)ToModelValue(reader,"id");
			meetingRoomTypeET.OrganizationId = (string)ToModelValue(reader,"organizationId");
			meetingRoomTypeET.RoomTypeId = (string)ToModelValue(reader,"RoomTypeId");
			meetingRoomTypeET.Cname = (string)ToModelValue(reader,"cname");
			meetingRoomTypeET.Value = (string)ToModelValue(reader,"value");
			return meetingRoomTypeET;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM MeetingRoomTypeET";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<MeetingRoomTypeET> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingRoomTypeET ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<MeetingRoomTypeET> GetAll()
		{
			string sql = "SELECT * FROM MeetingRoomTypeET";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<MeetingRoomTypeET> ToModels(SqlDataReader reader)
		{
			var list = new List<MeetingRoomTypeET>();
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
