using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class RoleBLL
    {
        public static object Insert(Role role)
        {
            return RoleDAL.Insert(role);
        }

        public static int DeleteById(int id)
        {
            return RoleDAL.DeleteById(id);
        }

		public static int Update(Role role)
        {
            return RoleDAL.Update(role);
        }
        

        public static Role GetById(int id)
        {
            return RoleDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return RoleDAL.GetTotalCount();
		}
		
		public static List<Role> GetPagedData(int startIndex,int endIndex)
		{
			return RoleDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<Role> GetAll()
		{
			return RoleDAL.GetAll();
		}
    }
}
