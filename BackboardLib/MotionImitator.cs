
using Android.Views;
using Com.Facebook.Rebound;
/**
* Maps a user's motion to a {@link android.view.View} via a {@link com.facebook.rebound.Spring}.
* <p>
* Created by ericleong on 5/13/14.
*/
namespace com.tumblr.backboard
{
	public class MotionImitator : EventImitator
	{

		//private static string TAG = MotionImitator..class.getSimpleName();
		private static string TAG = "MotionImitator";


		/**
		 * The motion property to imitate.
		 */

		protected MotionProperty mProperty;

		/**
		 * Used internally to keep track of the initial down position.
		 */
		protected float mDownPosition;

		/**
		 * The offset between the view left/right location and the desired "center" of the view.
		 */
		protected float mOffset;

		/**
		 * Constructor. Uses {@link #TRACK_ABSOLUTE} and {@link #FOLLOW_EXACT}.
		 *
		 * @param property
		 * 		the property to track.
		 */
		public MotionImitator(MotionProperty property) : base(0, TRACK_ABSOLUTE, FOLLOW_EXACT)
		{
			this.mSpring = null;
			this.mProperty = property;
			//Release = ReleaseInternal;
		}

		/**
		 * Constructor. It is necessary to call {@link #setSpring(Spring)} to set the spring.
		 *
		 * @param property
		 * 		the property to track.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		public MotionImitator(MotionProperty property, int trackStrategy, int followStrategy) : base(0, trackStrategy, followStrategy)
		{
			//this(null, property, 0, trackStrategy, followStrategy);
			this.mSpring = null;
			this.mProperty = property;
			//Release = ReleaseInternal;
		}

		/**
		 * Constructor. Uses {@link #TRACK_ABSOLUTE} and {@link #FOLLOW_EXACT}.
		 *
		 * @param spring
		 * 		the spring to use.
		 * @param property
		 * 		the property to track.
		 */
		public MotionImitator(Spring spring, MotionProperty property) : base(spring.EndValue, TRACK_ABSOLUTE, FOLLOW_EXACT)
		{
			//this(spring, property, spring.EndValue, TRACK_ABSOLUTE, FOLLOW_EXACT);
			mProperty = property;
			//Release = ReleaseInternal;
		}

		/**
		 * Constructor. Uses {@link #TRACK_ABSOLUTE} and {@link #FOLLOW_EXACT}.
		 *
		 * @param spring
		 * 		the spring to use.
		 * @param property
		 * 		the property to track.
		 * @param restValue
		 * 		the rest value for the spring.
		 */
		public MotionImitator(Spring spring, MotionProperty property, double restValue) : base(spring, restValue, TRACK_ABSOLUTE, FOLLOW_EXACT)
		{
			//this(spring, property, restValue, TRACK_ABSOLUTE, FOLLOW_EXACT);
			mProperty = property;
			//Release = ReleaseInternal;
		}

		/**
		 * Constructor.
		 *
		 * @param spring
		 * 		the spring to use.
		 * @param property
		 * 		the property to track.
		 * @param restValue
		 * 		the rest value for the spring.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		public MotionImitator(Spring spring, MotionProperty property, double restValue, int trackStrategy, int followStrategy) : base(spring, restValue, trackStrategy, followStrategy)
		{
			//super(spring, restValue, trackStrategy, followStrategy);
			mProperty = property;
			//Release = ReleaseInternal;
		}

		/**
		 * Constructor.
		 *
		 * @param property
		 * 		the property to track.
		 * @param restValue
		 * 		the rest value for the spring.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		public MotionImitator(MotionProperty property, double restValue, int trackStrategy,
							  int followStrategy) : base(restValue, trackStrategy, followStrategy)
		{
			//super(restValue, trackStrategy, followStrategy);
			mProperty = property;
			//Release = ReleaseInternal;
		}

		public void SetRestValue(double restValue)
		{
			this.mRestValue = restValue;
		}

		/**
		 * Puts the spring to rest.
		 *
		 * @return this object for chaining.
		 */
		public MotionImitator Rest()
		{
			if (mSpring != null)
			{
				mSpring.SetEndValue(mRestValue);
			}

			return this;
		}

		public override void Constrain(MotionEvent ev)
		{
			base.Constrain(ev);
			mDownPosition = mProperty.GetValue(ev) + mOffset;
		}


		public override void Imitate(View view, MotionEvent ev)
		{

			float viewValue = mProperty.GetValue(view);
			float eventValue = mProperty.GetValue(ev);
			mOffset = mProperty.GetOffset(view);

			if (ev.HistorySize > 0)
			{
				float historicalValue = mProperty.GetOldestValue(ev);

				Imitate(viewValue + mOffset, eventValue, eventValue - historicalValue, ev);
			}
			else {
				Imitate(viewValue + mOffset, eventValue, 0, ev);
			}
		}


		public override void Mime(float offset, float value, float delta, float dt, MotionEvent ev)
		{
			if (mTrackStrategy == TRACK_DELTA)
			{
				base.Mime(offset - mDownPosition, value, delta, dt, ev);
			}
			else {
				base.Mime(offset, value, delta, dt, ev);
			}
		}


		protected override double MapToSpring(float motionValue)
		{
			return motionValue;
		}

		public MotionProperty GetProperty()
		{
			return mProperty;
		}
	}
}
