
using System;
using Android.Views;
using Com.Facebook.Rebound;
/**
* Maps a {@link android.view.MotionEvent} to a {@link com.facebook.rebound.Spring},
* although it does not pick a property to map.
* <p>
* Created by ericleong on 5/30/14.
*/
namespace com.tumblr.backboard
{
	public abstract class EventImitator : Imitator
	{

		/**
		 * Constructor.
		 *
		 * @param spring
		 * 		the spring to use.
		 * @param restValue
		 * 		the rest value for the spring.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		public EventImitator(Spring spring, double restValue, int trackStrategy, int followStrategy) : base(spring, restValue, trackStrategy, followStrategy)
		{
			Release = ReleaseInternal;
		}

		/**
		 * Constructor. Note that the spring must be set with {@link #setSpring(Spring)}.
		 *
		 * @param restValue
		 * 		the rest value for the spring.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		protected EventImitator(double restValue, int trackStrategy, int followStrategy) : base(restValue, trackStrategy, followStrategy)
		{
			//super(restValue, trackStrategy, followStrategy);
			Release = ReleaseInternal;
		}

		/**
		 * Called when the user touches ({@link android.view.MotionEvent#ACTION_DOWN}).
		 *
		 * @param event
		 * 		the motion event
		 */
		public virtual void Constrain(MotionEvent ev)
		{
			if (mSpring != null && mFollowStrategy == FOLLOW_EXACT)
			{
				mSpring.SetVelocity(0);
			}
		}

		/**
		 * Called when the user moves their finger ({@link android.view.MotionEvent#ACTION_MOVE}).
		 *
		 * @param offset
		 * 		the value offset
		 * @param value
		 * 		the current value
		 * @param delta
		 * 		the change in the value
		 * @param dt
		 * 		the change in time
		 * @param event
		 * 		the motion event
		 */
		public virtual void Mime(float offset, float value, float delta, float dt, MotionEvent ev)
		{
			if (mSpring != null)
			{
				mSpring.SetEndValue(MapToSpring(offset + value));

				if (mFollowStrategy == FOLLOW_EXACT)
				{
					mSpring.SetCurrentValue(mSpring.EndValue);

					if (dt > 0)
					{
						mSpring.SetVelocity(delta / dt);
					}
				}
			}
		}

		/**
		 * Called when the user releases their finger ({@link android.view.MotionEvent#ACTION_UP}).
		 *
		 * @param event
		 * 		the motion event
		 */
		//DNV ACTION
		public Action<MotionEvent> Release { get; set; }
		public virtual void ReleaseInternal(MotionEvent ev)
		{
			if (mSpring != null)
			{
				mSpring.SetEndValue(mRestValue);
			}
		}

		/**
		 * Called by a {@link MotionImitator} (or another {@link
		 * android.view.View.OnTouchListener}) when a {@link android.view.MotionEvent} occurs.
		 *
		 * @param offset
		 * 		the value offset
		 * @param value
		 * 		the current value
		 * @param delta
		 * 		the change in the value
		 * @param event
		 * 		the motion event
		 */
		protected virtual void Imitate(float offset, float value, float delta, MotionEvent ev)
		{
			if (ev != null)
			{
				switch (ev.Action)
				{
					case MotionEventActions.Down:
						Constrain(ev);
						break;
					case MotionEventActions.Move:
						if (ev.HistorySize > 0)
						{
							Mime(offset, value, delta,
								 ev.EventTime - ev.GetHistoricalEventTime(0), ev);
						}
						else {
							Mime(offset, value, delta, 0, ev);
						}
						break;
					default:
					case MotionEventActions.Up:
						Release(ev);
						break;
				}
			}
		}

		/**
		 * Maps a user's motion to {@link android.view.View} via a {@link com.facebook.rebound.Spring}.
		 *
		 * @param view
		 * 		the view to perturb.
		 * @param event
		 * 		the motion to imitate.
		 */
		public abstract void Imitate(View view, MotionEvent ev);
	}
}
