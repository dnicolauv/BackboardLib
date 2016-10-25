
using Android.Views;
using Com.Facebook.Rebound;
/**
* Maps the motion of a {@link com.facebook.rebound.Spring} to a
* {@link android.util.Property} on a {@link android.view.View}.
* <p>
* Created by ericleong on 5/6/14.
*/
namespace com.tumblr.backboard
{
	public class MapPerformer : Performer
	{

		private static string TAG = "MapPerformer";

		private float initialStart, initialEnd, start, end;

		/**
		 * Constructor. Maps the spring domain [0, 1] to the view property range [0, 1].
		 *
		 * @param target
		 * 		the view to modify.
		 * @param property
		 * 		the view property to modify.
		 */
		public MapPerformer(View target, Android.Util.Property property) : base(target, property)
		{
			//this(target, property, 0, 1, 0, 1);
			initialStart = 0;
			initialEnd = 1;
			this.start = 0;
			this.end = 1;
		}

		/**
		 * Constructor. Maps the spring domain [0, 1] to the specified view property range.
		 *
		 * @param target
		 * 		the view to modify.
		 * @param property
		 * 		the view property to modify.
		 * @param start
		 * 		the minimum value for the view property range.
		 * @param end
		 * 		the maximum value for the view property range.
		 */
		public MapPerformer(View target, Android.Util.Property property, float start, float end) : base(target, property)
		{
			//this(target, property, 0, 1, start, end);
			initialStart = 0;
			initialEnd = 1;
			this.start = start;
			this.end = end;
		}

		/**
		 * Constructor. Maps the spring domain [0, 1] to the specified view property range.
		 *
		 * @param target
		 * 		the view to modify.
		 * @param property
		 * 		the view property to modify.
		 * @param initialStart
		 * 		the minimum value for the spring domain.
		 * @param initialEnd
		 * 		the maximum value for the spring domain.
		 * @param start
		 * 		the minimum value for the view property range.
		 * @param end
		 * 		the maximum value for the view property range.
		 */
		public MapPerformer(View target, Android.Util.Property property, float initialStart,
							float initialEnd, float start, float end) : base(target, property)
		{
			//super(target, property);

			this.initialStart = initialStart;
			this.initialEnd = initialEnd;
			this.start = start;
			this.end = end;
		}


		public override void OnSpringUpdate(Spring spring)
		{

			mProperty.Set(mTarget,
						  (float)SpringUtil.MapValueFromRangeToRange(spring.CurrentValue,
							initialStart, initialEnd, start, end)
			);
		}
	}
}
