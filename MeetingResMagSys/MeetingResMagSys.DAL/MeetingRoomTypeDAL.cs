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
    public partial class MeetingRoomTypeDAL
    {
        public static object Insert(MeetingRoomType meetingRoomType)
        {
            string sql = "INSERT INTO MeetingRoomType (RoomTypeId, name, introduction, organizationId, remark, c1, c2)  output inserted.id VALUES (@RoomTypeId, @name, @introduction, @organizationId, @remark, @c1, @c2)";
            SqlParameter[] para = new SqlParameter[]
                {
                        new SqlParameter("@RoomTypeId", ToDBValue(meetingRoomType.RoomTypeId)),
                        new SqlParameter("@name", ToDBValue(meetingRoomType.Name)),
                        new SqlParameter("@introduction", ToDBValue(meetingRoomType.Introduction)),
                        new SqlParameter("@organizationId", ToDBValue(meetingRoomType.OrganizationId)),
                        new SqlParameter("@remark", ToDBValue(meetingRoomType.Remark)),
                        new SqlParameter("@c1", ToDBValue(meetingRoomType.C1)),
                        new SqlParameter("@c2", ToDBValue(meetingRoomType.C2)),
                };

            return SqlHelper.ExecuteScalar(sql, CommandType.Text, para);
        }

        public static int DeleteById(int id)
        {
            string sql = "DELETE FROM MeetingRoomType WHERE Id = @Id";

            SqlParameter[] para = new SqlParameter[]
             {
                new SqlParameter("@id", id)
             };

            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }


        public static int Update(MeetingRoomType meetingRoomType)
        {
            string sql =
                @"UPDATE MeetingRoomType SET  RoomTypeId = @RoomTypeId 
                , name = @name 
                , introduction = @introduction 
                , organizationId = @organizationId 
                , remark = @remark 
                , c1 = @c1 
                , c2 = @c2 
                 WHERE id = @id";

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@id", meetingRoomType.Id)
                    ,new SqlParameter("@RoomTypeId", ToDBValue(meetingRoomType.RoomTypeId))
                    ,new SqlParameter("@name", ToDBValue(meetingRoomType.Name))
                    ,new SqlParameter("@introduction", ToDBValue(meetingRoomType.Introduction))
                    ,new SqlParameter("@organizationId", ToDBValue(meetingRoomType.OrganizationId))
                    ,new SqlParameter("@remark", ToDBValue(meetingRoomType.Remark))
                    ,new SqlParameter("@c1", ToDBValue(meetingRoomType.C1))
                    ,new SqlParameter("@c2", ToDBValue(meetingRoomType.C2))
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        public static MeetingRoomType GetById(int id)
        {
            string sql = "SELECT * FROM MeetingRoomType WHERE Id = @Id";
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

        public static MeetingRoomType ToModel(SqlDataReader reader)
        {
            MeetingRoomType meetingRoomType = new MeetingRoomType();

            meetingRoomType.Id = (int)ToModelValue(reader, "id");
            meetingRoomType.RoomTypeId = (string)ToModelValue(reader, "RoomTypeId");
            meetingRoomType.Name = (string)ToModelValue(reader, "name");
            meetingRoomType.Introduction = (string)ToModelValue(reader, "introduction");
            meetingRoomType.OrganizationId = (string)ToModelValue(reader, "organizationId");
            meetingRoomType.Remark = (string)ToModelValue(reader, "remark");
            meetingRoomType.C1 = (string)ToModelValue(reader, "c1");
            meetingRoomType.C2 = (string)ToModelValue(reader, "c2");
            return meetingRoomType;
        }

        public static int GetTotalCount()
        {
            string sql = "SELECT count(*) FROM MeetingRoomType";
            return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
        }

        public static List<MeetingRoomType> GetPagedData(int startIndex, int endIndex)
        {
            string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM MeetingRoomType ) t where rownum>=@startIndex and rownum<=@endIndex";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
                new SqlParameter("@startIndex", startIndex),
                new SqlParameter("@endIndex", endIndex)))
            {
                return ToModels(reader);
            }
        }

        public static List<MeetingRoomType> GetAll()
        {
            string sql = "SELECT * FROM MeetingRoomType";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
            {
                return ToModels(reader);
            }
        }

        protected static List<MeetingRoomType> ToModels(SqlDataReader reader)
        {
            var list = new List<MeetingRoomType>();
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

        //-----------------------------------------以下为非自动生成-----------------------------------------
        public static MeetingRoomType GetMaxRoomTypeId()
        {
            string sql = "select top(1)* from MeetingRoomType order by id desc";
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
        public static string CreateRoomTypeId()
        {
            MeetingRoomType model = GetMaxRoomTypeId();
            StringBuilder sb = new StringBuilder();
            sb.Append("RTID");
            sb.Append(DateTime.Now.ToString("yyyyMMdd"));
            if (model == null)
            {
                sb.Append("0001");
                return sb.ToString();
            }
            else
            {
                int newid = int.Parse(model.RoomTypeId.Substring(model.RoomTypeId.Length - 4)) + 1;
                string maxid = newid.ToString().PadLeft(4, '0');
                sb.Append(maxid);
                return sb.ToString();
            }
        }
        public static MeetingRoomType GetByRoomTypeId(string RoomTypeId)
        {
            string sql = "SELECT * FROM MeetingRoomType WHERE RoomTypeId = @RoomTypeId";
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@RoomTypeId", RoomTypeId)))
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
        public static int DeleteByRoomTypeId(string RoomTypeId)
        {
            string sql = "DELETE FROM MeetingRoomType WHERE RoomTypeId = @RoomTypeId";

            SqlParameter[] para = new SqlParameter[]
             {
                new SqlParameter("@RoomTypeId", RoomTypeId)
             };

            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }
    }
}

