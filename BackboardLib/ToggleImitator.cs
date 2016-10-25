
using System;
using Android.Views;
using Com.Facebook.Rebound;
/**
* Toggle between two {@link com.facebook.rebound.Spring} states depending on whether or not the user is touching the
* screen. When the user presses, {@link #constrain(android.view.MotionEvent)} is called and the active value is set.
* When the user releases, {@link #release(android.view.MotionEvent)} is called and the rest value is set.
* <p>
* Created by ericleong on 5/21/14.
*/
namespace com.tumblr.backboard
{
	public class ToggleImitator : EventImitator, View.IOnTouchListener
	{

		private double mActiveValue;

	/*	public IntPtr Handle
		{
			get
			{
				return this.Handle;
			}
		}*/

		/**
		 * Constructor. It is necessary to call {@link #setSpring(Spring)} to set the spring.
		 *
		 * @param spring
		 * 		the spring to use.
		 * @param restValue
		 * 		the value when off.
		 * @param activeValue
		 * 		the value when on.
		 */
		public ToggleImitator(Spring spring, double restValue, double activeValue) : base(spring, restValue, TRACK_ABSOLUTE, FOLLOW_EXACT)
		{
			//super(spring, restValue, TRACK_ABSOLUTE, FOLLOW_EXACT);
			mActiveValue = activeValue;
		}


		public override void Constrain(MotionEvent ev)
		{
			mSpring.SetEndValue(mActiveValue);
		}


		protected override double MapToSpring(float motionValue)
		{
			// not used
			return mActiveValue;
		}


		public bool OnTouch(View v, MotionEvent ev)
		{
			Imitate(v, ev);

			return true;
		}

		/*@Override
		@SuppressLint("ClickableViewAccessibility")*/
		public override void Imitate(View view, MotionEvent ev)
		{
			switch (ev.Action)
			{
				case MotionEventActions.Down:
					Constrain(ev);
					break;

				case MotionEventActions.Cancel:
				case MotionEventActions.Up:
					Release(ev);
					break;

				default:
					break;
			}
		}


		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}
}
