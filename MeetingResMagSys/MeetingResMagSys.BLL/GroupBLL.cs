using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class GroupBLL
    {
        public static object Insert(Group group)
        {
            return GroupDAL.Insert(group);
        }

        public static int DeleteById(int id)
        {
            return GroupDAL.DeleteById(id);
        }

		public static int Update(Group group)
        {
            return GroupDAL.Update(group);
        }
        

        public static Group GetById(int id)
        {
            return GroupDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return GroupDAL.GetTotalCount();
		}
		
		public static List<Group> GetPagedData(int startIndex,int endIndex)
		{
			return GroupDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<Group> GetAll()
		{
			return GroupDAL.GetAll();
		}
    }
}
