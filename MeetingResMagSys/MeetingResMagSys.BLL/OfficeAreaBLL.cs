using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class OfficeAreaBLL
    {
        public static object Insert(OfficeArea officeArea)
        {
            return OfficeAreaDAL.Insert(officeArea);
        }

        public static int DeleteById(int id)
        {
            return OfficeAreaDAL.DeleteById(id);
        }

		public static int Update(OfficeArea officeArea)
        {
            return OfficeAreaDAL.Update(officeArea);
        }
        

        public static OfficeArea GetById(int id)
        {
            return OfficeAreaDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return OfficeAreaDAL.GetTotalCount();
		}
		
		public static List<OfficeArea> GetPagedData(int startIndex,int endIndex)
		{
			return OfficeAreaDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<OfficeArea> GetAll()
		{
			return OfficeAreaDAL.GetAll();
		}
    }
}
