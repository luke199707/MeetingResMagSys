using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class MeetingRoomBLL
    {
        public static object Insert(MeetingRoom meetingRoom)
        {
            return MeetingRoomDAL.Insert(meetingRoom);
        }

        public static int DeleteById(int id)
        {
            return MeetingRoomDAL.DeleteById(id);
        }

		public static int Update(MeetingRoom meetingRoom)
        {
            return MeetingRoomDAL.Update(meetingRoom);
        }
        

        public static MeetingRoom GetById(int id)
        {
            return MeetingRoomDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return MeetingRoomDAL.GetTotalCount();
		}
		
		public static List<MeetingRoom> GetPagedData(int startIndex,int endIndex)
		{
			return MeetingRoomDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<MeetingRoom> GetAll()
		{
			return MeetingRoomDAL.GetAll();
		}
    }
}
