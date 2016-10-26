
using System;
/**
* Allows a {@link com.facebook.rebound.Spring} to imitate another {@link com.facebook.rebound.Spring}.
* <p>
* The default implementation sets the end value of the attached Spring to be the same as the
* current value of the couple it is imitating.
* <p>
* Created by ericleong on 5/16/14.
* 
*/
using Com.Facebook.Rebound;
namespace com.tumblr.backboard
{
	public class SpringImitator : Imitator, ISpringListener
	{
		/**
		 * @param spring
		 * 		the spring to imitate
		 */
		public SpringImitator(Spring spring) : base(spring)
		{
			//super(spring);
		}

		/**
		 * Constructor.
		 *
		 * @param spring
		 * 		the spring to imitate.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		protected SpringImitator(Spring spring, int trackStrategy, int followStrategy) : base(spring, trackStrategy, followStrategy)
		{
			//super(spring, trackStrategy, followStrategy);
		}

		/**
		 * Constructor.
		 *
		 * @param spring
		 * 		the spring to imitate.
		 * @param restValue
		 * 		the spring value when not being perturbed.
		 * @param trackStrategy
		 * 		the tracking strategy.
		 * @param followStrategy
		 * 		the follow strategy.
		 */
		protected SpringImitator(Spring spring, double restValue, int trackStrategy, int followStrategy) : base(spring, restValue, trackStrategy, followStrategy)
		{
			//super(spring, restValue, trackStrategy, followStrategy);
		}


		protected override double MapToSpring(float motionValue)
		{
			return motionValue;
		}



		public void OnSpringUpdate(Spring spring)
		{
			mSpring.SetEndValue(spring.CurrentValue);
		}

		public void OnSpringAtRest(Spring spring)
		{

		}

		public void OnSpringActivate(Spring spring)
		{

		}


		public void OnSpringEndStateChange(Spring spring)
		{

		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}
	}
}
