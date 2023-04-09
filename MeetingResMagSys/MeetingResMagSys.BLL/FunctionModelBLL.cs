using System;
using System.Collections.Generic;
using System.Text;
using MeetingResMagSys.DAL;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.BLL
{

    public partial class FunctionModelBLL
    {
        public static object Insert(FunctionModel functionModel)
        {
            return FunctionModelDAL.Insert(functionModel);
        }

        public static int DeleteById(int id)
        {
            return FunctionModelDAL.DeleteById(id);
        }

		public static int Update(FunctionModel functionModel)
        {
            return FunctionModelDAL.Update(functionModel);
        }
        

        public static FunctionModel GetById(int id)
        {
            return FunctionModelDAL.GetById(id);
        }
		public static int GetTotalCount()
		{
			return FunctionModelDAL.GetTotalCount();
		}
		
		public static List<FunctionModel> GetPagedData(int startIndex,int endIndex)
		{
			return FunctionModelDAL.GetPagedData(startIndex,endIndex);
		}
		
		public static List<FunctionModel> GetAll()
		{
			return FunctionModelDAL.GetAll();
		}
    }
}
