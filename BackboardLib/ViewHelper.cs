using System;
using Android.Util;
using Android.Views;
namespace com.tumblr.backboard
{
	public static class ViewHelper
	{
		public static Android.Util.Property GetViewStaticProperty(this View view, String name)
		{
			Property prop = Property.Of(Java.Lang.Class.FromType(typeof(View)), Java.Lang.Class.FromType(typeof(Java.Lang.Float)), name);
			return prop;
		}
	}
}
