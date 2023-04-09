//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingRoomFacility
	{	
			private int _id;
			private string _roomId;
			private string _facilityId;
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
			public string FacilityId
			{
				get {  return _facilityId;}
				set {  _facilityId = value;}
			}
	}
}
