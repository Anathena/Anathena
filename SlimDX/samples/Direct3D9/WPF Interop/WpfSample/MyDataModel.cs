// Copyright (c) 2010, LizardTech, a Celartem company
// All rights reserved.
//                                                              
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//       this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of LizardTech nor the names of its contributors may be
//       used to endorse or promote products derived from this software without
//       specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Windows;
using System.Windows.Resources;
using SlimDX;
using SlimDX.Direct3D9;
using System.IO;
using System.Drawing;
using SlimDX.Wpf;

namespace WpfSample
{
   public class MyDataModel
   {
      private byte[] _buffer;
      private SlimDXControl _control;

      public MyDataModel(SlimDXControl slimDXControl, string imageName, Color color)
      {
         _control = slimDXControl;

         Name = imageName;
         Uri uri = new Uri("pack://application:,,,/Resources/" + imageName + ".png");

         using (Stream s = Application.GetResourceStream(uri).Stream)
         {
            _buffer = new byte[s.Length];
            s.Read(_buffer, 0, (int)s.Length);
         }

         Color = color;
      }

      public string Name
      {
         private set;
         get;
      }

      public Stream Stream
      {
         get
         {
            MemoryStream ms = new MemoryStream(_buffer);
            return ms;
         }
      }

      private Color m_color;
      public Color Color
      {
         get
         {
            return m_color;
         }
         set
         {
            if (m_color != value)
            {
               m_color = value;
               Color4 = new Color4(m_color);
               _control.ForceRendering();

            }
         }
      }

      public Color4 Color4
      {
         get;
         private set;
      }
   }

}
