//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingRoomTypeExtInfo
	{	
			private int _id;
			private string _organizationId;
			private int? _useEC;
			private string _isUseET;
			public int Id
			{
				get {  return _id;}
				set {  _id = value;}
			}
			public string OrganizationId
			{
				get {  return _organizationId;}
				set {  _organizationId = value;}
			}
			public int? UseEC
			{
				get {  return _useEC;}
				set {  _useEC = value;}
			}
			public string IsUseET
			{
				get {  return _isUseET;}
				set {  _isUseET = value;}
			}
	}
}
