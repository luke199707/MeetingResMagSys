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
	public partial class MeetingRoomTypeEMTDAL
	{
        public static object Insert(MeetingRoomTypeEMT meetingRoomTypeEMT)
		{
				string sql ="INSERT INTO MeetingRoomTypeEMT (organizationId, cname, lable, type)  output inserted.id VALUES (@organizationId, @cname, @lable, @type)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@organizationId", ToDBValue(meetingRoomTypeEMT.OrganizationId)),
						new SqlParameter("@cname", ToDBValue(meetingRoomTypeEMT.Cname)),
						new SqlParameter("@lable", ToDBValue(meetingRoomTypeEMT.Lable)),
						new SqlParameter("@type", ToDBValue(meetingRoomTypeEMT.Type)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM MeetingRoomTypeEMT WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(MeetingRoomTypeEMT meetingRoomTypeEMT)
        {
            string sql =
                @"UPDATE MeetingRoomTypeEMT SET  organizationId = @organizationId 
                , cname = @cname 
                , lable = @lable 
                , type = @type 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", meetingRoomTypeEMT.Id)
					,new SqlParameter("@organizationId", ToDBValue(meetingRoomTypeEMT.OrganizationId))
					,new SqlParameter("@cname", ToDBValue(meetingRoomTypeEMT.Cname))
					,new SqlParameter("@lable", ToDBValue(meetingRoomTypeEMT.Lable))
					,new SqlParameter("@type", ToDBValue(meetingRoomTypeEMT.Type))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static MeetingRoomTypeEMT GetById(int id)
        {
            string sql = "SELECT * FROM MeetingRoomTypeEMT WHERE Id = @Id";
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
		
		public static MeetingRoomTypeEMT ToModel(SqlDataReader reader)
		{
			MeetingRoomTypeEMT meetingRoomTypeEMT = new MeetingRoomTypeEMT();

			meetingRoomTypeEMT.Id = (int)ToModelValue(reader,"id");
			meetingRoomTypeEMT.OrganizationId = (string)ToModelValue(reader,"organizationId");
			meetingRoomTypeEMT.Cname = (string)ToModelValue(reader,"cname");
			meetingRoomTypeEMT.Lable = (string)ToModelValue(reader,"lable");
			meetingRoomTypeEMT.Type = (string)ToModelValue(reader,"type");
			return meetingRoomTypeEMT;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM MeetingRoomTypeEMT";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<MeetingRoomTypeEMT> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingRoomTypeEMT ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<MeetingRoomTypeEMT> GetAll()
		{
			string sql = "SELECT * FROM MeetingRoomTypeEMT";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<MeetingRoomTypeEMT> ToModels(SqlDataReader reader)
		{
			var list = new List<MeetingRoomTypeEMT>();
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
