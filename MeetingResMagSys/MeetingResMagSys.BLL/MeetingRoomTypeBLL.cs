using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class MeetingRoomTypeBLL
    {
        public static object Insert(MeetingRoomType meetingRoomType)
        {
            return MeetingRoomTypeDAL.Insert(meetingRoomType);
        }

        public static int DeleteById(int id)
        {
            return MeetingRoomTypeDAL.DeleteById(id);
        }

		public static int Update(MeetingRoomType meetingRoomType)
        {
            return MeetingRoomTypeDAL.Update(meetingRoomType);
        }
        

        public static MeetingRoomType GetById(int id)
        {
            return MeetingRoomTypeDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return MeetingRoomTypeDAL.GetTotalCount();
		}
		
		public static List<MeetingRoomType> GetPagedData(int startIndex,int endIndex)
		{
			return MeetingRoomTypeDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<MeetingRoomType> GetAll()
		{
			return MeetingRoomTypeDAL.GetAll();
		}
    }
}
