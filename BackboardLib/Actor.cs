

using System.Collections.Generic;
using Android.Views;
using Com.Facebook.Rebound;
using Java.Interop;
using Java.Lang;
using System;
/**
* Coordinates the relationship between {@link com.tumblr.backboard.imitator.MotionImitator}s,
* {@link com.facebook.rebound.Spring}s, and {@link com.tumblr.backboard.performer.Performer}s on a
* single {@link android.view.View}.
* <p>
* This primarily exists to manage the {@link android.view.View.OnTouchListener} on the
* {@link android.view.View}.
* <p>
* Created by ericleong on 5/20/14.
*/
namespace com.tumblr.backboard
{
	public class Actor
	{

		/**
		 * Distance in pixels that can be moved before a touch is no longer considered a "click".
		 */
			public static  int MAX_CLICK_DISTANCE = 10;

		/**
		 * Contains the imitators and listeners coupled to a single spring.
		 */
		public class Motion
		{
			
			public Spring spring;
		
			public EventImitator[] imitators;
		
			public Performer[] performers;
		
			public ISpringListener[] springListeners;

			public Motion(Spring spring, EventImitator imitator, Performer[] performers, ISpringListener[] springListeners)
			{
				//this(spring, new EventImitator[] { imitator }, performers, springListeners);
				this.spring = spring;
				this.imitators = new EventImitator[] { imitator };
				this.performers = performers;
				this.springListeners = springListeners;
			}

			public Motion(Spring spring, Performer[] performers, ISpringListener[] springListeners)
			{
				this.imitators = new MotionImitator[0];
				this.performers = performers;
				this.spring = spring;
				this.springListeners = springListeners;
			}

			public Motion(Spring spring, EventImitator[] imitators, Performer[] performers, ISpringListener[] springListeners)
			{
				this.imitators = imitators;
				this.performers = performers;
				this.spring = spring;
				this.springListeners = springListeners;
			}

			
			public Spring GetSpring()
			{
				return spring;
			}

			
			public EventImitator[] GetImitators()
			{
				return imitators;
			}
		}

		
		private  View mView;
	
		private  List<Motion> mMotions;
		
		private  MotionListener mMotionListener;
	
		private  View.IOnTouchListener mOnTouchListener;
		/**
		 * Allows the user to disable the motion listener.
		 */
		private bool mMotionListenerEnabled;
		/**
		 * Prevent parent from intercepting touch events (useful when in lists).
		 */
		private bool mRequestDisallowTouchEvent;

		private Actor(View view, List<Motion> motions, View.IOnTouchListener onTouchListener, bool motionListenerEnabled, bool attachTouchListener, bool requestDisallowTouchEvent)
		{
			mView = view;
			mMotions = motions;
			mOnTouchListener = onTouchListener;

            try
            {
                mMotionListener = new MotionListener();
                mMotionListener.mMotionListenerEnabled = mMotionListenerEnabled;
                mMotionListener.mMotions = mMotions;
                mMotionListener.mOnTouchListener = onTouchListener;
                mMotionListener.mRequestDisallowTouchEvent = mRequestDisallowTouchEvent;
                mMotionListener.MAX_CLICK_DISTANCE = MAX_CLICK_DISTANCE;
            }
            catch(Java.Lang.Exception ex)
            {
                throw ex;
            }
			
				mMotionListenerEnabled = motionListenerEnabled;

			mRequestDisallowTouchEvent = requestDisallowTouchEvent;

			if (attachTouchListener)
			{
				view.SetOnTouchListener(mMotionListener);
            }
		}
		
		public View.IOnTouchListener GetOnTouchListener()
		{
			return mOnTouchListener;
		}

		
		public View.IOnTouchListener GetMotionListener()
		{
			return mMotionListener;
		}

		
		public View GetView()
		{
			return mView;
		}

		
		public List<Motion> GetMotions()
		{
			return mMotions;
		}

		public bool IsTouchEnabled()
		{
			return mMotionListenerEnabled;
		}

		public void SetTouchEnabled( bool enabled)
		{
			this.mMotionListenerEnabled = enabled;
		}

		/**
		 * Removes all spring listeners controlled by this {@link Actor}.
		 */
		public void RemoveAllListeners()
		{
			foreach (Motion motion in mMotions)
			{
				foreach (Performer performer in motion.performers)
				{
					motion.spring.RemoveListener(performer);
				}

				if (motion.springListeners != null)
				{
					foreach (ISpringListener listener in motion.springListeners)
					{
						motion.spring.RemoveListener(listener);
					}
				}
			}
		}

