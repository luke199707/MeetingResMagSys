using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class MeetingMemberBLL
    {
        public static object Insert(MeetingMember meetingMember)
        {
            return MeetingMemberDAL.Insert(meetingMember);
        }

        public static int DeleteById(int id)
        {
            return MeetingMemberDAL.DeleteById(id);
        }

		public static int Update(MeetingMember meetingMember)
        {
            return MeetingMemberDAL.Update(meetingMember);
        }
        

        public static MeetingMember GetById(int id)
        {
            return MeetingMemberDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return MeetingMemberDAL.GetTotalCount();
		}
		
		public static List<MeetingMember> GetPagedData(int startIndex,int endIndex)
		{
			return MeetingMemberDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<MeetingMember> GetAll()
		{
			return MeetingMemberDAL.GetAll();
		}
    }
}
