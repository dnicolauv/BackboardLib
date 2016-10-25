using System;
using System.Collections.Generic;
using Android.Views;

namespace com.tumblr.backboard
{
	public class MotionListener : Java.Lang.Object, View.IOnTouchListener
	{
		internal bool mMotionListenerEnabled;
		internal List<Actor.Motion> mMotions;
		internal View.IOnTouchListener mOnTouchListener;
		internal bool mRequestDisallowTouchEvent;
		internal int MAX_CLICK_DISTANCE;

		//@SuppressLint("ClickableViewAccessibility")
		public bool OnTouch(View v, MotionEvent ev)
		{
			
			bool retVal;

			//if (!mMotionListenerEnabled || mMotions.Count 
			//    < 0 )
			//{

			//	if (mOnTouchListener != null)
			//	{
			//		retVal = mOnTouchListener.OnTouch(v, ev);
			//	}
			//	else {
			//		retVal = false;
			//	}

			//	return retVal;
			//}

			foreach (Actor.Motion motion in mMotions)
			{
				foreach (EventImitator imitator in motion.imitators)
				{
					imitator.Imitate(v, ev);
				}
			}

			if (mOnTouchListener != null)
			{
				retVal = mOnTouchListener.OnTouch(v, ev);
			}
			else {
				retVal = true;
			}

			if (mRequestDisallowTouchEvent)
			{
				// prevents parent from scrolling or otherwise stealing touch events
				v.Parent.RequestDisallowInterceptTouchEvent(true);
			}

			if (v.Clickable)
			{
				if (ev.EventTime - ev.DownTime > ViewConfiguration.LongPressTimeout) {
					v.Pressed = false;

				return true;
				}

				if (ev.HistorySize > 0) {
					float deltaX = ev.GetHistoricalSize(ev.HistorySize - 1) - ev.GetX();
				    float deltaY = ev.GetHistoricalSize(ev.HistorySize - 1) - ev.GetY();

					// if user has moved too far, it is no longer a click
					bool removeClickState = Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)	> Math.Pow(MAX_CLICK_DISTANCE, 2);

					v.Pressed = !removeClickState;

					return removeClickState;
				} else {
					return false;
				}
			}

			return retVal;
		}
	}
}