		/**
		 * Adds all spring listeners back.
		 */
		public void AddAllListeners()
		{
            try
            {
                foreach (Motion motion in mMotions)
                {
                    foreach (Performer performer in motion.performers)
                    {
                        motion.spring.AddListener(performer);
                    }

                    if (motion.springListeners != null)
                    {
                        foreach (ISpringListener listener in motion.springListeners)
                        {
                            motion.spring.AddListener(listener);
                        }
                    }
                }
            }catch(System.Exception ex)
            {
                throw ex;
            }
		}

		/**
		 * Implements the builder pattern for {@link Actor}.
		 */
		public class Builder
		{
			private View mView;
		
			public  List<Motion> mMotions = new List<Motion>();
			
			private View.IOnTouchListener mOnTouchListener;
			
			private  SpringSystem mSpringSystem;
			public bool mMotionListenerEnabled = true;
			private bool mAttachMotionListener = true;
			private bool mRequestDisallowTouchEvent;
			private bool mAttachISpringListeners = true;

			/**
			 * Animates the given view with the default {@link com.facebook.rebound.SpringConfig} and
			 * automatically creates a {@link com.facebook.rebound.SpringSystem}.
			 *
			 * @param springSystem
			 * 		the spring system to use
			 * @param view
			 * 		the view to animate
			 */
			public Builder(SpringSystem springSystem, View view)
			{
				mView = view;
				mSpringSystem = springSystem;
			}

			/**
			 * @param onTouchListener
			 * 		a touch listener to pass touch events to
			 * @return this builder for chaining
			 */
			
			public Builder OnTouchListener( View.IOnTouchListener onTouchListener)
			{
				mOnTouchListener = onTouchListener;
				return this;
			}

			/**
			 * Uses the default {@link com.facebook.rebound.SpringConfig} to animate the view.
			 *
			 * @param properties
			 * 		the event fields to imitate and the view properties to animate.
			 * @return this builder for chaining
			 */
			
			public Builder AddTranslateMotion( params MotionProperty[] properties)
			{
				return AddMotion(mSpringSystem.CreateSpring(), properties);
			}

			/**
			 * Uses the default {@link com.facebook.rebound.SpringConfig} to animate the view.
			 *
			 * @param property
			 * 		the event field to imitate and the view property to animate.
			 * @param listener
			 * 		a listener to call
			 * @return this builder for chaining
			 */
			
			public Builder AddTranslateMotion( MotionProperty property,  ISpringListener listener)
			{
				return AddMotion(mSpringSystem.CreateSpring(), Imitator.TRACK_ABSOLUTE,
						Imitator.FOLLOW_EXACT, new MotionProperty[] { property },
						new ISpringListener[] { listener });
			}

			/**
			 * Uses the default {@link com.facebook.rebound.SpringConfig} to animate the view.
			 *
			 * @param trackStrategy
			 * 		the tracking behavior
			 * @param followStrategy
			 * 		the follow behavior
			 * @param properties
			 * 		the event fields to imitate and the view properties to animate.
			 * @return this builder for chaining
			 */
			
			public Builder AddTranslateMotion( int trackStrategy,  int followStrategy, params MotionProperty[] properties)
			{
				return AddMotion(mSpringSystem.CreateSpring(), trackStrategy, followStrategy, properties);
			}

			/**
			 * Uses the default {@link com.facebook.rebound.SpringConfig} to animate the view.
			 *
			 * @param trackStrategy
			 * 		the tracking behavior
			 * @param followStrategy
			 * 		the follow behavior
			 * @param restValue
			 * 		the rest value of the spring
			 * @param properties
			 * 		the event fields to imitate and the view properties to animate.
			 * @return this builder for chaining
			 */
			
			public Builder AddTranslateMotion( int trackStrategy,  int followStrategy, int restValue, params MotionProperty[] properties)
			{
				return AddMotion(mSpringSystem.CreateSpring(), trackStrategy, followStrategy, restValue, properties);
			}

			/**
			 * @param spring
			 * 		the underlying {@link com.facebook.rebound.Spring}.
			 * @param properties
			 * 		the event fields to imitate and the view properties to animate.
			 * @return this builder for chaining
			 */
			
			public Builder AddMotion(Spring spring, params MotionProperty[] properties)
			{
				return AddMotion(spring, Imitator.TRACK_ABSOLUTE, Imitator.FOLLOW_EXACT, properties);
			}

