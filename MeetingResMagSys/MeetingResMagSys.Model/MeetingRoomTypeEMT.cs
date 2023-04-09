//============================================================
// author:yangyiliang
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingResMagSys.Model
{	
	[Serializable()]
	public class MeetingRoomTypeEMT
	{	
			private int _id;
			private string _organizationId;
			private string _cname;
			private string _lable;
			private string _type;
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
			public string Cname
			{
				get {  return _cname;}
				set {  _cname = value;}
			}
			public string Lable
			{
				get {  return _lable;}
				set {  _lable = value;}
			}
			public string Type
			{
				get {  return _type;}
				set {  _type = value;}
			}
	}
}
