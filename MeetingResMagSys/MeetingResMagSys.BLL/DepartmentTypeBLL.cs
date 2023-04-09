using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class DepartmentTypeBLL
    {
        public static object Insert(DepartmentType departmentType)
        {
            return DepartmentTypeDAL.Insert(departmentType);
        }

        public static int DeleteById(int id)
        {
            return DepartmentTypeDAL.DeleteById(id);
        }

		public static int Update(DepartmentType departmentType)
        {
            return DepartmentTypeDAL.Update(departmentType);
        }
        

        public static DepartmentType GetById(int id)
        {
            return DepartmentTypeDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return DepartmentTypeDAL.GetTotalCount();
		}
		
		public static List<DepartmentType> GetPagedData(int startIndex,int endIndex)
		{
			return DepartmentTypeDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<DepartmentType> GetAll()
		{
			return DepartmentTypeDAL.GetAll();
		}
    }
}
