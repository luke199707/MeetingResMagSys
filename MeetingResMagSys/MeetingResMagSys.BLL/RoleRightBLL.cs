using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class RoleRightBLL
    {
        public static object Insert(RoleRight roleRight)
        {
            return RoleRightDAL.Insert(roleRight);
        }

        public static int DeleteById(int id)
        {
            return RoleRightDAL.DeleteById(id);
        }

		public static int Update(RoleRight roleRight)
        {
            return RoleRightDAL.Update(roleRight);
        }
        

        public static RoleRight GetById(int id)
        {
            return RoleRightDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return RoleRightDAL.GetTotalCount();
		}
		
		public static List<RoleRight> GetPagedData(int startIndex,int endIndex)
		{
			return RoleRightDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<RoleRight> GetAll()
		{
			return RoleRightDAL.GetAll();
		}
    }
}
