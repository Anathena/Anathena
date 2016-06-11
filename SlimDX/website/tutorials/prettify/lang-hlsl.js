// Copyright (C) 2009 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.



/**
 * @fileoverview
 * Registers a language handler for HLSL.
 *
 *
 * To use, include prettify.js and this file in your HTML page.
 * Then put your code in an HTML tag like
 *      <pre class="prettyprint lang-hlsl"></pre>
 *
 *
 * @author mike.popoloski@gmail.com
 */

PR.registerLangHandler(
    PR.createSimpleLexer(
        [
         [PR.PR_STRING, /^(?:\'(?:[^\\\']|\\[\s\S])*(?:\'|$)|\"(?:[^\\\"]|\\[\s\S])*(?:\"|$)|\`(?:[^\\\`]|\\[\s\S])*(?:\`|$))/, null, '\'"`'],
         [PR.PR_PLAIN, /^\s+/, null, ' \r\n\t\xA0'],
         [PR.PR_COMMENT, /^#(?:(?:define|elif|else|endif|error|ifdef|include|ifndef|line|pragma|undef|warning)\b|[^\r\n]*)/, null, '#']
        ],
        [
         [PR.PR_STRING, /^<(?:(?:(?:\.\.\/)*|\/?)(?:[\w-]+(?:\/[\w-]+)+)?[\w-]+\.h|[a-z]\w*)>/, null],
         [PR.PR_COMMENT, /^\/\/[^\r\n]*/, null],
         [PR.PR_COMMENT, /^\/\*[\s\S]*?(?:\*\/|$)/, null],
         [PR.PR_KEYWORD, /^(?:break|continue|discard|do|for|if|switch|while|inline|return|in|out|inout|uniform|linear|centroid|nointerpolation|noperspective|sample|static|extern|const|else|true|false|compile|texture|Texture1D|Texture1DArray|Texture2D|Texture2DArray|Texture2DMS|Texture2DMSArray|Texture3D|TextureCube|TextureCubeArray|VertexShader|PixelShader|GeometryShader|struct|precise|shared|groupshared|volatile|void|switch|row_major|column_major|packoffset|register|unorm|snorm|(?:double[x0-9])|(?:float[x0-9])|(?:bool[x0-9])|(?:int[x0-9])|(?:uint[x0-9])|(?:half[x0-9])|string|sampler|sampler1D|sampler2D|sampler3D|samplerCUBE|sampler_state|SamplerState|SamplerComparisonState|typedef|(?:(?:vs|gs|ps|ds|cs|hs)_[0-9]_[0-9])|class|interface|namespace|pass|technique|technique10)\b/i, null],
         [PR.PR_LITERAL, /^@[a-z_$][a-z_$@0-9]*/i, null],
         [PR.PR_TYPE, /^@?(?:Buffer|vector|matrix|CBuffer|DepthStencilState|DepthStencilView|BlendState|StateBlock|stateblock_state|TBuffer|RasterizerState|RenderTargetView)\b/i, null],
         [PR.PR_TAG, /^\:\s*[A-Za-z_$@0-9]+/, null],
         [PR.PR_PLAIN, /^[\.A-Za-z_$][a-z_$@0-9]*/i, null],
         [PR.PR_LITERAL,
          new RegExp(
             '^(?:'
             // A hex number
             + '0x[a-f0-9]+'
             // or an octal or decimal number,
             + '|(?:\\d(?:_\\d+)*\\d*(?:\\.\\d*)?|\\.\\d\\+)'
             // possibly in scientific notation
             + '(?:e[+\\-]?\\d+)?'
             + ')'
             // with an optional modifier like UL for unsigned long
             + '[a-z]*', 'i'),
          null, '0123456789'],
          [PR.PR_PUNCTUATION, /^.[^\s\w\.$@\'\"\`\/\#]*/, null]
        ]),
    ['hlsl']);
