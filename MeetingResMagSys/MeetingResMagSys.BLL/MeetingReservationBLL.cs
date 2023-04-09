using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class MeetingReservationBLL
    {
        public static object Insert(MeetingReservation meetingReservation)
        {
            return MeetingReservationDAL.Insert(meetingReservation);
        }

        public static int DeleteById(int id)
        {
            return MeetingReservationDAL.DeleteById(id);
        }

		public static int Update(MeetingReservation meetingReservation)
        {
            return MeetingReservationDAL.Update(meetingReservation);
        }
        

        public static MeetingReservation GetById(int id)
        {
            return MeetingReservationDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return MeetingReservationDAL.GetTotalCount();
		}
		
		public static List<MeetingReservation> GetPagedData(int startIndex,int endIndex)
		{
			return MeetingReservationDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<MeetingReservation> GetAll()
		{
			return MeetingReservationDAL.GetAll();
		}
    }
}
