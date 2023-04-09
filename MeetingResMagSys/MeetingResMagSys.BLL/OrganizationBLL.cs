using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class OrganizationBLL
    {
        public static object Insert(Organization organization)
        {
            return OrganizationDAL.Insert(organization);
        }

        public static int DeleteById(int id)
        {
            return OrganizationDAL.DeleteById(id);
        }

		public static int Update(Organization organization)
        {
            return OrganizationDAL.Update(organization);
        }
        

        public static Organization GetById(int id)
        {
            return OrganizationDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return OrganizationDAL.GetTotalCount();
		}
		
		public static List<Organization> GetPagedData(int startIndex,int endIndex)
		{
			return OrganizationDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<Organization> GetAll()
		{
			return OrganizationDAL.GetAll();
		}
    }
}
