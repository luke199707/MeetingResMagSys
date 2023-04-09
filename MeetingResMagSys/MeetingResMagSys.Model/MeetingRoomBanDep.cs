//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingRoomBanDep
	{	
			private int _id;
			private string _roomId;
			private string _departmentId;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string RoomId
			{
				get {  return _roomId;}
				set {  _roomId = value;}
			}
			public string DepartmentId
			{
				get {  return _departmentId;}
				set {  _departmentId = value;}
			}
	}
}
