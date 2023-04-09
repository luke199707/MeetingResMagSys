//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MeetingResMagSys.Model;

namespace MeetingResMagSys.DAL
{
	public partial class DepartmentDAL
	{
        public static object Insert(Department department)
		{
				string sql ="INSERT INTO Department (departmentId, name, introduction, superiorDepartment, supervisor, officeArea, type, Email, organizationId, organizationName, time, remark)  output inserted.id VALUES (@departmentId, @name, @introduction, @superiorDepartment, @supervisor, @officeArea, @type, @Email, @organizationId, @organizationName, @time, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@departmentId", ToDBValue(department.DepartmentId)),
						new SqlParameter("@name", ToDBValue(department.Name)),
						new SqlParameter("@introduction", ToDBValue(department.Introduction)),
						new SqlParameter("@superiorDepartment", ToDBValue(department.SuperiorDepartment)),
						new SqlParameter("@supervisor", ToDBValue(department.Supervisor)),
						new SqlParameter("@officeArea", ToDBValue(department.OfficeArea)),
						new SqlParameter("@type", ToDBValue(department.Type)),
						new SqlParameter("@Email", ToDBValue(department.Email)),
						new SqlParameter("@organizationId", ToDBValue(department.OrganizationId)),
						new SqlParameter("@organizationName", ToDBValue(department.OrganizationName)),
						new SqlParameter("@time", ToDBValue(department.Time)),
						new SqlParameter("@remark", ToDBValue(department.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM Department WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(Department department)
        {
            string sql =
                @"UPDATE Department SET  departmentId = @departmentId 
                , name = @name 
                , introduction = @introduction 
                , superiorDepartment = @superiorDepartment 
                , supervisor = @supervisor 
                , officeArea = @officeArea 
                , type = @type 
                , Email = @Email 
                , organizationId = @organizationId 
                , organizationName = @organizationName 
                , time = @time 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", department.Id)
					,new SqlParameter("@departmentId", ToDBValue(department.DepartmentId))
					,new SqlParameter("@name", ToDBValue(department.Name))
					,new SqlParameter("@introduction", ToDBValue(department.Introduction))
					,new SqlParameter("@superiorDepartment", ToDBValue(department.SuperiorDepartment))
					,new SqlParameter("@supervisor", ToDBValue(department.Supervisor))
					,new SqlParameter("@officeArea", ToDBValue(department.OfficeArea))
					,new SqlParameter("@type", ToDBValue(department.Type))
					,new SqlParameter("@Email", ToDBValue(department.Email))
					,new SqlParameter("@organizationId", ToDBValue(department.OrganizationId))
					,new SqlParameter("@organizationName", ToDBValue(department.OrganizationName))
					,new SqlParameter("@time", ToDBValue(department.Time))
					,new SqlParameter("@remark", ToDBValue(department.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static Department GetById(int id)
        {
            string sql = "SELECT * FROM Department WHERE Id = @Id";
            using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
       		}
        }
		
		public static Department ToModel(SqlDataReader reader)
		{
			Department department = new Department();

			department.Id = (int)ToModelValue(reader,"id");
			department.DepartmentId = (string)ToModelValue(reader,"departmentId");
			department.Name = (string)ToModelValue(reader,"name");
			department.Introduction = (string)ToModelValue(reader,"introduction");
			department.SuperiorDepartment = (string)ToModelValue(reader,"superiorDepartment");
			department.Supervisor = (string)ToModelValue(reader,"supervisor");
			department.OfficeArea = (string)ToModelValue(reader,"officeArea");
			department.Type = (string)ToModelValue(reader,"type");
			department.Email = (string)ToModelValue(reader,"Email");
			department.OrganizationId = (string)ToModelValue(reader,"organizationId");
			department.OrganizationName = (string)ToModelValue(reader,"organizationName");
			department.Time = (DateTime?)ToModelValue(reader,"time");
			department.Remark = (string)ToModelValue(reader,"remark");
			return department;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM Department";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<Department> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM Department ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<Department> GetAll()
		{
			string sql = "SELECT * FROM Department";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<Department> ToModels(SqlDataReader reader)
		{
			var list = new List<Department>();
			while(reader.Read())
			{
				list.Add(ToModel(reader));
			}	
			return list;
		}		
		
		protected static object ToDBValue(object value)
		{
			if(value==null)
			{
				return DBNull.Value;
			}
			else
			{
				return value;
			}
		}
		
		protected static object ToModelValue(SqlDataReader reader,string columnName)
		{
			if(reader.IsDBNull(reader.GetOrdinal(columnName)))
			{
				return null;
			}
			else
			{
				return reader[columnName];
			}
		}
		//-----------------------------------------以下为非自动生成-----------------------------------------
		public static Department GetMaxDepartmentId()
		{
			string sql = "select top(1)* from Department order by id desc";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
			}
		}
		public static string CreateDepartmentId()
		{
			Department model = GetMaxDepartmentId();
			StringBuilder sb = new StringBuilder();
			sb.Append("DEP");
			sb.Append(DateTime.Now.ToString("yyyyMMdd"));
			if (model == null)
			{
				sb.Append("000001");
				return sb.ToString();
			}
			else
			{
				int newid = int.Parse(model.DepartmentId.Substring(model.DepartmentId.Length - 6)) + 1;
				string maxid = newid.ToString().PadLeft(6, '0');
				sb.Append(maxid);
				return sb.ToString();
			}
		}
		public static Department GetByDepartmentId(string departmentId)
		{
			string sql = "SELECT * FROM Department WHERE departmentId = @departmentId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@departmentId", departmentId)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
			}
		}
		public static int DeleteByDepartmentIdId(string departmentId)
		{
			string sql = "DELETE FROM Department WHERE departmentId = @departmentId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@departmentId", departmentId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
	}
}
