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
	public partial class MeetingMemberDAL
	{
        public static object Insert(MeetingMember meetingMember)
		{
				string sql ="INSERT INTO MeetingMember (meetingId, userId, organizationId, organizationName, remark)  output inserted.id VALUES (@meetingId, @userId, @organizationId, @organizationName, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@meetingId", ToDBValue(meetingMember.MeetingId)),
						new SqlParameter("@userId", ToDBValue(meetingMember.UserId)),
						new SqlParameter("@organizationId", ToDBValue(meetingMember.OrganizationId)),
						new SqlParameter("@organizationName", ToDBValue(meetingMember.OrganizationName)),
						new SqlParameter("@remark", ToDBValue(meetingMember.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM MeetingMember WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(MeetingMember meetingMember)
        {
            string sql =
                @"UPDATE MeetingMember SET  meetingId = @meetingId 
                , userId = @userId 
                , organizationId = @organizationId 
                , organizationName = @organizationName 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", meetingMember.Id)
					,new SqlParameter("@meetingId", ToDBValue(meetingMember.MeetingId))
					,new SqlParameter("@userId", ToDBValue(meetingMember.UserId))
					,new SqlParameter("@organizationId", ToDBValue(meetingMember.OrganizationId))
					,new SqlParameter("@organizationName", ToDBValue(meetingMember.OrganizationName))
					,new SqlParameter("@remark", ToDBValue(meetingMember.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static MeetingMember GetById(int id)
        {
            string sql = "SELECT * FROM MeetingMember WHERE Id = @Id";
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
		
		public static MeetingMember ToModel(SqlDataReader reader)
		{
			MeetingMember meetingMember = new MeetingMember();

			meetingMember.Id = (int)ToModelValue(reader,"id");
			meetingMember.MeetingId = (string)ToModelValue(reader,"meetingId");
			meetingMember.UserId = (string)ToModelValue(reader,"userId");
			meetingMember.OrganizationId = (string)ToModelValue(reader,"organizationId");
			meetingMember.OrganizationName = (string)ToModelValue(reader,"organizationName");
			meetingMember.Remark = (string)ToModelValue(reader,"remark");
			return meetingMember;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM MeetingMember";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<MeetingMember> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingMember ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<MeetingMember> GetAll()
		{
			string sql = "SELECT * FROM MeetingMember";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<MeetingMember> ToModels(SqlDataReader reader)
		{
			var list = new List<MeetingMember>();
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
		//----------------------------------------非自动生成-----------------------------------------
		public static MeetingMember GetByMeetingIdUserId(string meetingId,string userId)
		{
			string sql = "SELECT * FROM MeetingMember WHERE meetingId = @meetingId and userId=@userId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@meetingId", meetingId),new SqlParameter("@userId", userId)))
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
		public static int DeleteByMeetingId(string meetingId)
		{
			string sql = "DELETE FROM MeetingMember WHERE meetingId = @meetingId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@meetingId", meetingId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
	}
}
