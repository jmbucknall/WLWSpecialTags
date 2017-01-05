Windows Live Writer special tag/entity inserter
===

_Inserting code elements and entity characters into Windows Live Writer_

Introduction
---

The blog posts I write are mostly about code and the like. I found myself wishing for the ability to insert code identifiers into the main body of the document like this: `someIdentifier`. Yes, I could go into the Source view and manually add them, but I wanted to be able to type them markdown style: \`someIdentifer\`, press a couple of keystrokes and magically get it done.

Then, along the same lines, I wanted to be able to insert special entity characters into the text. Things like non-breaking spaces, the multiply character, trademarks, and so on. 

Enter the Windows Live Writer special tag/entity inserter.

Using with Window Live Writer
===

Compiling the code with Visual Studio will produce a DLL called `WLWSpecialTags.dll`. Make sure you are not running Windows Live Writer, then copy this DLL to the `\Program Files (x86)\Windows Live\Writer\Plugins` folder (you'll need admin rights). Restart Window Live Writer. 

To convert some text, select it, go to the Insert tab, and look in the Plug-ins folder. Select Special Tag Inserter and the selected text will be converted.

Conversions made
===

These are the conversions made by the plug-in:

- `` `selectedtext` `` is converted to `<code>selectedtext</code>`
- `?` is converted to a complete list of the entities known by the plug-in
- if the selected text is not one of the abbreviations below, it is converted as `&selectedtext;`

This is the list of known entity abbreviations, plus the converted character

- `sp` => `&nbsp;`   non-breaking space
- `>>` => `&#xbb;`   chevron right
- `<<` => `&#xab;`   chevron left
- `&gt;&gt;` => `&#xbb;`   chevron right
- `&lt;&lt;` => `&#xab;`   chevron left
- `(c)` => `&#xa9;`   copyright
- `S` => `&#xa7;`   section mark
- `P` => `&para;`   paragraph mark
- `*` => `&#x2022;`   bullet
- `.` => `&#xb7;`   middle dot 
- `-` => `&#x2013;`   dash
- `--` => `&#x2014;`   long dash
- `:>` => `&#x25b6;`   large triangle pointing right
- `:&gt;` => `&#x25b6;`   large triangle pointing right
- `o` => `&#x25e6;`   degree symbol
- `[]` => `&#x25ab;`   small middle square
- `<>` => `&#x25c7;`   diamond
- `&lt;&gt;` => `&#x25c7;`   diamond
- `1/2` => `&frac12;`   1/2 as character
- `1/4` => `&frac14;`   1/4 as character
- `3/4` => `&frac34;`   3/4 as character
- `tm` => `&trade;`   trademark
- `(r)` => `&reg;`   registered trademark
- `...` => `&hellip;`   ellipsis
- `x` => `&times;`   multiply operator
- `/` => `&divide;`   divide operator

So for example, selecting the text `:>` and invoking the plug-in will replace the selected text with &#x25b6;.

