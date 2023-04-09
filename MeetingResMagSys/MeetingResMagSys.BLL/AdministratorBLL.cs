using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class AdministratorBLL
    {
        public static object Insert(Administrator administrator)
        {
            return AdministratorDAL.Insert(administrator);
        }

        public static int DeleteById(int id)
        {
            return AdministratorDAL.DeleteById(id);
        }

		public static int Update(Administrator administrator)
        {
            return AdministratorDAL.Update(administrator);
        }
        

        public static Administrator GetById(int id)
        {
            return AdministratorDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return AdministratorDAL.GetTotalCount();
		}
		
		public static List<Administrator> GetPagedData(int startIndex,int endIndex)
		{
			return AdministratorDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<Administrator> GetAll()
		{
			return AdministratorDAL.GetAll();
		}
    }
}
