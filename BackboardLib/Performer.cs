

using System;
using Android.Views;
using Com.Facebook.Rebound;
/**
* Directly maps the motion of a {@link com.facebook.rebound.Spring} to a
* {@link android.util.Property} on a {@link android.view.View}.
* <p>
* Created by ericleong on 5/19/14.
*/
namespace com.tumblr.backboard
{
	public class Performer : Java.Lang.Object, ISpringListener
	{

		/**
		 * The view to modify.
		 */

		protected View mTarget;
		/**
		 * The property of the view to modify.
		 */

		protected Android.Util.Property mProperty;

		//public IntPtr Handle
		//{
		//	get
		//	{
		//		return this.Handle;
		//	}
		//}

		/**
		 * Constructor. Note that a {@link View} must be specified by {@link #setTarget(View)}.
		 *
		 * @param property
		 * 		the view property to modify.
		 */
		public Performer(Android.Util.Property property)
		{
			mTarget = null;
			mProperty = property;
		}

		/**
		 * Constructor.
		 *
		 * @param target
		 * 		the view to modify.
		 * @param property
		 * 		the view property to modify.
		 */
		public Performer(View target, Android.Util.Property property)
		{
			this.mTarget = target;
			this.mProperty = property;
		}


		public View GetTarget()
		{
			return mTarget;
		}

		public void SetTarget(View target)
		{
			this.mTarget = target;
		}


		public Android.Util.Property GetProperty()
		{
			return mProperty;
		}

		public void SetProperty(Android.Util.Property property)
		{
			this.mProperty = property;
		}


		public virtual void OnSpringUpdate(Spring spring)
		{
			if (mProperty != null && mTarget != null)
			{
				mProperty.Set(mTarget, (float)spring.CurrentValue);
			}
		}


		public virtual void OnSpringAtRest(Spring spring)
		{

		}


		public virtual void OnSpringActivate(Spring spring)
		{

		}


		public virtual void OnSpringEndStateChange(Spring spring)
		{

		}

		//public void Dispose()
		//{
		//	//throw new NotImplementedException();
		//}
	}
}
