using System;
using Android.Util;
using Android.Views;
namespace com.tumblr.backboard
{
	public static class ViewHelper
	{
		private static Android.Util.Property GetViewStaticProperty(String name)
		{
			Property prop = Property.Of(Java.Lang.Class.FromType(typeof(View)), Java.Lang.Class.FromType(typeof(Java.Lang.Float)), name);
			return prop;
		}

		public static Android.Util.Property TranslationX
		{
			get { return GetViewStaticProperty("TranslationX"); }
		}

		public static Android.Util.Property TranslationY
		{
			get { return GetViewStaticProperty("TranslationY"); }
		}

		public static Android.Util.Property Alpha
		{
			get { return GetViewStaticProperty("Alpha"); }
		}

        //Test comment from vs
	}
}
