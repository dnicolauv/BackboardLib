using System.Collections.Generic;
using Android.Views;
using System;
using Java.Lang;
using Android.Util;

namespace com.tumblr.backboard
{
	public class MotionProperty
	{
		/// <summary>
		/// X direction, corresponds to <seealso cref="MotionEvent#getX()"/> and maps to <seealso cref="View#TRANSLATION_X"/>.
		/// </summary>
		//public static readonly MotionProperty X = new MotionProperty("X", InnerEnum.X, View.X);
		public static readonly MotionProperty X = new MotionProperty("X", InnerEnum.X, Property.Of(Class.FromType(typeof(View)), Class.FromType(typeof(Float)), "TranslationX"));
		/// <summary>
		/// Y direction, corresponds to <seealso cref="MotionEvent#getY()"/> and maps to <seealso cref="View#TRANSLATION_Y"/>.
		/// </summary>
		//public static readonly MotionProperty Y = new MotionProperty("Y", InnerEnum.Y, View.Y);
		public static readonly MotionProperty Y = new MotionProperty("Y", InnerEnum.Y, Property.Of(Class.FromType(typeof(View)), Class.FromType(typeof(Float)), "TranslationY"));

		private static readonly IList<MotionProperty> valueList = new List<MotionProperty>();

		static MotionProperty()
		{
			valueList.Add(X);
			valueList.Add(Y);
		}

		public enum InnerEnum
		{
			X,
			Y
		}

		private readonly string nameValue;
		private readonly int ordinalValue;
		private readonly InnerEnum innerEnumValue;
		private static int nextOrdinal = 0;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @NonNull private final android.util.Property<android.view.View, android.support.annotation.Nullable<float>> mViewProperty;
		private readonly Property mViewProperty;

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: private MotionProperty(@NonNull final android.util.Property<android.view.View, android.support.annotation.Nullable<float>> viewProperty)
		//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		private MotionProperty(string name, InnerEnum innerEnum, Android.Util.Property viewProperty)
		{
			mViewProperty = viewProperty;
            
			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @NonNull public android.util.Property<android.view.View, android.support.annotation.Nullable<float>> getViewProperty()
		public Android.Util.Property ViewProperty
		{
			get
			{
				return mViewProperty;
			}
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: public float getValue(@Nullable final android.view.View view)
		//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		public float GetValue(View view)
		{
			if (view != null)
			{
				return (float)mViewProperty.Get(view);
			}

			return 0;
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: public float getValue(@Nullable final android.view.MotionEvent event)
		//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		public float GetValue(MotionEvent @event)
		{
			if (@event != null)
			{
				switch (innerEnumValue)
				{
					case InnerEnum.X:
						return @event.GetX(0);
					case InnerEnum.Y:
						return @event.GetY(0);
					default:
						return @event.GetX(0);
				}
			}

			return 0;
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: public float getHistoricalValue(@Nullable final android.view.MotionEvent event, final int index)
		//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		public float GetHistoricalValue(MotionEvent @event, int index)
		{
			if (@event != null)
			{
				switch (innerEnumValue)
				{
					case InnerEnum.X:
						return @event.GetHistoricalX(index);
					case InnerEnum.Y:
						return @event.GetHistoricalY(index);
					default:
						return 0;
				}
			}

			return 0;
		}

		//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		//ORIGINAL LINE: public float getOldestValue(final android.view.MotionEvent event)
		public float GetOldestValue(MotionEvent @event)
		{
			return GetHistoricalValue(@event, 0);
		}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: public float getOffset(@Nullable final android.view.View view)
		//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		public float GetOffset(View view)
		{
			if (view != null)
			{
				switch (innerEnumValue)
				{
					case InnerEnum.X:
						return -view.Width / 2;
					case InnerEnum.Y:
						return -view.Height / 2;
					default:
						return -view.Width / 2;
				}
			}

			return 0;
		}

		public static IList<MotionProperty> Values()
		{
			return valueList;
		}

		public InnerEnum InnerEnumValue()
		{
			return innerEnumValue;
		}

		public int Ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static MotionProperty ValueOf(string name)
		{
			foreach (MotionProperty enumInstance in MotionProperty.Values())
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}
}