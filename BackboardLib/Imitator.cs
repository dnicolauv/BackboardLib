
using Com.Facebook.Rebound;
/**
* Perturbs a {@link com.facebook.rebound.Spring} based on some other source.
* {@link #mapToSpring(float)} controls how the source value maps to the spring. Track and follow
* strategies are hints to subclasses as to the desired behavior of the mapping.
* <p>
* Created by ericleong on 5/16/14.
*/
namespace com.tumblr.backboard
{
	public abstract class Imitator : Java.Lang.Object
	{

		/**
		 * Directly map the imitatee to the spring.
		 */
		public static int TRACK_ABSOLUTE = 1;
		/**
		 * Whether or not the spring will simply connect the object to the property we are imitating.
		 */
		protected int mTrackStrategy = TRACK_ABSOLUTE;
		/**
		 * Map the change in the imitatee to the spring.
		 */
		public static int TRACK_DELTA = 2;
		/**
		 * Map the imitatee to the current value of the spring.
		 */
		public static int FOLLOW_EXACT = 1;
		/**
		 * Whether or not the spring will simply connect the object to the property we are imitating.
		 */
		protected int mFollowStrategy = FOLLOW_EXACT;
		/**
		 * Map the imitatee to the end value of the spring.
		 */
		public static int FOLLOW_SPRING = 2;

		/**
		 * The spring to perturb.
		 */
		public Spring mSpring;
		/**
		 * The desired rest value of the spring when not being perturbed.
		 */
		protected double mRestValue;

		/**
		 * Intended to be used as part of a builder. The spring must be set with {@link #setSpring(Spring)}.
		 *
		 * @param restValue
		 * 		the rest value for the spring.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		protected Imitator(double restValue, int trackStrategy, int followStrategy)
		{
			//DNV
			//return new Imitator(null, restValue, trackStrategy, followStrategy);
			mSpring = null;
			mRestValue = restValue;
			mTrackStrategy = trackStrategy;
			mFollowStrategy = followStrategy;
		}

		/**
		 * Constructor. Uses {@link #TRACK_ABSOLUTE} and {@link #FOLLOW_EXACT}.
		 *
		 * @param spring
		 * 		the spring to use.
		 */
		protected Imitator(Spring spring)
		{
			//DNV
			//this(spring, TRACK_ABSOLUTE, FOLLOW_EXACT);
			mSpring = spring;
			mTrackStrategy = TRACK_ABSOLUTE;
			mFollowStrategy = FOLLOW_EXACT;
		}

		/**
		 * Constructor.
		 *
		 * @param spring
		 * 		the spring to use.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		protected Imitator(Spring spring, int trackStrategy, int followStrategy)
		{
			//DNV
			//this(spring, spring.EndValue, trackStrategy, followStrategy);
			mSpring = spring;
			mRestValue = spring.EndValue;
			mTrackStrategy = trackStrategy;
			mFollowStrategy = followStrategy;
		}

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
		protected Imitator(Spring spring, double restValue, int trackStrategy, int followStrategy)
		{
			mSpring = spring;
			mRestValue = restValue;
			mTrackStrategy = trackStrategy;
			mFollowStrategy = followStrategy;
		}

		/**
		 * @param motionValue
		 * 		Maps the value we are tracking to the value of the spring.
		 * @return the new end value of the spring. If set to {@link #FOLLOW_EXACT}, it is also the current value of the
		 * spring.
		 */
		protected abstract double MapToSpring(float motionValue);

		public int GetTrackStrategy()
		{
			return mTrackStrategy;
		}

		/**
		 * @param trackStrategy
		 * 		the desired tracking strategy
		 * @return this object for chaining
		 */
		public Imitator SetTrackStrategy(int trackStrategy)
		{
			mTrackStrategy = trackStrategy;
			return this;
		}

		public int GetFollowStrategy()
		{
			return mFollowStrategy;
		}

		/**
		 * @param followStrategy
		 * 		the desired follow strategy
		 * @return this object for chaining
		 */
		public Imitator SetFollowStrategy(int followStrategy)
		{
			mFollowStrategy = followStrategy;
			return this;
		}

		public Spring GetSpring()
		{
			return mSpring;
		}

		/**
		 * @param spring
		 * 		the spring to set (sets spring's current to the rest value)
		 */
		public virtual void SetSpring(Spring spring)
		{
			mSpring = spring;

			if (mSpring != null)
			{
				// Start spring at rest.
				//DNV
				//mSpring.SetCurrentValue(mRestValue, true);
				mSpring.SetCurrentValue(mRestValue);
			}
		}
	}
}