			/**
			 * @param spring
			 * 		the underlying {@link com.facebook.rebound.Spring}.
			 * @param trackStrategy
			 * 		the tracking behavior
			 * @param followStrategy
			 * 		the follow behavior
			 * @param properties
			 * 		the event fields to imitate and the view properties to animate.
			 * @return this builder for chaining
			 */
			
			public Builder AddMotion(Spring spring,  int trackStrategy,  int followStrategy, MotionProperty[] properties)
			{

				mMotions.Add(CreateMotionFromProperties(spring, properties, null, trackStrategy, followStrategy, 0));

				return this;
			}

			/**
			 * @param spring
			 * 		the underlying {@link com.facebook.rebound.Spring}.
			 * @param trackStrategy
			 * 		the tracking behavior
			 * @param followStrategy
			 * 		the follow behavior
			 * @param restValue
			 * 		the rest value
			 * @param properties
			 * 		the event fields to imitate and the view properties to animate.
			 * @return this builder for chaining
			 */
			
			public Builder AddMotion(Spring spring,  int trackStrategy,  int followStrategy, int restValue,   MotionProperty[] properties)
			{

				mMotions.Add(CreateMotionFromProperties(spring, properties, null, trackStrategy, followStrategy, restValue));

				return this;
			}

			/**
			 * @param spring
			 * 		the underlying {@link com.facebook.rebound.Spring}.
			 * @param trackStrategy
			 * 		the tracking behavior
			 * @param followStrategy
			 * 		the follow behavior
			 * @param restValue
			 * 		the rest value
			 * @param property
			 * 		the event fields to imitate and the view property to animate.
			 * @param springListener
			 * 		a spring listener to attach to the spring
			 * @return this builder for chaining
			 */
			
			public Builder AddMotion(Spring spring,  int trackStrategy,  int followStrategy,
									  int restValue,  MotionProperty property,   ISpringListener springListener)
			{

				mMotions.Add(CreateMotionFromProperties(spring, new MotionProperty[] { property },
								new ISpringListener[] { springListener }, trackStrategy, followStrategy, restValue));

				return this;
			}

			/**
			 * @param spring
			 * 		the underlying {@link com.facebook.rebound.Spring}.
			 * @param trackStrategy
			 * 		the tracking behavior
			 * @param followStrategy
			 * 		the follow behavior
			 * @param properties
			 * 		the event fields to imitate and the view properties to animate.
			 * @param springListeners
			 * 		an array of spring listeners to attach to the spring
			 * @return this builder for chaining
			 */
			
			public Builder AddMotion(Spring spring,  int trackStrategy,  int followStrategy,
									   MotionProperty[] properties,  ISpringListener[] springListeners)
			{

				mMotions.Add(CreateMotionFromProperties(spring, properties, springListeners, trackStrategy, followStrategy, 0));

				return this;
			}

			/**
			 * Uses a default {@link com.facebook.rebound.SpringConfig}.
			 *
			 * @param eventImitator
			 * 		maps an event to a {@link com.facebook.rebound.Spring}
			 * @param viewProperties
			 * 		the {@link android.view.View} property to animate
			 * @return the builder for chaining
			 */

			public Builder AddMotion(EventImitator eventImitator, params Android.Util.Property[] viewProperties)
			{
				Performer[] performers = new Performer[viewProperties.Length];

				for (int i = 0; i < viewProperties.Length; i++)
				{
					performers[i] = new Performer(viewProperties[i]);
				}

				return AddMotion(mSpringSystem.CreateSpring(), eventImitator, performers);
			}

			/**
			 * Uses a default {@link com.facebook.rebound.SpringConfig}.
			 *
			 * @param eventImitator
			 * 		maps an event to a {@link com.facebook.rebound.Spring}
			 * @param performers
			 * 		map the {@link com.facebook.rebound.Spring} to a
			 * 		{@link android.view.View}
			 * @return the builder for chaining
			 */
			
			public Builder AddMotion(  EventImitator eventImitator,  Performer[] performers)
			{
				return AddMotion(mSpringSystem.CreateSpring(), eventImitator, performers);
			}

			/**
			 * @param spring
			 * 		the underlying {@link com.facebook.rebound.Spring}.
			 * @param eventImitator
			 * 		maps an event to a {@link com.facebook.rebound.Spring}
			 * @param performers
			 * 		map the {@link com.facebook.rebound.Spring} to a
			 * 		{@link android.view.View}
			 * @return the builder for chaining
			 */
			
