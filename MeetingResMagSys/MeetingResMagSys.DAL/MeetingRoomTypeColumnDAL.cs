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
	public partial class MeetingRoomTypeColumnDAL
	{
        public static object Insert(MeetingRoomTypeColumn meetingRoomTypeColumn)
		{
				string sql ="INSERT INTO MeetingRoomTypeColumn (organizationId, cname, lable, isExtension)  output inserted.id VALUES (@organizationId, @cname, @lable, @isExtension)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@organizationId", ToDBValue(meetingRoomTypeColumn.OrganizationId)),
						new SqlParameter("@cname", ToDBValue(meetingRoomTypeColumn.Cname)),
						new SqlParameter("@lable", ToDBValue(meetingRoomTypeColumn.Lable)),
						new SqlParameter("@isExtension", ToDBValue(meetingRoomTypeColumn.IsExtension)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM MeetingRoomTypeColumn WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(MeetingRoomTypeColumn meetingRoomTypeColumn)
        {
            string sql =
                @"UPDATE MeetingRoomTypeColumn SET  organizationId = @organizationId 
                , cname = @cname 
                , lable = @lable 
                , isExtension = @isExtension 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", meetingRoomTypeColumn.Id)
					,new SqlParameter("@organizationId", ToDBValue(meetingRoomTypeColumn.OrganizationId))
					,new SqlParameter("@cname", ToDBValue(meetingRoomTypeColumn.Cname))
					,new SqlParameter("@lable", ToDBValue(meetingRoomTypeColumn.Lable))
					,new SqlParameter("@isExtension", ToDBValue(meetingRoomTypeColumn.IsExtension))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static MeetingRoomTypeColumn GetById(int id)
        {
            string sql = "SELECT * FROM MeetingRoomTypeColumn WHERE Id = @Id";
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
		
		public static MeetingRoomTypeColumn ToModel(SqlDataReader reader)
		{
			MeetingRoomTypeColumn meetingRoomTypeColumn = new MeetingRoomTypeColumn();

			meetingRoomTypeColumn.Id = (int)ToModelValue(reader,"id");
			meetingRoomTypeColumn.OrganizationId = (string)ToModelValue(reader,"organizationId");
			meetingRoomTypeColumn.Cname = (string)ToModelValue(reader,"cname");
			meetingRoomTypeColumn.Lable = (string)ToModelValue(reader,"lable");
			meetingRoomTypeColumn.IsExtension = (string)ToModelValue(reader,"isExtension");
			return meetingRoomTypeColumn;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM MeetingRoomTypeColumn";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<MeetingRoomTypeColumn> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingRoomTypeColumn ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<MeetingRoomTypeColumn> GetAll()
		{
			string sql = "SELECT * FROM MeetingRoomTypeColumn";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<MeetingRoomTypeColumn> ToModels(SqlDataReader reader)
		{
			var list = new List<MeetingRoomTypeColumn>();
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

		//---------------------------------以下非自动生成-----------------------------------------
		public static MeetingRoomTypeColumn GetByOrgId(String organizationId)
		{
			string sql = "SELECT * FROM MeetingRoomTypeColumn WHERE organizationId = @IorganizationIdd";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@organizationId", organizationId)))
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
		public static int AddDefaultData(String organizationId)
		{
			string sql = string.Format("INSERT INTO MeetingRoomTypeColumn (organizationId, cname, lable, isExtension)  output inserted.id VALUES ('{0}', 'id', 'ID主键', 'N');",organizationId);
			sql += string.Format("INSERT INTO MeetingRoomTypeColumn (organizationId, cname, lable, isExtension)  output inserted.id VALUES ('{0}', 'RoomTypeId', '会议室类型编号', 'N');", organizationId);
			sql += string.Format("INSERT INTO MeetingRoomTypeColumn (organizationId, cname, lable, isExtension)  output inserted.id VALUES ('{0}', 'name', '会议室类型名称', 'N');", organizationId);
			sql += string.Format("INSERT INTO MeetingRoomTypeColumn (organizationId, cname, lable, isExtension)  output inserted.id VALUES ('{0}', 'introduction', '会议室类型介绍', 'N');", organizationId);
			sql += string.Format("INSERT INTO MeetingRoomTypeColumn (organizationId, cname, lable, isExtension)  output inserted.id VALUES ('{0}', 'organizationId', '组织编号', 'N');", organizationId);
			sql += string.Format("INSERT INTO MeetingRoomTypeColumn (organizationId, cname, lable, isExtension)  output inserted.id VALUES ('{0}', 'remark', '备注信息', 'N');", organizationId);
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text);
		}
	}
}
