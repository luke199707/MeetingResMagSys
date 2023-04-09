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
	public partial class MeetingReservationDAL
	{
        public static object Insert(MeetingReservation meetingReservation)
		{
				string sql ="INSERT INTO MeetingReservation (meetingId, title, meetingRoom, introduction, time, startTime, endTime, booker, department, state, reviewer, organizationId, orderTime, remark, refuseReason)  output inserted.id VALUES (@meetingId, @title, @meetingRoom, @introduction, @time, @startTime, @endTime, @booker, @department, @state, @reviewer, @organizationId, @orderTime, @remark, @refuseReason)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@meetingId", ToDBValue(meetingReservation.MeetingId)),
						new SqlParameter("@title", ToDBValue(meetingReservation.Title)),
						new SqlParameter("@meetingRoom", ToDBValue(meetingReservation.MeetingRoom)),
						new SqlParameter("@introduction", ToDBValue(meetingReservation.Introduction)),
						new SqlParameter("@time", ToDBValue(meetingReservation.Time)),
						new SqlParameter("@startTime", ToDBValue(meetingReservation.StartTime)),
						new SqlParameter("@endTime", ToDBValue(meetingReservation.EndTime)),
						new SqlParameter("@booker", ToDBValue(meetingReservation.Booker)),
						new SqlParameter("@department", ToDBValue(meetingReservation.Department)),
						new SqlParameter("@state", ToDBValue(meetingReservation.State)),
						new SqlParameter("@reviewer", ToDBValue(meetingReservation.Reviewer)),
						new SqlParameter("@organizationId", ToDBValue(meetingReservation.OrganizationId)),
						new SqlParameter("@orderTime", ToDBValue(meetingReservation.OrderTime)),
						new SqlParameter("@remark", ToDBValue(meetingReservation.Remark)),
						new SqlParameter("@refuseReason", ToDBValue(meetingReservation.RefuseReason)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM MeetingReservation WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(MeetingReservation meetingReservation)
        {
            string sql =
                @"UPDATE MeetingReservation SET  meetingId = @meetingId 
                , title = @title 
                , meetingRoom = @meetingRoom 
                , introduction = @introduction 
                , time = @time 
                , startTime = @startTime 
                , endTime = @endTime 
                , booker = @booker 
                , department = @department 
                , state = @state 
                , reviewer = @reviewer 
                , organizationId = @organizationId 
                , orderTime = @orderTime 
                , remark = @remark 
                , refuseReason = @refuseReason 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", meetingReservation.Id)
					,new SqlParameter("@meetingId", ToDBValue(meetingReservation.MeetingId))
					,new SqlParameter("@title", ToDBValue(meetingReservation.Title))
					,new SqlParameter("@meetingRoom", ToDBValue(meetingReservation.MeetingRoom))
					,new SqlParameter("@introduction", ToDBValue(meetingReservation.Introduction))
					,new SqlParameter("@time", ToDBValue(meetingReservation.Time))
					,new SqlParameter("@startTime", ToDBValue(meetingReservation.StartTime))
					,new SqlParameter("@endTime", ToDBValue(meetingReservation.EndTime))
					,new SqlParameter("@booker", ToDBValue(meetingReservation.Booker))
					,new SqlParameter("@department", ToDBValue(meetingReservation.Department))
					,new SqlParameter("@state", ToDBValue(meetingReservation.State))
					,new SqlParameter("@reviewer", ToDBValue(meetingReservation.Reviewer))
					,new SqlParameter("@organizationId", ToDBValue(meetingReservation.OrganizationId))
					,new SqlParameter("@orderTime", ToDBValue(meetingReservation.OrderTime))
					,new SqlParameter("@remark", ToDBValue(meetingReservation.Remark))
					,new SqlParameter("@refuseReason", ToDBValue(meetingReservation.RefuseReason))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static MeetingReservation GetById(int id)
        {
            string sql = "SELECT * FROM MeetingReservation WHERE Id = @Id";
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
		
		public static MeetingReservation ToModel(SqlDataReader reader)
		{
			MeetingReservation meetingReservation = new MeetingReservation();

			meetingReservation.Id = (int)ToModelValue(reader,"id");
			meetingReservation.MeetingId = (string)ToModelValue(reader,"meetingId");
			meetingReservation.Title = (string)ToModelValue(reader,"title");
			meetingReservation.MeetingRoom = (string)ToModelValue(reader,"meetingRoom");
			meetingReservation.Introduction = (string)ToModelValue(reader,"introduction");
			meetingReservation.Time = (string)ToModelValue(reader,"time");
			meetingReservation.StartTime = (string)ToModelValue(reader,"startTime");
			meetingReservation.EndTime = (string)ToModelValue(reader,"endTime");
			meetingReservation.Booker = (string)ToModelValue(reader,"booker");
			meetingReservation.Department = (string)ToModelValue(reader,"department");
			meetingReservation.State = (string)ToModelValue(reader,"state");
			meetingReservation.Reviewer = (string)ToModelValue(reader,"reviewer");
			meetingReservation.OrganizationId = (string)ToModelValue(reader,"organizationId");
			meetingReservation.OrderTime = (string)ToModelValue(reader,"orderTime");
			meetingReservation.Remark = (string)ToModelValue(reader,"remark");
			meetingReservation.RefuseReason = (string)ToModelValue(reader,"refuseReason");
			return meetingReservation;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM MeetingReservation";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<MeetingReservation> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingReservation ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<MeetingReservation> GetAll()
		{
			string sql = "SELECT * FROM MeetingReservation";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<MeetingReservation> ToModels(SqlDataReader reader)
		{
			var list = new List<MeetingReservation>();
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
        public static MeetingReservation GetMaxMeetingId()
        {
            string sql = "select top(1)* from MeetingReservation order by id desc";
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
        public static string CreateMeetingId()
        {
            MeetingReservation model = GetMaxMeetingId();
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
                int newid = int.Parse(model.MeetingId.Substring(model.MeetingId.Length - 4)) + 1;
                string maxid = newid.ToString().PadLeft(4, '0');
                sb.Append(maxid);
                return sb.ToString();
            }
        }
        public static MeetingReservation GetByMeetingId(string meetingId)
        {
            string sql = "SELECT * FROM MeetingReservation WHERE meetingId = @meetingId";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@meetingId", meetingId)))
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
            string sql = "DELETE FROM MeetingReservation WHERE meetingId = @meetingId";

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@meetingId", meetingId)
            };

            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }
        public static List<MeetingReservation> GetAllByDateAndRoom(string roomId, string date, string orgId)
        {
            string sql = "SELECT * FROM MeetingReservation where meetingRoom = @meetingRoom and time=@time and organizationId=@organizationId";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@meetingRoom", roomId),
                new SqlParameter("@time",date),
                new SqlParameter("@organizationId",orgId)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, para))
            {
                return ToModels(reader);
            }
        }
        /// <summary>
        /// 重载，用于更新操作
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="date"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static List<MeetingReservation> GetAllByDateAndRoom(string roomId, string date, string meetingId, string orgId)
        {
            string sql = "SELECT * FROM MeetingReservation where meetingRoom = @meetingRoom and time=@time and meetingId<>@meetingId and organizationId=@organizationId";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@meetingRoom", roomId),
                new SqlParameter("@time",date),
                new SqlParameter("@meetingId",meetingId),
                new SqlParameter("@organizationId",orgId)
            };
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, para))
            {
                return ToModels(reader);
            }
        }
        public static bool compareTime(List<MeetingReservation> list, string StartTime, string EndTime)
        {
            bool flag = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (DateTime.Parse(StartTime).CompareTo(DateTime.Parse(list[i].StartTime)) < 0)
                {
                    if (DateTime.Parse(EndTime).CompareTo(DateTime.Parse(list[i].StartTime)) <= 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false; break;
                    }
                }
                else if (DateTime.Parse(StartTime).CompareTo(DateTime.Parse(list[i].EndTime)) >= 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false; break;
                }
            }
            return flag;
        }
        public static string getMouthMeetingCountByRoom(string roomName, string orgId)
        {
            string sql = "SELECT COUNT(meetingId) as 'count' " +
                " FROM MeetingReservation join MeetingRoom on  MeetingReservation.meetingRoom=MeetingRoom.roomId " +
                " where datediff(MONTH,MeetingReservation.time,getdate())=0 and MeetingReservation.organizationId=@organizationId and MeetingReservation.state='正常' and name=@roomName";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@roomName", roomName),
                new SqlParameter("@organizationId",orgId)
            };
            using (DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text, para))
            {
                if (dt.Rows[0][0] != null)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        
        public static string getYearMeetingCountByRoom(string roomName, string orgId)
        {
            string sql = "SELECT COUNT(meetingId) as 'count' " +
                " FROM MeetingReservation join MeetingRoom on  MeetingReservation.meetingRoom=MeetingRoom.roomId " +
                " where datediff(YEAR,MeetingReservation.time,getdate())=0 and MeetingReservation.organizationId=@organizationId and MeetingReservation.state='正常' and name=@roomName";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@roomName", roomName),
                new SqlParameter("@organizationId",orgId)
            };
            using (DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text, para))
            {
                if (dt.Rows[0][0] != null)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        
        public static string getTotalMeetingCountByRoom(string roomName,string orgId)
        {
            string sql = "SELECT COUNT(meetingId) as 'count' "+
                " FROM MeetingReservation join MeetingRoom on  MeetingReservation.meetingRoom=MeetingRoom.roomId "+
                " where MeetingReservation.organizationId=@organizationId and MeetingReservation.state='正常' and name=@roomName";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@roomName", roomName),
                new SqlParameter("@organizationId",orgId)
            };
            using (DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text, para))
            {
                if (dt.Rows[0][0]!=null)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        public static string getWeekMeetingCountByBooker(string userId, string orgId)
        {
            string sql = "SELECT COUNT(meetingId) as 'count' " +
                " FROM MeetingReservation " +
                " where datediff(WEEK,time,getdate())=0 and organizationId=@organizationId and state='正常' and booker=@booker";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@booker", userId),
                new SqlParameter("@organizationId",orgId)
            };
            using (DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text, para))
            {
                if (dt.Rows[0][0] != null)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        public static string getMouthMeetingCountByBooker(string userId, string orgId)
        {
            string sql = "SELECT COUNT(meetingId) as 'count' " +
                " FROM MeetingReservation " +
                " where datediff(MONTH,time,getdate())=0 and organizationId=@organizationId and state='正常' and booker=@booker";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@booker", userId),
                new SqlParameter("@organizationId",orgId)
            };
            using (DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text, para))
            {
                if (dt.Rows[0][0] != null)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        public static string getYearMeetingCountByBooker(string userId, string orgId)
        {
            string sql = "SELECT COUNT(meetingId) as 'count' " +
                " FROM MeetingReservation " +
                " where datediff(YEAR,time,getdate())=0 and organizationId=@organizationId and state='正常' and booker=@booker";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@booker", userId),
                new SqlParameter("@organizationId",orgId)
            };
            using (DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text, para))
            {
                if (dt.Rows[0][0] != null)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        public static string getTotalMeetingCountByBooker(string userId, string orgId)
        {
            string sql = "SELECT COUNT(meetingId) as 'count' " +
                " FROM MeetingReservation " +
                " where organizationId=@organizationId and state='正常' and booker=@booker";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@booker", userId),
                new SqlParameter("@organizationId",orgId)
            };
            using (DataTable dt = SqlHelper.ExecuteDataTable(sql, CommandType.Text, para))
            {
                if (dt.Rows[0][0] != null)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
    }
}