			public Builder AddMotion(Spring spring, EventImitator eventImitator, Performer[] performers)
			{

				 Motion motion = new Motion(spring, eventImitator, performers, null);

				// connect actors
				motion.imitators[0].SetSpring(motion.spring);

				foreach (Performer performer in motion.performers)
				{
					performer.SetTarget(mView);
				}

				mMotions.Add(motion);

				return this;
			}

			/**
			 * @param spring
			 * 		the underlying {@link com.facebook.rebound.Spring}.
			 * @param eventImitator
			 * 		maps an event to a {@link com.facebook.rebound.Spring}
			 * @param performers
			 * 		map the {@link com.facebook.rebound.Spring} to a
			 * 		{@link android.view.View}
			 * @param springListeners
			 * 		additional listeners to attach
			 * @return the builder for chaining
			 */
			
			public Builder AddMotion(  Spring spring,   EventImitator eventImitator,
									   Performer[] performers,  ISpringListener[] springListeners)
			{

				// create struct
				 Motion motion = new Motion(spring, eventImitator, performers, springListeners);

				// connect actors
				motion.imitators[0].SetSpring(motion.spring);

				foreach (Performer performer in motion.performers)
				{
					performer.SetTarget(mView);
				}

				mMotions.Add(motion);

				return this;
			}

			/**
			 * @param motionImitator
			 * 		maps an event to a {@link com.facebook.rebound.Spring}
			 * @param viewProperty
			 * 		the {@link android.view.View} property to animate
			 * @param springListener
			 * 		additional listener to attach
			 * @return the builder for chaining
			 */
			
			public Builder AddMotion(  MotionImitator motionImitator,
			                           Android.Util.Property viewProperty,
									  ISpringListener springListener)
			{

				return AddMotion(mSpringSystem.CreateSpring(), motionImitator,
						new Performer[] { new Performer(viewProperty) },
						new ISpringListener[] { springListener });
			}

			/**
			 * @return flag to tell the attached {@link android.view.View.OnTouchListener} to call
			 * {@link android.view.ViewParent#requestDisallowInterceptTouchEvent(bool)} with
			 * <code>true</code>.
			 */
			
			public Builder RequestDisallowTouchEvent()
			{
				mRequestDisallowTouchEvent = true;
				return this;
			}

			/**
			 * A flag to tell this {@link Actor} not to attach the touch listener to the view.
			 *
			 * @return the builder for chaining
			 */
			
			public Builder DontAttachMotionListener()
			{
				mAttachMotionListener = false;
				return this;
			}

			/**
			 * A flag to tell this builder not to attach the spring listeners to the spring.
			 * They can be added with {@link Actor#addAllListeners()}.
			 *
			 * @return the builder for chaining
			 */
			
			public Builder DontAttachISpringListeners()
			{
				mAttachISpringListeners = false;
				return this;
			}

			/**
			 * Creations a new motion object.
			 *
			 * @param spring
			 * 		the spring to use
			 * @param motionProperties
			 * 		the properties of the event to track
			 * @param springListeners
			 * 		additional spring listeners to add
			 * @param trackStrategy
			 * 		the tracking strategy
			 * @param followStrategy
			 * 		the follow strategy
			 * @param restValue
			 * 		the spring rest value
			 * @return a motion object
			 */
			
			private Motion CreateMotionFromProperties(  Spring spring,
													    MotionProperty[] motionProperties,
													    ISpringListener[] springListeners,
													   int trackStrategy,  int followStrategy,
													   int restValue)
			{

				 MotionImitator[] motionImitators = new MotionImitator[motionProperties.Length];
				 Performer[] performers = new Performer[motionProperties.Length];

				for (int i = 0; i < motionProperties.Length; i++)
				{

					 MotionProperty property = motionProperties[i];

					motionImitators[i] = new MotionImitator(spring, property, restValue, trackStrategy, followStrategy);
					performers[i] = new Performer(mView, property.ViewProperty);
				}

				return new Motion(spring, motionImitators, performers, springListeners);
			}

			/**
			 * @return Builds the {@link Actor}.
			 */
			
			public Actor Build()
			{
				// make connections

				 Actor actor = new Actor(mView, mMotions, mOnTouchListener, mMotionListenerEnabled, mAttachMotionListener,
						mRequestDisallowTouchEvent);

				if (mAttachISpringListeners)
				{
					actor.AddAllListeners();
				}

				return actor;
			}

		}
	}
}