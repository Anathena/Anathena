using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.Images
{
	public sealed class ImgContainer
	{
		private ComPtr<imgIContainer> _container;

		public ImgContainer()
		{
			_container = Xpcom.CreateInstance2<imgIContainer>( Contracts.ImageContainer );
		}

		internal ImgContainer(imgIContainer container)
		{
			_container = new ComPtr<imgIContainer>( container );
		}		

		public void Draw(gfxContext context, gfxGraphicsFilter filter, gfxMatrix matrix, gfxRect fill, nsIntRect subImage, uint viewportSize, IntPtr aSVGContext, uint aWhichFrame, uint flags)
		{
			_container.Instance.Draw(context, filter, matrix, fill, subImage, viewportSize, aSVGContext, aWhichFrame, flags);
		}

		public IntPtr GetFrame(uint whichFrame, uint flags)
		{
			return _container.Instance.GetFrame(whichFrame, flags);
		}

		//GetImageContainer
		//nsIFrame GetRootLayoutFrame()

		public void LockImage()
		{
			_container.Instance.LockImage();
		}

		public void RequestDecode()
		{
			_container.Instance.RequestDecode();
		}

		public void RequestDiscard()
		{
			//void requestDiscard(); Requires Gecko 13.0
		}
		
		public void RequestRefresh(ulong time)
		{
			_container.Instance.RequestRefresh( time );
		}

		public void ResetAnimation()
		{
			_container.Instance.ResetAnimation();
		}


		public void UnlockImage()
		{
			_container.Instance.UnlockImage();
		}

		public Locker Lock()
		{
			return new Locker( this );
		}

		public bool Animated
		{
			get { return _container.Instance.GetAnimatedAttribute(); }
		}

		public Animation AnimationMode
		{
			get { return ( Animation ) _container.Instance.GetAnimationModeAttribute(); }
			set { _container.Instance.SetAnimationModeAttribute( ( ushort ) value ); }
		}
		
		public int Height
		{
			get { return _container.Instance.GetHeightAttribute(); }
		}

		public uint Type
		{
			get
			{
				// type1 getter may be faster (C++ vs scriptable)
				var type1 = _container.Instance.GetType();
				var type2=_container.Instance.GetTypeAttribute();
				if (type1!=type2)
				{
					Console.WriteLine("Warning ImgContainter Type1!=Type2");
				}
				return type1;
			}
		}

		public int Width
		{
			get { return _container.Instance.GetWidthAttribute(); }
		}




		public enum Animation
			:ushort
		{
			Normal=0,
			DontAnimate=1,
			LoopOnce=2
		}
		

		public sealed class Locker
			:IDisposable
		{
			private ImgContainer _container;
			internal Locker(ImgContainer container)
			{
				_container = container;
				_container.LockImage();
			}

			~Locker()
			{
				Free();
			}

			public void Dispose()
			{
				Free();
				GC.SuppressFinalize( this );
			}

			private void Free()
			{
				if (_container == null) return;
				_container.UnlockImage();
				_container = null;
			}
		}
	}
}
