
using System;
using Android.Views;
using Com.Facebook.Rebound;
/**
* A {@link ConstrainedMotionImitator} that moves freely when the
* user is not dragging it. It copies the {@link com.facebook.rebound.SpringConfig} in
* {@link #setSpring(com.facebook.rebound.Spring)} to use when the user is dragging.
* <p>
* Created by ericleong on 11/6/14.
*/
namespace com.tumblr.backboard
{
	public class InertialImitator : ConstrainedMotionImitator, ISpringListener
	{

		/**
		 * The friction (in {@link com.facebook.rebound.SpringConfig}) to use when moving freely.
		 */
		public static float DEFAULT_FRICTION = 1.0f;

		/**
		 * The {@link com.facebook.rebound.SpringConfig} to use when moving freely.
		 */
		public static SpringConfig SPRING_CONFIG_FRICTION = new SpringConfig(0, DEFAULT_FRICTION);

		/**
		 * Used to convert {@link com.facebook.rebound.Spring#getVelocity()} to the units needed by
		 * {@link #calculateRestPosition()}.
		 */
		public static int VELOCITY_RATIO = 24;

		/**
		 * The {@link SpringConfig} to use when being dragged.
		 */
		protected SpringConfig mOriginalConfig;

		/*public IntPtr Handle
		{
			get
			{
				//return this.Handle;
			}
		}*/

		/**
		 * Constructor. Uses {@link #TRACK_ABSOLUTE} and {@link #FOLLOW_EXACT}.
		 *
		 * @param property
		 * 		the desired property to imitate
		 * @param minValue
		 * 		the minimum value
		 * @param maxValue
		 * 		the maximum value
		 */
		public InertialImitator(MotionProperty property, double minValue, double maxValue) :base(property, minValue, maxValue)
		{
			//super(property, minValue, maxValue);
		}

		/**
		 * Constructor.
		 *
		 * @param property
		 * 		the desired property to imitate
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 * @param minValue
		 * 		the desired minimum spring value.
		 * @param maxValue
		 * 		the desired maximum spring value.
		 */
		public InertialImitator(MotionProperty property, int trackStrategy, int followStrategy,
								double minValue, double maxValue) : base(property, trackStrategy, followStrategy, minValue, maxValue)
		{
			//super(property, trackStrategy, followStrategy, minValue, maxValue);
		}

		/**
		 * Sets the {@link com.facebook.rebound.Spring} that this imitator should use. This class
		 * attaches itself as a {@link com.facebook.rebound.SpringListener} and stores the
		 * {@link com.facebook.rebound.SpringConfig} to use when the user is dragging.
		 *
		 * @param spring
		 * 		the spring to use
		 */

		public override void SetSpring(Spring spring)
		{
			base.SetSpring(spring);
			spring.AddListener(this);

			mOriginalConfig = spring.SpringConfig;
		}


		public override void Constrain(MotionEvent ev)
		{
			base.Constrain(ev);

			mSpring.SetSpringConfig(mOriginalConfig);
		}

		//OVER
		public override void ReleaseInternal(MotionEvent ev)
		{
			// snap to left or right depending on current location
			double restPosition = CalculateRestPosition();
			if (mSpring.CurrentValue > mMaxValue && restPosition > mMaxValue)
			{
				mSpring.SetEndValue(mMaxValue);
			}
			else if (mSpring.CurrentValue < mMinValue && restPosition < mMinValue)
			{
				mSpring.SetEndValue(mMinValue);
			}
			else {
				mSpring.SetSpringConfig(SPRING_CONFIG_FRICTION);
				mSpring.SetEndValue(Java.Lang.Double.MaxValue);
			}
		}

		/**
		 * @return the spring position when it comes to rest (given infinite time).
		 */
		private double CalculateRestPosition()
		{
			// http://prettygoodphysics.wikispaces.com/file/view/DifferentialEquations.pdf
			return mSpring.CurrentValue + VELOCITY_RATIO * mSpring.Velocity / (mSpring.SpringConfig.Friction);
		}

		public void SetMinValue(double minValue)
		{
			this.mMinValue = minValue;
		}

		public void SetMaxValue(double maxValue)
		{
			this.mMaxValue = maxValue;
		}


		public void OnSpringUpdate(Spring spring)
		{
			if (mSpring != null)
			{
				double restPosition = CalculateRestPosition();
				if (mSpring.SpringConfig.Equals(SPRING_CONFIG_FRICTION))
				{
					if (mSpring.CurrentValue > mMaxValue && restPosition > mMaxValue)
					{
						mSpring.SetSpringConfig(mOriginalConfig);
						mSpring.SetEndValue(mMaxValue);
					}
					else if (mSpring.CurrentValue < mMinValue && restPosition < mMinValue)
					{
						mSpring.SetSpringConfig(mOriginalConfig);
						mSpring.SetEndValue(mMinValue);
					}
				}
			}
		}

		public void OnSpringAtRest(Spring spring)
		{
			// pass
		}


		public void OnSpringActivate(Spring spring)
		{
			// pass
		}

		public void OnSpringEndStateChange(Spring spring)
		{
			// pass
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}
}
