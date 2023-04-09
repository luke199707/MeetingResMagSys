using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class DepartmentBLL
    {
        public static object Insert(Department department)
        {
            return DepartmentDAL.Insert(department);
        }

        public static int DeleteById(int id)
        {
            return DepartmentDAL.DeleteById(id);
        }

		public static int Update(Department department)
        {
            return DepartmentDAL.Update(department);
        }
        

        public static Department GetById(int id)
        {
            return DepartmentDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return DepartmentDAL.GetTotalCount();
		}
		
		public static List<Department> GetPagedData(int startIndex,int endIndex)
		{
			return DepartmentDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<Department> GetAll()
		{
			return DepartmentDAL.GetAll();
		}
    }
}
