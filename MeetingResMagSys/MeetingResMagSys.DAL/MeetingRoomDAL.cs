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
	public partial class MeetingRoomDAL
	{
        public static object Insert(MeetingRoom meetingRoom)
		{
				string sql ="INSERT INTO MeetingRoom (roomId, name, image, officeArea, position, capacity, type, introduction, facility, attention, resDepartment, director, useRole, useDepartment, available, reason, organizationId, isCheck, time, remark)  output inserted.id VALUES (@roomId, @name, @image, @officeArea, @position, @capacity, @type, @introduction, @facility, @attention, @resDepartment, @director, @useRole, @useDepartment, @available, @reason, @organizationId, @isCheck, @time, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@roomId", ToDBValue(meetingRoom.RoomId)),
						new SqlParameter("@name", ToDBValue(meetingRoom.Name)),
						new SqlParameter("@image", ToDBValue(meetingRoom.Image)),
						new SqlParameter("@officeArea", ToDBValue(meetingRoom.OfficeArea)),
						new SqlParameter("@position", ToDBValue(meetingRoom.Position)),
						new SqlParameter("@capacity", ToDBValue(meetingRoom.Capacity)),
						new SqlParameter("@type", ToDBValue(meetingRoom.Type)),
						new SqlParameter("@introduction", ToDBValue(meetingRoom.Introduction)),
						new SqlParameter("@facility", ToDBValue(meetingRoom.Facility)),
						new SqlParameter("@attention", ToDBValue(meetingRoom.Attention)),
						new SqlParameter("@resDepartment", ToDBValue(meetingRoom.ResDepartment)),
						new SqlParameter("@director", ToDBValue(meetingRoom.Director)),
						new SqlParameter("@useRole", ToDBValue(meetingRoom.UseRole)),
						new SqlParameter("@useDepartment", ToDBValue(meetingRoom.UseDepartment)),
						new SqlParameter("@available", ToDBValue(meetingRoom.Available)),
						new SqlParameter("@reason", ToDBValue(meetingRoom.Reason)),
						new SqlParameter("@organizationId", ToDBValue(meetingRoom.OrganizationId)),
						new SqlParameter("@isCheck", ToDBValue(meetingRoom.IsCheck)),
						new SqlParameter("@time", ToDBValue(meetingRoom.Time)),
						new SqlParameter("@remark", ToDBValue(meetingRoom.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM MeetingRoom WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(MeetingRoom meetingRoom)
        {
            string sql =
                @"UPDATE MeetingRoom SET  roomId = @roomId 
                , name = @name 
                , image = @image 
                , officeArea = @officeArea 
                , position = @position 
                , capacity = @capacity 
                , type = @type 
                , introduction = @introduction 
                , facility = @facility 
                , attention = @attention 
                , resDepartment = @resDepartment 
                , director = @director 
                , useRole = @useRole 
                , useDepartment = @useDepartment 
                , available = @available 
                , reason = @reason 
                , organizationId = @organizationId 
                , isCheck = @isCheck 
                , time = @time 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", meetingRoom.Id)
					,new SqlParameter("@roomId", ToDBValue(meetingRoom.RoomId))
					,new SqlParameter("@name", ToDBValue(meetingRoom.Name))
					,new SqlParameter("@image", ToDBValue(meetingRoom.Image))
					,new SqlParameter("@officeArea", ToDBValue(meetingRoom.OfficeArea))
					,new SqlParameter("@position", ToDBValue(meetingRoom.Position))
					,new SqlParameter("@capacity", ToDBValue(meetingRoom.Capacity))
					,new SqlParameter("@type", ToDBValue(meetingRoom.Type))
					,new SqlParameter("@introduction", ToDBValue(meetingRoom.Introduction))
					,new SqlParameter("@facility", ToDBValue(meetingRoom.Facility))
					,new SqlParameter("@attention", ToDBValue(meetingRoom.Attention))
					,new SqlParameter("@resDepartment", ToDBValue(meetingRoom.ResDepartment))
					,new SqlParameter("@director", ToDBValue(meetingRoom.Director))
					,new SqlParameter("@useRole", ToDBValue(meetingRoom.UseRole))
					,new SqlParameter("@useDepartment", ToDBValue(meetingRoom.UseDepartment))
					,new SqlParameter("@available", ToDBValue(meetingRoom.Available))
					,new SqlParameter("@reason", ToDBValue(meetingRoom.Reason))
					,new SqlParameter("@organizationId", ToDBValue(meetingRoom.OrganizationId))
					,new SqlParameter("@isCheck", ToDBValue(meetingRoom.IsCheck))
					,new SqlParameter("@time", ToDBValue(meetingRoom.Time))
					,new SqlParameter("@remark", ToDBValue(meetingRoom.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static MeetingRoom GetById(int id)
        {
            string sql = "SELECT * FROM MeetingRoom WHERE Id = @Id";
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
		
		public static MeetingRoom ToModel(SqlDataReader reader)
		{
			MeetingRoom meetingRoom = new MeetingRoom();

			meetingRoom.Id = (int)ToModelValue(reader,"id");
			meetingRoom.RoomId = (string)ToModelValue(reader,"roomId");
			meetingRoom.Name = (string)ToModelValue(reader,"name");
			meetingRoom.Image = (string)ToModelValue(reader,"image");
			meetingRoom.OfficeArea = (string)ToModelValue(reader,"officeArea");
			meetingRoom.Position = (string)ToModelValue(reader,"position");
			meetingRoom.Capacity = (string)ToModelValue(reader,"capacity");
			meetingRoom.Type = (string)ToModelValue(reader,"type");
			meetingRoom.Introduction = (string)ToModelValue(reader,"introduction");
			meetingRoom.Facility = (string)ToModelValue(reader,"facility");
			meetingRoom.Attention = (string)ToModelValue(reader,"attention");
			meetingRoom.ResDepartment = (string)ToModelValue(reader,"resDepartment");
			meetingRoom.Director = (string)ToModelValue(reader,"director");
			meetingRoom.UseRole = (string)ToModelValue(reader,"useRole");
			meetingRoom.UseDepartment = (string)ToModelValue(reader,"useDepartment");
			meetingRoom.Available = (string)ToModelValue(reader,"available");
			meetingRoom.Reason = (string)ToModelValue(reader,"reason");
			meetingRoom.OrganizationId = (string)ToModelValue(reader,"organizationId");
			meetingRoom.IsCheck = (string)ToModelValue(reader,"isCheck");
			meetingRoom.Time = (DateTime?)ToModelValue(reader,"time");
			meetingRoom.Remark = (string)ToModelValue(reader,"remark");
			return meetingRoom;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM MeetingRoom";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<MeetingRoom> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingRoom ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<MeetingRoom> GetAll()
		{
			string sql = "SELECT * FROM MeetingRoom";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<MeetingRoom> ToModels(SqlDataReader reader)
		{
			var list = new List<MeetingRoom>();
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
		//-----------------------------------------------手动添加---------------------------------------------
		public static int DeleteByRoomId(string roomId)
		{
			string sql = "DELETE FROM MeetingRoom WHERE roomId = @roomId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@roomId", roomId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
		public static MeetingRoom GetMaxRoomId()
		{
			string sql = "select top(1)* from MeetingRoom order by id desc";
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
		public static string CreateRoomId()
		{
			MeetingRoom model = GetMaxRoomId();
			StringBuilder sb = new StringBuilder();
			sb.Append("MRID");
			sb.Append(DateTime.Now.ToString("yyyyMMdd"));
			if (model == null)
			{
				sb.Append("0001");
				return sb.ToString();
			}
			else
			{
				int newid = int.Parse(model.RoomId.Substring(model.RoomId.Length - 4)) + 1;
				string maxid = newid.ToString().PadLeft(4, '0');
				sb.Append(maxid);
				return sb.ToString();
			}
		}
		public static MeetingRoom GetByRoomId(string roomId)
		{
			string sql = "SELECT * FROM MeetingRoom WHERE roomId = @roomId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@roomId", roomId)))
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
