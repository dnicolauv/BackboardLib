
using Android.Views;
using com.tumblr.backboard;
/**
* Constrains the motion between the minimum and maximum values.
* <p>
* Created by ericleong on 10/9/14.
*/
namespace com.tumblr.backboard
{
	public class ConstrainedMotionImitator : MotionImitator
	{
		/**
		 * Desired minimum spring value (overshoot may still occur).
		 */
		protected double mMinValue;
		/**
		 * Desired maximum spring value (overshoot may still occur).
		 */
		protected double mMaxValue;

		/**
		 * Constructor. Uses {@link #TRACK_ABSOLUTE} and {@link #FOLLOW_EXACT}.
		 *
		 * @param property
		 * 		the property to track.
		 * @param minValue
		 * 		the desired minimum spring value.
		 * @param maxValue
		 * 		the desired maximum spring value.
		 */
		public ConstrainedMotionImitator(MotionProperty property, double minValue, double maxValue) : base(property, TRACK_ABSOLUTE, FOLLOW_EXACT)
		{
			this.mMinValue = minValue;
			this.mMaxValue = maxValue;
		}

		/**
		 * Constructor.
		 *
		 * @param property
		 * 		the property to track.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 * @param minValue
		 * 		the desired minimum spring value.
		 * @param maxValue
		 * 		the desired maximum spring value.
		 */
		public ConstrainedMotionImitator(MotionProperty property, int trackStrategy, int followStrategy, double minValue, double maxValue)
									: base(property, trackStrategy, followStrategy)
		{
			this.mMinValue = minValue;
			this.mMaxValue = maxValue;
		}


		//OVER
		public override void ReleaseInternal(MotionEvent ev)
		{
			if (mSpring != null)
			{
				// snap to left or right depending on current location
				if (mSpring.CurrentValue > mMaxValue)
				{
					mSpring.SetEndValue(mMaxValue);
				}
				else if (mSpring.CurrentValue < mMinValue)
				{
					mSpring.SetEndValue(mMinValue);
				}
			}
		}

		public void SetMinValue(double minValue)
		{
			this.mMinValue = minValue;
		}

		public void SetMaxValue(double maxValue)
		{
			this.mMaxValue = maxValue;
		}
	}
}
