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
    public partial class MeetingRoomTypeExtInfoDAL
    {
        public static object Insert(MeetingRoomTypeExtInfo meetingRoomTypeExtInfo)
        {
            string sql = "INSERT INTO MeetingRoomTypeExtInfo (organizationId, useEC, isUseET)  output inserted.id VALUES (@organizationId, @useEC, @isUseET)";
            SqlParameter[] para = new SqlParameter[]
                {
                        new SqlParameter("@organizationId", ToDBValue(meetingRoomTypeExtInfo.OrganizationId)),
                        new SqlParameter("@useEC", ToDBValue(meetingRoomTypeExtInfo.UseEC)),
                        new SqlParameter("@isUseET", ToDBValue(meetingRoomTypeExtInfo.IsUseET)),
                };

            return SqlHelper.ExecuteScalar(sql, CommandType.Text, para);
        }

        public static int DeleteById(int id)
        {
            string sql = "DELETE FROM MeetingRoomTypeExtInfo WHERE Id = @Id";

            SqlParameter[] para = new SqlParameter[]
             {
                new SqlParameter("@id", id)
             };

            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }


        public static int Update(MeetingRoomTypeExtInfo meetingRoomTypeExtInfo)
        {
            string sql =
                @"UPDATE MeetingRoomTypeExtInfo SET  organizationId = @organizationId 
                , useEC = @useEC 
                , isUseET = @isUseET 
                 WHERE id = @id";

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@id", meetingRoomTypeExtInfo.Id)
                    ,new SqlParameter("@organizationId", ToDBValue(meetingRoomTypeExtInfo.OrganizationId))
                    ,new SqlParameter("@useEC", ToDBValue(meetingRoomTypeExtInfo.UseEC))
                    ,new SqlParameter("@isUseET", ToDBValue(meetingRoomTypeExtInfo.IsUseET))
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        public static MeetingRoomTypeExtInfo GetById(int id)
        {
            string sql = "SELECT * FROM MeetingRoomTypeExtInfo WHERE Id = @Id";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
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

        public static MeetingRoomTypeExtInfo ToModel(SqlDataReader reader)
        {
            MeetingRoomTypeExtInfo meetingRoomTypeExtInfo = new MeetingRoomTypeExtInfo();

            meetingRoomTypeExtInfo.Id = (int)ToModelValue(reader, "id");
            meetingRoomTypeExtInfo.OrganizationId = (string)ToModelValue(reader, "organizationId");
            meetingRoomTypeExtInfo.UseEC = (int?)ToModelValue(reader, "useEC");
            meetingRoomTypeExtInfo.IsUseET = (string)ToModelValue(reader, "isUseET");
            return meetingRoomTypeExtInfo;
        }

        public static int GetTotalCount()
        {
            string sql = "SELECT count(*) FROM MeetingRoomTypeExtInfo";
            return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
        }

        public static List<MeetingRoomTypeExtInfo> GetPagedData(int startIndex, int endIndex)
        {
            string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingRoomTypeExtInfo ) t where rownum>=@startIndex and rownum<=@endIndex";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
                new SqlParameter("@startIndex", startIndex),
                new SqlParameter("@endIndex", endIndex)))
            {
                return ToModels(reader);
            }
        }

        public static List<MeetingRoomTypeExtInfo> GetAll()
        {
            string sql = "SELECT * FROM MeetingRoomTypeExtInfo";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
            {
                return ToModels(reader);
            }
        }

        protected static List<MeetingRoomTypeExtInfo> ToModels(SqlDataReader reader)
        {
            var list = new List<MeetingRoomTypeExtInfo>();
            while (reader.Read())
            {
                list.Add(ToModel(reader));
            }
            return list;
        }

        protected static object ToDBValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        protected static object ToModelValue(SqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return null;
            }
            else
            {
                return reader[columnName];
            }
        }

        //---------------------------以下非自动生成-------------------------------
        public static MeetingRoomTypeExtInfo GetByOrgId(String orgId)
        {
            string sql = "SELECT * FROM MeetingRoomTypeExtInfo WHERE organizationId = @orgId";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@orgId", orgId)))
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
            string sql = "INSERT INTO MeetingRoomTypeExtInfo (organizationId, useEC, isUseET)  output inserted.id VALUES (@organizationId, @useEC, @isUseET)";

            SqlParameter[] para = new SqlParameter[]
             {
                new SqlParameter("@organizationId", ToDBValue(organizationId)),
                        new SqlParameter("@useEC", ToDBValue(0)),
                        new SqlParameter("@isUseET", ToDBValue("N"))
             };

            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }
    }
}
