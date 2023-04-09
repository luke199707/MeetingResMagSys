using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class GroupMemberBLL
    {
        public static object Insert(GroupMember groupMember)
        {
            return GroupMemberDAL.Insert(groupMember);
        }

        public static int DeleteById(int id)
        {
            return GroupMemberDAL.DeleteById(id);
        }

		public static int Update(GroupMember groupMember)
        {
            return GroupMemberDAL.Update(groupMember);
        }
        

        public static GroupMember GetById(int id)
        {
            return GroupMemberDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return GroupMemberDAL.GetTotalCount();
		}
		
		public static List<GroupMember> GetPagedData(int startIndex,int endIndex)
		{
			return GroupMemberDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<GroupMember> GetAll()
		{
			return GroupMemberDAL.GetAll();
		}
    }
}
