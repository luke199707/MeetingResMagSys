using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class UserBLL
    {
        public static object Insert(User user)
        {
            return UserDAL.Insert(user);
        }

        public static int DeleteById(int id)
        {
            return UserDAL.DeleteById(id);
        }

		public static int Update(User user)
        {
            return UserDAL.Update(user);
        }
        

        public static User GetById(int id)
        {
            return UserDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return UserDAL.GetTotalCount();
		}
		
		public static List<User> GetPagedData(int startIndex,int endIndex)
		{
			return UserDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<User> GetAll()
		{
			return UserDAL.GetAll();
		}
    }
}
