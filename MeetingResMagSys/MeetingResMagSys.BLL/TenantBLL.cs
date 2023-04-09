using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class TenantBLL
    {
        public static object Insert(Tenant tenant)
        {
            return TenantDAL.Insert(tenant);
        }

        public static int DeleteById(int id)
        {
            return TenantDAL.DeleteById(id);
        }

		public static int Update(Tenant tenant)
        {
            return TenantDAL.Update(tenant);
        }
        

        public static Tenant GetById(int id)
        {
            return TenantDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return TenantDAL.GetTotalCount();
		}
		
		public static List<Tenant> GetPagedData(int startIndex,int endIndex)
		{
			return TenantDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<Tenant> GetAll()
		{
			return TenantDAL.GetAll();
		}
    }
}
