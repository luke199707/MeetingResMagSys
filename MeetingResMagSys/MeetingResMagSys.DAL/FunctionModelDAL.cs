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
	public partial class FunctionModelDAL
	{
        public static object Insert(FunctionModel functionModel)
		{
				string sql ="INSERT INTO FunctionModel (parentId, modelName, childId, currentId, url, css, target, time, remark)  output inserted.id VALUES (@parentId, @modelName, @childId, @currentId, @url, @css, @target, @time, @remark)";
				SqlParameter[] para = new SqlParameter[]
					{
						new SqlParameter("@parentId", ToDBValue(functionModel.ParentId)),
						new SqlParameter("@modelName", ToDBValue(functionModel.ModelName)),
						new SqlParameter("@childId", ToDBValue(functionModel.ChildId)),
						new SqlParameter("@currentId", ToDBValue(functionModel.CurrentId)),
						new SqlParameter("@url", ToDBValue(functionModel.Url)),
						new SqlParameter("@css", ToDBValue(functionModel.Css)),
						new SqlParameter("@target", ToDBValue(functionModel.Target)),
						new SqlParameter("@time", ToDBValue(functionModel.Time)),
						new SqlParameter("@remark", ToDBValue(functionModel.Remark)),
					};
					
				return SqlHelper.ExecuteScalar(sql,CommandType.Text, para);
		}

        public static int DeleteById(int id)
		{
            string sql = "DELETE FROM FunctionModel WHERE Id = @Id";

           SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", id)
			};
		
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
		}
		
				
        public static int Update(FunctionModel functionModel)
        {
            string sql =
                @"UPDATE FunctionModel SET  parentId = @parentId 
                , modelName = @modelName 
                , childId = @childId 
                , currentId = @currentId 
                , url = @url 
                , css = @css 
                , target = @target 
                , time = @time 
                , remark = @remark 
                 WHERE id = @id";

			SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@id", functionModel.Id)
					,new SqlParameter("@parentId", ToDBValue(functionModel.ParentId))
					,new SqlParameter("@modelName", ToDBValue(functionModel.ModelName))
					,new SqlParameter("@childId", ToDBValue(functionModel.ChildId))
					,new SqlParameter("@currentId", ToDBValue(functionModel.CurrentId))
					,new SqlParameter("@url", ToDBValue(functionModel.Url))
					,new SqlParameter("@css", ToDBValue(functionModel.Css))
					,new SqlParameter("@target", ToDBValue(functionModel.Target))
					,new SqlParameter("@time", ToDBValue(functionModel.Time))
					,new SqlParameter("@remark", ToDBValue(functionModel.Remark))
			};
			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text,para);
        }		
		
        public static FunctionModel GetById(int id)
        {
            string sql = "SELECT * FROM FunctionModel WHERE Id = @Id";
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
		
		public static FunctionModel ToModel(SqlDataReader reader)
		{
			FunctionModel functionModel = new FunctionModel();

			functionModel.Id = (int)ToModelValue(reader,"id");
			functionModel.ParentId = (string)ToModelValue(reader,"parentId");
			functionModel.ModelName = (string)ToModelValue(reader,"modelName");
			functionModel.ChildId = (string)ToModelValue(reader,"childId");
			functionModel.CurrentId = (string)ToModelValue(reader,"currentId");
			functionModel.Url = (string)ToModelValue(reader,"url");
			functionModel.Css = (string)ToModelValue(reader,"css");
			functionModel.Target = (string)ToModelValue(reader,"target");
			functionModel.Time = (DateTime?)ToModelValue(reader,"time");
			functionModel.Remark = (string)ToModelValue(reader,"remark");
			return functionModel;
		}
		
		public static int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM FunctionModel";
			return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text);
		}
		
		public static List<FunctionModel> GetPagedData(int startIndex,int endIndex)
		{
			string sql = "SELECT * from(SELECT *,row_number() over(order by id desc) rownum FROM FunctionModel ) t where rownum>=@startIndex and rownum<=@endIndex";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text,
				new SqlParameter("@startIndex",startIndex),
				new SqlParameter("@endIndex",endIndex)))
			{
				return ToModels(reader);					
			}
		}
		
		public static List<FunctionModel> GetAll()
		{
			string sql = "SELECT * FROM FunctionModel";
			using(SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text))
			{
				return ToModels(reader);			
			}
		}
		
		protected static List<FunctionModel> ToModels(SqlDataReader reader)
		{
			var list = new List<FunctionModel>();
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
		//----------------------------------------从下面开始自己添加，不是生成的-------------------------------------
		/// <summary>
		/// 通过用户名和角色ID，得到该用户的一级菜单
		/// 一级菜单模块代码为：RoleRight表中存储的为FunctionModel中的childId字段
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		public static List<FunctionModel> GetFirstLevel(string userName,string orgId)
		{
			string sql = "select FunctionModel.* from AllUser inner join RoleRight on RoleRight.roleName=AllUser.role and RoleRight.organizationId=AllUser.organizationId inner join FunctionModel on FunctionModel.childId = RoleRight.rightCode where AllUser.name=@name and AllUser.organizationId=@organizationId ORDER BY currentId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@name", userName),
				new SqlParameter("@organizationId",orgId)))
			{
				return ToModels(reader);
			}
		}
		/// <summary>
		/// 通过用户名，得到该用户的一级菜单
		/// 一级菜单模块代码为：RoleRight表中存储的为FunctionModel中的childId字段
		/// </summary>
		/// <param name="userName">用户名</param>
		/// <returns></returns>
		public static List<FunctionModel> GetFirstLevel(string userName)
		{
			string sql = "select FunctionModel.* from AllUser inner join RoleRight on RoleRight.roleName=AllUser.role inner join FunctionModel on FunctionModel.childId = RoleRight.rightCode where AllUser.name=@name ORDER BY currentId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@name", userName)))
			{
				return ToModels(reader);
			}
		}
		/// <summary>
		/// 根据父节点标识，加载其下方的二级节点
		/// 二级菜单模块代码为：RoleRight表中存储的为FunctionModel中的currentId字段
		/// </summary>
		/// <param name="parentId">父节点标识</param>
		/// <returns></returns>
		public static List<FunctionModel> GetByParentID(string parentId)
		{
			string sql = "select * from FunctionModel where parentId=@parentId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@parentId", parentId)))
			{
				return ToModels(reader);
			}
		}

		public static FunctionModel GetByCurrentID(string currentId)
		{
			string sql = "select * from FunctionModel where currentId=@currentId";
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, new SqlParameter("@currentId", currentId)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				return null;
			}
		}

		public static int DeleteByCurrentId(string currentId)
		{
			string sql = "DELETE FROM FunctionModel WHERE currentId = @currentId";

			SqlParameter[] para = new SqlParameter[]
			 {
				new SqlParameter("@currentId", currentId)
			 };

			return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
		}
		public static FunctionModel GetByCurrentID(string currentId, string roleid)
		{
			int roleId = Convert.ToInt32(roleid);
			string sql = "select * from FunctionModel where currentId=@currentId and roleId=@roleId";
			SqlParameter[] sp = { new SqlParameter("@currentId", currentId), new SqlParameter("@roleId", roleId) };
			using (SqlDataReader reader = SqlHelper.ExecuteDataReader(sql, CommandType.Text, sp))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				return null;
			}
		}
	}
}
