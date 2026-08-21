using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class SpecGfmTests
{
    [TestMethod]
    public void Example1Line368Foobazbim()
    {
        var input = @"
	foo	baz		bim
";
        var expected = @"
<pre><code>foo	baz		bim
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example2Line375Foobazbim()
    {
        var input = @"
  	foo	baz		bim
";
        var expected = @"
<pre><code>foo	baz		bim
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example3Line382AanA()
    {
        var input = @"
    a	a
    ὐ	a
";
        var expected = @"
<pre><code>a	a
ὐ	a
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example4Line395Foonnbar()
    {
        var input = @"
  - foo

	bar
";
        var expected = @"
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example5Line408Foonnbar()
    {
        var input = @"
- foo

		bar
";
        var expected = @"
<ul>
<li>
<p>foo</p>
<pre><code>  bar
</code></pre>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example6Line431Foo()
    {
        var input = @"
>		foo
";
        var expected = @"
<blockquote>
<pre><code>  foo
</code></pre>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example7Line440Foo()
    {
        var input = @"
-		foo
";
        var expected = @"
<ul>
<li>
<pre><code>  foo
</code></pre>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example8Line452Foonbar()
    {
        var input = @"
    foo
	bar
";
        var expected = @"
<pre><code>foo
bar
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example9Line461FoonBarnBaz()
    {
        var input = @"
 - foo
   - bar
	 - baz
";
        var expected = @"
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>baz</li>
</ul>
</li>
</ul>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example10Line479Foo()
    {
        var input = @"
#	Foo
";
        var expected = @"
<h1>Foo</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example11Line485()
    {
        var input = @"
*	*	*	
";
        var expected = @"
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example12Line512OnenTwo()
    {
        var input = @"
- `one
- two`
";
        var expected = @"
<ul>
<li>`one</li>
<li>two`</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example13Line551NN()
    {
        var input = @"
***
---
___
";
        var expected = @"
<hr />
<hr />
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example14Line564()
    {
        var input = @"
+++
";
        var expected = @"
<p>+++</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example15Line571()
    {
        var input = @"
===
";
        var expected = @"
<p>===</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example16Line580Nn()
    {
        var input = @"
--
**
__
";
        var expected = @"
<p>--
**
__</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example17Line593NN()
    {
        var input = @"
 ***
  ***
   ***
";
        var expected = @"
<hr />
<hr />
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example18Line606()
    {
        var input = @"
    ***
";
        var expected = @"
<pre><code>***
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example19Line614Foon()
    {
        var input = @"
Foo
    ***
";
        var expected = @"
<p>Foo
***</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example20Line625()
    {
        var input = @"
_____________________________________
";
        var expected = @"
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example21Line634()
    {
        var input = @"
 - - -
";
        var expected = @"
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example22Line641()
    {
        var input = @"
 **  * ** * ** * **
";
        var expected = @"
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example23Line648()
    {
        var input = @"
-     -      -      -
";
        var expected = @"
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example24Line657()
    {
        var input = @"
- - - -    
";
        var expected = @"
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example25Line666AnnaNnA()
    {
        var input = @"
_ _ _ _ a

a------

---a---
";
        var expected = @"
<p>_ _ _ _ a</p>
<p>a------</p>
<p>---a---</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example26Line682()
    {
        var input = @"
 *-*
";
        var expected = @"
<p><em>-</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example27Line691FoonnBar()
    {
        var input = @"
- foo
***
- bar
";
        var expected = @"
<ul>
<li>foo</li>
</ul>
<hr />
<ul>
<li>bar</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example28Line708Foonnbar()
    {
        var input = @"
Foo
***
bar
";
        var expected = @"
<p>Foo</p>
<hr />
<p>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example29Line725FoonNbar()
    {
        var input = @"
Foo
---
bar
";
        var expected = @"
<h2>Foo</h2>
<p>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example30Line738FoonNBar()
    {
        var input = @"
* Foo
* * *
* Bar
";
        var expected = @"
<ul>
<li>Foo</li>
</ul>
<hr />
<ul>
<li>Bar</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example31Line755Foon()
    {
        var input = @"
- Foo
- * * *
";
        var expected = @"
<ul>
<li>Foo</li>
<li>
<hr />
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example32Line784FoonFoonFoonFoonFoonFoo()
    {
        var input = @"
# foo
## foo
### foo
#### foo
##### foo
###### foo
";
        var expected = @"
<h1>foo</h1>
<h2>foo</h2>
<h3>foo</h3>
<h4>foo</h4>
<h5>foo</h5>
<h6>foo</h6>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example33Line803Foo()
    {
        var input = @"
####### foo
";
        var expected = @"
<p>####### foo</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example34Line8185Boltnnhashtag()
    {
        var input = @"
#5 bolt

#hashtag
";
        var expected = @"
<p>#5 bolt</p>
<p>#hashtag</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example35Line830Foo()
    {
        var input = @"
\## foo
";
        var expected = @"
<p>## foo</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example36Line839FooBarBaz()
    {
        var input = @"
# foo *bar* \*baz\*
";
        var expected = @"
<h1>foo <em>bar</em> *baz*</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example37Line848Foo()
    {
        var input = @"
#                  foo                     
";
        var expected = @"
<h1>foo</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example38Line857FoonFoonFoo()
    {
        var input = @"
 ### foo
  ## foo
   # foo
";
        var expected = @"
<h3>foo</h3>
<h2>foo</h2>
<h1>foo</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example39Line870Foo()
    {
        var input = @"
    # foo
";
        var expected = @"
<pre><code># foo
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example40Line878FoonBar()
    {
        var input = @"
foo
    # bar
";
        var expected = @"
<p>foo
# bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example41Line889FooNBar()
    {
        var input = @"
## foo ##
  ###   bar    ###
";
        var expected = @"
<h2>foo</h2>
<h3>bar</h3>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example42Line900FooNFoo()
    {
        var input = @"
# foo ##################################
##### foo ##
";
        var expected = @"
<h1>foo</h1>
<h5>foo</h5>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example43Line911Foo()
    {
        var input = @"
### foo ###     
";
        var expected = @"
<h3>foo</h3>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example44Line922FooB()
    {
        var input = @"
### foo ### b
";
        var expected = @"
<h3>foo ### b</h3>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example45Line931Foo()
    {
        var input = @"
# foo#
";
        var expected = @"
<h1>foo#</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example46Line941FooNFooNFoo()
    {
        var input = @"
### foo \###
## foo #\##
# foo \#
";
        var expected = @"
<h3>foo ###</h3>
<h2>foo ###</h2>
<h1>foo #</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example47Line955NFoon()
    {
        var input = @"
****
## foo
****
";
        var expected = @"
<hr />
<h2>foo</h2>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example48Line966FooBarnBaznBarFoo()
    {
        var input = @"
Foo bar
# baz
Bar foo
";
        var expected = @"
<p>Foo bar</p>
<h1>baz</h1>
<p>Bar foo</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example49Line979Nn()
    {
        var input = @"
## 
#
### ###
";
        var expected = @"
<h2></h2>
<h1></h1>
<h3></h3>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example50Line1019FooBarnnnFooBarn()
    {
        var input = @"
Foo *bar*
=========

Foo *bar*
---------
";
        var expected = @"
<h1>Foo <em>bar</em></h1>
<h2>Foo <em>bar</em></h2>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example51Line1033FooBarnbazn()
    {
        var input = @"
Foo *bar
baz*
====
";
        var expected = @"
<h1>Foo <em>bar
baz</em></h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example52Line1047FooBarnbazn()
    {
        var input = @"
  Foo *bar
baz*	
====
";
        var expected = @"
<h1>Foo <em>bar
baz</em></h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example53Line1059FoonNnFoon()
    {
        var input = @"
Foo
-------------------------

Foo
==
";
        var expected = @"
<h2>Foo</h2>
<h1>Foo</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example54Line1074FoonNnFoonNnFoon()
    {
        var input = @"
   Foo
---

  Foo
-----

  Foo
  ===
";
        var expected = @"
<h2>Foo</h2>
<h2>Foo</h2>
<h1>Foo</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example55Line1092FoonNnFoon()
    {
        var input = @"
    Foo
    ---

    Foo
---
";
        var expected = @"
<pre><code>Foo
---

Foo
</code></pre>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example56Line1111Foon()
    {
        var input = @"
Foo
   ----      
";
        var expected = @"
<h2>Foo</h2>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example57Line1121Foon()
    {
        var input = @"
Foo
    ---
";
        var expected = @"
<p>Foo
---</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example58Line1132FoonNnFoon()
    {
        var input = @"
Foo
= =

Foo
--- -
";
        var expected = @"
<p>Foo
= =</p>
<p>Foo</p>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example59Line1148FooN()
    {
        var input = @"
Foo  
-----
";
        var expected = @"
<h2>Foo</h2>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example60Line1158Foon()
    {
        var input = @"
Foo\
----
";
        var expected = @"
<h2>Foo\</h2>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example61Line1169FoonNnnaTitleaLotnNofDashes()
    {
        var input = @"
`Foo
----
`

<a title=""a lot
---
of dashes""/>
";
        var expected = @"
<h2>`Foo</h2>
<p>`</p>
<h2>&lt;a title=&quot;a lot</h2>
<p>of dashes&quot;/&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example62Line1188Foon()
    {
        var input = @"
> Foo
---
";
        var expected = @"
<blockquote>
<p>Foo</p>
</blockquote>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example63Line1199Foonbarn()
    {
        var input = @"
> foo
bar
===
";
        var expected = @"
<blockquote>
<p>foo
bar
===</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example64Line1212Foon()
    {
        var input = @"
- Foo
---
";
        var expected = @"
<ul>
<li>Foo</li>
</ul>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example65Line1227FoonBarn()
    {
        var input = @"
Foo
Bar
---
";
        var expected = @"
<h2>Foo
Bar</h2>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example66Line1240FoonNBarnNBaz()
    {
        var input = @"
Foo
---
Bar
---
Baz
";
        var expected = @"
<h2>Foo</h2>
<h2>Bar</h2>
<p>Baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example67Line1257N()
    {
        var input = @"

====
";
        var expected = @"
<p>====</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    // TODO:
    [Ignore]
    [TestMethod]
    public void Example68Line1269N()
    {
        var input = @"
---
---
";
        var expected = @"
<hr />
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example69Line1278Foon()
    {
        var input = @"
- foo
-----
";
        var expected = @"
<ul>
<li>foo</li>
</ul>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example70Line1289Foon()
    {
        var input = @"
    foo
---
";
        var expected = @"
<pre><code>foo
</code></pre>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example71Line1299Foon()
    {
        var input = @"
> foo
-----
";
        var expected = @"
<blockquote>
<p>foo</p>
</blockquote>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example72Line1313Foon()
    {
        var input = @"
\> foo
------
";
        var expected = @"
<h2>&gt; foo</h2>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example73Line1344FoonnbarnNbaz()
    {
        var input = @"
Foo

bar
---
baz
";
        var expected = @"
<p>Foo</p>
<h2>bar</h2>
<p>baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example74Line1360FoonbarnnNnbaz()
    {
        var input = @"
Foo
bar

---

baz
";
        var expected = @"
<p>Foo
bar</p>
<hr />
<p>baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example75Line1378FoonbarnNbaz()
    {
        var input = @"
Foo
bar
* * *
baz
";
        var expected = @"
<p>Foo
bar</p>
<hr />
<p>baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example76Line1393FoonbarnNbaz()
    {
        var input = @"
Foo
bar
\---
baz
";
        var expected = @"
<p>Foo
bar
---
baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example77Line1421ASimplenIndentedCodeBlock()
    {
        var input = @"
    a simple
      indented code block
";
        var expected = @"
<pre><code>a simple
  indented code block
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example78Line1435FoonnBar()
    {
        var input = @"
  - foo

    bar
";
        var expected = @"
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example79Line14491FoonnBar()
    {
        var input = @"
1.  foo

    - bar
";
        var expected = @"
<ol>
<li>
<p>foo</p>
<ul>
<li>bar</li>
</ul>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example80Line1469AnHinnOne()
    {
        var input = @"
    <a/>
    *hi*

    - one
";
        var expected = @"
<pre><code>&lt;a/&gt;
*hi*

- one
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example81Line1485Chunk1nnChunk2nNNNChunk3()
    {
        var input = @"
    chunk1

    chunk2
  
 
 
    chunk3
";
        var expected = @"
<pre><code>chunk1

chunk2



chunk3
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example82Line1508Chunk1nNChunk2()
    {
        var input = @"
    chunk1
      
      chunk2
";
        var expected = @"
<pre><code>chunk1
  
  chunk2
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example83Line1523FoonBarn()
    {
        var input = @"
Foo
    bar

";
        var expected = @"
<p>Foo
bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example84Line1537Foonbar()
    {
        var input = @"
    foo
bar
";
        var expected = @"
<pre><code>foo
</code></pre>
<p>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example85Line1550HeadingnFoonHeadingnNFoon()
    {
        var input = @"
# Heading
    foo
Heading
------
    foo
----
";
        var expected = @"
<h1>Heading</h1>
<pre><code>foo
</code></pre>
<h2>Heading</h2>
<pre><code>foo
</code></pre>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example86Line1570FoonBar()
    {
        var input = @"
        foo
    bar
";
        var expected = @"
<pre><code>    foo
bar
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example87Line1583NNFoonN()
    {
        var input = @"

    
    foo
    

";
        var expected = @"
<pre><code>foo
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example88Line1597Foo()
    {
        var input = @"
    foo  
";
        var expected = @"
<pre><code>foo  
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example89Line1652NnN()
    {
        var input = @"
```
<
 >
```
";
        var expected = @"
<pre><code>&lt;
 &gt;
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example90Line1666NnN()
    {
        var input = @"
~~~
<
 >
~~~
";
        var expected = @"
<pre><code>&lt;
 &gt;
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example91Line1679Nfoon()
    {
        var input = @"
``
foo
``
";
        var expected = @"
<p><code>foo</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example92Line1690Naaann()
    {
        var input = @"
```
aaa
~~~
```
";
        var expected = @"
<pre><code>aaa
~~~
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example93Line1702Naaann()
    {
        var input = @"
~~~
aaa
```
~~~
";
        var expected = @"
<pre><code>aaa
```
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example94Line1716Naaann()
    {
        var input = @"
````
aaa
```
``````
";
        var expected = @"
<pre><code>aaa
```
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example95Line1728Naaann()
    {
        var input = @"
~~~~
aaa
~~~
~~~~
";
        var expected = @"
<pre><code>aaa
~~~
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example96Line1743()
    {
        var input = @"
```
";
        var expected = @"
<pre><code></code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example97Line1750Nnnaaa()
    {
        var input = @"
`````

```
aaa
";
        var expected = @"
<pre><code>
```
aaa
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example98Line1763NAaannbbb()
    {
        var input = @"
> ```
> aaa

bbb
";
        var expected = @"
<blockquote>
<pre><code>aaa
</code></pre>
</blockquote>
<p>bbb</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example99Line1779NnN()
    {
        var input = @"
```

  
```
";
        var expected = @"
<pre><code>
  
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example100Line1793N()
    {
        var input = @"
```
```
";
        var expected = @"
<pre><code></code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example101Line1805NAaanaaan()
    {
        var input = @"
 ```
 aaa
aaa
```
";
        var expected = @"
<pre><code>aaa
aaa
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example102Line1817NaaanAaanaaan()
    {
        var input = @"
  ```
aaa
  aaa
aaa
  ```
";
        var expected = @"
<pre><code>aaa
aaa
aaa
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example103Line1831NAaanAaanAaan()
    {
        var input = @"
   ```
   aaa
    aaa
  aaa
   ```
";
        var expected = @"
<pre><code>aaa
 aaa
aaa
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example104Line1847NAaan()
    {
        var input = @"
    ```
    aaa
    ```
";
        var expected = @"
<pre><code>```
aaa
```
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example105Line1862Naaan()
    {
        var input = @"
```
aaa
  ```
";
        var expected = @"
<pre><code>aaa
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example106Line1872Naaan()
    {
        var input = @"
   ```
aaa
  ```
";
        var expected = @"
<pre><code>aaa
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example107Line1884Naaan()
    {
        var input = @"
```
aaa
    ```
";
        var expected = @"
<pre><code>aaa
    ```
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example108Line1898Naaa()
    {
        var input = @"
``` ```
aaa
";
        var expected = @"
<p><code> </code>
aaa</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example109Line1907Naaan()
    {
        var input = @"
~~~~~~
aaa
~~~ ~~
";
        var expected = @"
<pre><code>aaa
~~~ ~~
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example110Line1921Foonnbarnnbaz()
    {
        var input = @"
foo
```
bar
```
baz
";
        var expected = @"
<p>foo</p>
<pre><code>bar
</code></pre>
<p>baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example111Line1938FoonNnbarnnBaz()
    {
        var input = @"
foo
---
~~~
bar
~~~
# baz
";
        var expected = @"
<h2>foo</h2>
<pre><code>bar
</code></pre>
<h1>baz</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example112Line1960RubyndefFooxnReturn3nendn()
    {
        var input = @"
```ruby
def foo(x)
  return 3
end
```
";
        var expected = @"
<pre><code class=""language-ruby"">def foo(x)
  return 3
end
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example113Line1974RubyStartline3NdefFooxnReturn3nendn()
    {
        var input = @"
~~~~    ruby startline=3 $%@#$
def foo(x)
  return 3
end
~~~~~~~
";
        var expected = @"
<pre><code class=""language-ruby"">def foo(x)
  return 3
end
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example114Line1988N()
    {
        var input = @"
````;
````
";
        var expected = @"
<pre><code class=""language-;""></code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example115Line1998AaNfoo()
    {
        var input = @"
``` aa ```
foo
";
        var expected = @"
<p><code>aa</code>
foo</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example116Line2009AaNfoon()
    {
        var input = @"
~~~ aa ``` ~~~
foo
~~~
";
        var expected = @"
<pre><code class=""language-aa"">foo
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example117Line2021NAaan()
    {
        var input = @"
```
``` aaa
```
";
        var expected = @"
<pre><code>``` aaa
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example118Line2100TabletrtdnprenHellonnworldnprentdtrtable()
    {
        var input = @"
<table><tr><td>
<pre>
**Hello**,

_world_.
</pre>
</td></tr></table>
";
        var expected = @"
<table><tr><td>
<pre>
**Hello**,
<p><em>world</em>.
</pre></p>
</td></tr></table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example119Line2129TablenTrnTdnHinTdnTrntablennokay()
    {
        var input = @"
<table>
  <tr>
    <td>
           hi
    </td>
  </tr>
</table>

okay.
";
        var expected = @"
<table>
  <tr>
    <td>
           hi
    </td>
  </tr>
</table>
<p>okay.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example120Line2151DivnHellonFooa()
    {
        var input = @"
 <div>
  *hello*
         <foo><a>
";
        var expected = @"
 <div>
  *hello*
         <foo><a>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example121Line2164Divnfoo()
    {
        var input = @"
</div>
*foo*
";
        var expected = @"
</div>
*foo*
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example122Line2175DIVCLASSfoonnMarkdownnnDIV()
    {
        var input = @"
<DIV CLASS=""foo"">

*Markdown*

</DIV>
";
        var expected = @"
<DIV CLASS=""foo"">
<p><em>Markdown</em></p>
</DIV>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example123Line2191DivIdfoonClassbarndiv()
    {
        var input = @"
<div id=""foo""
  class=""bar"">
</div>
";
        var expected = @"
<div id=""foo""
  class=""bar"">
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example124Line2202DivIdfooClassbarnBazndiv()
    {
        var input = @"
<div id=""foo"" class=""bar
  baz"">
</div>
";
        var expected = @"
<div id=""foo"" class=""bar
  baz"">
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example125Line2214Divnfoonnbar()
    {
        var input = @"
<div>
*foo*

*bar*
";
        var expected = @"
<div>
*foo*
<p><em>bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example126Line2230DivIdfoonhi()
    {
        var input = @"
<div id=""foo""
*hi*
";
        var expected = @"
<div id=""foo""
*hi*
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example127Line2239DivClassnfoo()
    {
        var input = @"
<div class
foo
";
        var expected = @"
<div class
foo
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example128Line2251DivNfoo()
    {
        var input = @"
<div *???-&&&-<---
*foo*
";
        var expected = @"
<div *???-&&&-<---
*foo*
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example129Line2263DivaHrefbarfooadiv()
    {
        var input = @"
<div><a href=""bar"">*foo*</a></div>
";
        var expected = @"
<div><a href=""bar"">*foo*</a></div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example130Line2270Tabletrtdnfoontdtrtable()
    {
        var input = @"
<table><tr><td>
foo
</td></tr></table>
";
        var expected = @"
<table><tr><td>
foo
</td></tr></table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example131Line2287DivdivnCnintX33n()
    {
        var input = @"
<div></div>
``` c
int x = 33;
```
";
        var expected = @"
<div></div>
``` c
int x = 33;
```
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example132Line2304AHreffoonbarna()
    {
        var input = @"
<a href=""foo"">
*bar*
</a>
";
        var expected = @"
<a href=""foo"">
*bar*
</a>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example133Line2317WarningnbarnWarning()
    {
        var input = @"
<Warning>
*bar*
</Warning>
";
        var expected = @"
<Warning>
*bar*
</Warning>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example134Line2328IClassfoonbarni()
    {
        var input = @"
<i class=""foo"">
*bar*
</i>
";
        var expected = @"
<i class=""foo"">
*bar*
</i>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example135Line2339Insnbar()
    {
        var input = @"
</ins>
*bar*
";
        var expected = @"
</ins>
*bar*
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example136Line2354Delnfoondel()
    {
        var input = @"
<del>
*foo*
</del>
";
        var expected = @"
<del>
*foo*
</del>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example137Line2369Delnnfoonndel()
    {
        var input = @"
<del>

*foo*

</del>
";
        var expected = @"
<del>
<p><em>foo</em></p>
</del>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example138Line2387Delfoodel()
    {
        var input = @"
<del>*foo*</del>
";
        var expected = @"
<p><del><em>foo</em></del></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example139Line2403PreLanguagehaskellcodenimportTextHTMLTagSoupnnmainIONmainPrintParseTagsTagsncodeprenokay()
    {
        var input = @"
<pre language=""haskell""><code>
import Text.HTML.TagSoup

main :: IO ()
main = print $ parseTags tags
</code></pre>
okay
";
        var expected = @"
<pre language=""haskell""><code>
import Text.HTML.TagSoup

main :: IO ()
main = print $ parseTags tags
</code></pre>
<p>okay</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example140Line2424ScriptTypetextjavascriptnJavaScriptExamplenndocumentgetElementByIddemoinnerHTMLHelloJavaScriptnscriptnokay()
    {
        var input = @"
<script type=""text/javascript"">
// JavaScript example

document.getElementById(""demo"").innerHTML = ""Hello JavaScript!"";
</script>
okay
";
        var expected = @"
<script type=""text/javascript"">
// JavaScript example

document.getElementById(""demo"").innerHTML = ""Hello JavaScript!"";
</script>
<p>okay</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example141Line2443StylenTypetextcssnh1ColorrednnpColorbluenstylenokay()
    {
        var input = @"
<style
  type=""text/css"">
h1 {color:red;}

p {color:blue;}
</style>
okay
";
        var expected = @"
<style
  type=""text/css"">
h1 {color:red;}

p {color:blue;}
</style>
<p>okay</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example142Line2466StylenTypetextcssnnfoo()
    {
        var input = @"
<style
  type=""text/css"">

foo
";
        var expected = @"
<style
  type=""text/css"">

foo
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example143Line2479DivnFoonnbar()
    {
        var input = @"
> <div>
> foo

bar
";
        var expected = @"
<blockquote>
<div>
foo
</blockquote>
<p>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example144Line2493DivnFoo()
    {
        var input = @"
- <div>
- foo
";
        var expected = @"
<ul>
<li>
<div>
</li>
<li>foo</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example145Line2508Stylepcolorredstylenfoo()
    {
        var input = @"
<style>p{color:red;}</style>
*foo*
";
        var expected = @"
<style>p{color:red;}</style>
<p><em>foo</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example146Line2517FooBarnbaz()
    {
        var input = @"
<!-- foo -->*bar*
*baz*
";
        var expected = @"
<!-- foo -->*bar*
<p><em>baz</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example147Line2529Scriptnfoonscript1Bar()
    {
        var input = @"
<script>
foo
</script>1. *bar*
";
        var expected = @"
<script>
foo
</script>1. *bar*
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example148Line2542FoonnbarnBazNokay()
    {
        var input = @"
<!-- Foo

bar
   baz -->
okay
";
        var expected = @"
<!-- Foo

bar
   baz -->
<p>okay</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example149Line2560PhpnnEchoNnnokay()
    {
        var input = @"
<?php

  echo '>';

?>
okay
";
        var expected = @"
<?php

  echo '>';

?>
<p>okay</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example150Line2579DOCTYPEHtml()
    {
        var input = @"
<!DOCTYPE html>
";
        var expected = @"
<!DOCTYPE html>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example151Line2588CDATAnfunctionMatchwoabnnIfABA0ThenNReturn1nnElseNnReturn0nNnnokay()
    {
        var input = @"
<![CDATA[
function matchwo(a,b)
{
  if (a < b && a < 0) then {
    return 1;

  } else {

    return 0;
  }
}
]]>
okay
";
        var expected = @"
<![CDATA[
function matchwo(a,b)
{
  if (a < b && a < 0) then {
    return 1;

  } else {

    return 0;
  }
}
]]>
<p>okay</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example152Line2621FooNnFoo()
    {
        var input = @"
  <!-- foo -->

    <!-- foo -->
";
        var expected = @"
  <!-- foo -->
<pre><code>&lt;!-- foo --&gt;
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example153Line2632DivnnDiv()
    {
        var input = @"
  <div>

    <div>
";
        var expected = @"
  <div>
<pre><code>&lt;div&gt;
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example154Line2646Foondivnbarndiv()
    {
        var input = @"
Foo
<div>
bar
</div>
";
        var expected = @"
<p>Foo</p>
<div>
bar
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example155Line2663Divnbarndivnfoo()
    {
        var input = @"
<div>
bar
</div>
*foo*
";
        var expected = @"
<div>
bar
</div>
*foo*
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example156Line2678FoonaHrefbarnbaz()
    {
        var input = @"
Foo
<a href=""bar"">
baz
";
        var expected = @"
<p>Foo
<a href=""bar"">
baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example157Line2719DivnnEmphasizedTextnndiv()
    {
        var input = @"
<div>

*Emphasized* text.

</div>
";
        var expected = @"
<div>
<p><em>Emphasized</em> text.</p>
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example158Line2732DivnEmphasizedTextndiv()
    {
        var input = @"
<div>
*Emphasized* text.
</div>
";
        var expected = @"
<div>
*Emphasized* text.
</div>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example159Line2754TablenntrnntdnHintdnntrnntable()
    {
        var input = @"
<table>

<tr>

<td>
Hi
</td>

</tr>

</table>
";
        var expected = @"
<table>
<tr>
<td>
Hi
</td>
</tr>
</table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example160Line2781TablennTrnnTdnHinTdnnTrnntable()
    {
        var input = @"
<table>

  <tr>

    <td>
      Hi
    </td>

  </tr>

</table>
";
        var expected = @"
<table>
  <tr>
<pre><code>&lt;td&gt;
  Hi
&lt;/td&gt;
</code></pre>
  </tr>
</table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example161Line2829FooUrlTitlennfoo()
    {
        var input = @"
[foo]: /url ""title""

[foo]
";
        var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example162Line2838FooNUrlNTheTitleNnfoo()
    {
        var input = @"
   [foo]: 
      /url  
           'the title'  

[foo]
";
        var expected = @"
<p><a href=""/url"" title=""the title"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example163Line2849FoobarmyurlTitleWithParensnnFoobar()
    {
        var input = @"
[Foo*bar\]]:my_(url) 'title (with parens)'

[Foo*bar\]]
";
        var expected = @"
<p><a href=""my_(url)"" title=""title (with parens)"">Foo*bar]</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example164Line2858FooBarnmyUrlntitlennFooBar()
    {
        var input = @"
[Foo bar]:
<my url>
'title'

[Foo bar]
";
        var expected = @"
<p><a href=""my%20url"" title=""title"">Foo bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example165Line2871FooUrlNtitlenline1nline2nnnfoo()
    {
        var input = @"
[foo]: /url '
title
line1
line2
'

[foo]
";
        var expected = @"
<p><a href=""/url"" title=""
title
line1
line2
"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example166Line2890FooUrlTitlennwithBlankLinennfoo()
    {
        var input = @"
[foo]: /url 'title

with blank line'

[foo]
";
        var expected = @"
<p>[foo]: /url 'title</p>
<p>with blank line'</p>
<p>[foo]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example167Line2905Foonurlnnfoo()
    {
        var input = @"
[foo]:
/url

[foo]
";
        var expected = @"
<p><a href=""/url"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example168Line2917Foonnfoo()
    {
        var input = @"
[foo]:

[foo]
";
        var expected = @"
<p>[foo]:</p>
<p>[foo]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example169Line2929FooNnfoo()
    {
        var input = @"
[foo]: <>

[foo]
";
        var expected = @"
<p><a href="""">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example170Line2940FooBarbaznnfoo()
    {
        var input = @"
[foo]: <bar>(baz)

[foo]
";
        var expected = @"
<p>[foo]: <bar>(baz)</p>
<p>[foo]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example171Line2953FooUrlbarbazFoobarbaznnfoo()
    {
        var input = @"
[foo]: /url\bar\*baz ""foo\""bar\baz""

[foo]
";
        var expected = @"
<p><a href=""/url%5Cbar*baz"" title=""foo&quot;bar\baz"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example172Line2964FoonnfooUrl()
    {
        var input = @"
[foo]

[foo]: url
";
        var expected = @"
<p><a href=""url"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example173Line2976FoonnfooFirstnfooSecond()
    {
        var input = @"
[foo]

[foo]: first
[foo]: second
";
        var expected = @"
<p><a href=""first"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example174Line2989FOOUrlnnFoo()
    {
        var input = @"
[FOO]: /url

[Foo]
";
        var expected = @"
<p><a href=""/url"">Foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example175Line2998Nn()
    {
        var input = @"
[ΑΓΩ]: /φου

[αγω]
";
        var expected = @"
<p><a href=""/%CF%86%CE%BF%CF%85"">αγω</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example176Line3010FooUrl()
    {
        var input = @"
[foo]: /url
";
        var expected = @"
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example177Line3018NfoonUrlnbar()
    {
        var input = @"
[
foo
]: /url
bar
";
        var expected = @"
<p>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example178Line3031FooUrlTitleOk()
    {
        var input = @"
[foo]: /url ""title"" ok
";
        var expected = @"
<p>[foo]: /url &quot;title&quot; ok</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example179Line3040FooUrlntitleOk()
    {
        var input = @"
[foo]: /url
""title"" ok
";
        var expected = @"
<p>&quot;title&quot; ok</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example180Line3051FooUrlTitlennfoo()
    {
        var input = @"
    [foo]: /url ""title""

[foo]
";
        var expected = @"
<pre><code>[foo]: /url &quot;title&quot;
</code></pre>
<p>[foo]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example181Line3065NfooUrlnnnfoo()
    {
        var input = @"
```
[foo]: /url
```

[foo]
";
        var expected = @"
<pre><code>[foo]: /url
</code></pre>
<p>[foo]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example182Line3080FoonbarBaznnbar()
    {
        var input = @"
Foo
[bar]: /baz

[bar]
";
        var expected = @"
<p>Foo
[bar]: /baz</p>
<p>[bar]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example183Line3095FoonfooUrlnBar()
    {
        var input = @"
# [Foo]
[foo]: /url
> bar
";
        var expected = @"
<h1><a href=""/url"">Foo</a></h1>
<blockquote>
<p>bar</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example184Line3106FooUrlnbarnnfoo()
    {
        var input = @"
[foo]: /url
bar
===
[foo]
";
        var expected = @"
<h1>bar</h1>
<p><a href=""/url"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example185Line3116FooUrlnnfoo()
    {
        var input = @"
[foo]: /url
===
[foo]
";
        var expected = @"
<p>===
<a href=""/url"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example186Line3129FooFooUrlFoonbarBarUrlnBarnbazBazUrlnnfoonbarnbaz()
    {
        var input = @"
[foo]: /foo-url ""foo""
[bar]: /bar-url
  ""bar""
[baz]: /baz-url

[foo],
[bar],
[baz]
";
        var expected = @"
<p><a href=""/foo-url"" title=""foo"">foo</a>,
<a href=""/bar-url"" title=""bar"">bar</a>,
<a href=""/baz-url"">baz</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example187Line3150FoonnFooUrl()
    {
        var input = @"
[foo]

> [foo]: /url
";
        var expected = @"
<p><a href=""/url"">foo</a></p>
<blockquote>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example188Line3167FooUrl()
    {
        var input = @"
[foo]: /url
";
        var expected = @"
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example189Line3184Aaannbbb()
    {
        var input = @"
aaa

bbb
";
        var expected = @"
<p>aaa</p>
<p>bbb</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example190Line3196Aaanbbbnncccnddd()
    {
        var input = @"
aaa
bbb

ccc
ddd
";
        var expected = @"
<p>aaa
bbb</p>
<p>ccc
ddd</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example191Line3212Aaannnbbb()
    {
        var input = @"
aaa


bbb
";
        var expected = @"
<p>aaa</p>
<p>bbb</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example192Line3225AaanBbb()
    {
        var input = @"
  aaa
 bbb
";
        var expected = @"
<p>aaa
bbb</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example193Line3237AaanBbbnCcc()
    {
        var input = @"
aaa
             bbb
                                       ccc
";
        var expected = @"
<p>aaa
bbb
ccc</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example194Line3251Aaanbbb()
    {
        var input = @"
   aaa
bbb
";
        var expected = @"
<p>aaa
bbb</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example195Line3260Aaanbbb()
    {
        var input = @"
    aaa
bbb
";
        var expected = @"
<pre><code>aaa
</code></pre>
<p>bbb</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example196Line3274AaaNbbb()
    {
        var input = @"
aaa     
bbb     
";
        var expected = @"
<p>aaa<br />
bbb</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example197Line3291NnaaanNnAaann()
    {
        var input = @"
  

aaa
  

# aaa

  
";
        var expected = @"
<p>aaa</p>
<h1>aaa</h1>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example198Line3326FooBarNNBazBim()
    {
        var input = @"
| foo | bar |
| --- | --- |
| baz | bim |
";
        var expected = @"
<table>
<thead>
<tr>
<th>foo</th>
<th>bar</th>
</tr>
</thead>
<tbody>
<tr>
<td>baz</td>
<td>bim</td>
</tr>
</tbody>
</table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example199Line3350AbcDefghiNNbarBaz()
    {
        var input = @"
| abc | defghi |
:-: | -----------:
bar | baz
";
        var expected = @"
<table>
<thead>
<tr>
<th align=""center"">abc</th>
<th align=""right"">defghi</th>
</tr>
</thead>
<tbody>
<tr>
<td align=""center"">bar</td>
<td align=""right"">baz</td>
</tr>
</tbody>
</table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example200Line3374FooNNBAzNBIm()
    {
        var input = @"
| f\|oo  |
| ------ |
| b `\|` az |
| b **\|** im |
";
        var expected = @"
<table>
<thead>
<tr>
<th>f|oo</th>
</tr>
</thead>
<tbody>
<tr>
<td>b <code>|</code> az</td>
</tr>
<tr>
<td>b <strong>|</strong> im</td>
</tr>
</tbody>
</table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example201Line3400AbcDefNNBarBazNBar()
    {
        var input = @"
| abc | def |
| --- | --- |
| bar | baz |
> bar
";
        var expected = @"
<table>
<thead>
<tr>
<th>abc</th>
<th>def</th>
</tr>
</thead>
<tbody>
<tr>
<td>bar</td>
<td>baz</td>
</tr>
</tbody>
</table>
<blockquote>
<p>bar</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example202Line3425AbcDefNNBarBazNbarnnbar()
    {
        var input = @"
| abc | def |
| --- | --- |
| bar | baz |
bar

bar
";
        var expected = @"
<table>
<thead>
<tr>
<th>abc</th>
<th>def</th>
</tr>
</thead>
<tbody>
<tr>
<td>bar</td>
<td>baz</td>
</tr>
<tr>
<td>bar</td>
<td></td>
</tr>
</tbody>
</table>
<p>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example203Line3457AbcDefNNBar()
    {
        var input = @"
| abc | def |
| --- |
| bar |
";
        var expected = @"
<p>| abc | def |
| --- |
| bar |</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example204Line3471AbcDefNNBarNBarBazBoo()
    {
        var input = @"
| abc | def |
| --- | --- |
| bar |
| bar | baz | boo |
";
        var expected = @"
<table>
<thead>
<tr>
<th>abc</th>
<th>def</th>
</tr>
</thead>
<tbody>
<tr>
<td>bar</td>
<td></td>
</tr>
<tr>
<td>bar</td>
<td>baz</td>
</tr>
</tbody>
</table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example205Line3499AbcDefN()
    {
        var input = @"
| abc | def |
| --- | --- |
";
        var expected = @"
<table>
<thead>
<tr>
<th>abc</th>
<th>def</th>
</tr>
</thead>
</table>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example206Line3565FoonBarnBaz()
    {
        var input = @"
> # Foo
> bar
> baz
";
        var expected = @"
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example207Line3580FoonbarnBaz()
    {
        var input = @"
># Foo
>bar
> baz
";
        var expected = @"
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example208Line3595FoonBarnBaz()
    {
        var input = @"
   > # Foo
   > bar
 > baz
";
        var expected = @"
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example209Line3610FoonBarnBaz()
    {
        var input = @"
    > # Foo
    > bar
    > baz
";
        var expected = @"
<pre><code>&gt; # Foo
&gt; bar
&gt; baz
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example210Line3625FoonBarnbaz()
    {
        var input = @"
> # Foo
> bar
baz
";
        var expected = @"
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example211Line3641BarnbaznFoo()
    {
        var input = @"
> bar
baz
> foo
";
        var expected = @"
<blockquote>
<p>bar
baz
foo</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example212Line3665Foon()
    {
        var input = @"
> foo
---
";
        var expected = @"
<blockquote>
<p>foo</p>
</blockquote>
<hr />
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example213Line3685FoonBar()
    {
        var input = @"
> - foo
- bar
";
        var expected = @"
<blockquote>
<ul>
<li>foo</li>
</ul>
</blockquote>
<ul>
<li>bar</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example214Line3703FoonBar()
    {
        var input = @"
>     foo
    bar
";
        var expected = @"
<blockquote>
<pre><code>foo
</code></pre>
</blockquote>
<pre><code>bar
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example215Line3716Nfoon()
    {
        var input = @"
> ```
foo
```
";
        var expected = @"
<blockquote>
<pre><code></code></pre>
</blockquote>
<p>foo</p>
<pre><code></code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example216Line3732FoonBar()
    {
        var input = @"
> foo
    - bar
";
        var expected = @"
<blockquote>
<p>foo
- bar</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example217Line3756()
    {
        var input = @"
>
";
        var expected = @"
<blockquote>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example218Line3764NN()
    {
        var input = @"
>
>  
> 
";
        var expected = @"
<blockquote>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example219Line3776NFoon()
    {
        var input = @"
>
> foo
>  
";
        var expected = @"
<blockquote>
<p>foo</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example220Line3789FoonnBar()
    {
        var input = @"
> foo

> bar
";
        var expected = @"
<blockquote>
<p>foo</p>
</blockquote>
<blockquote>
<p>bar</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example221Line3811FoonBar()
    {
        var input = @"
> foo
> bar
";
        var expected = @"
<blockquote>
<p>foo
bar</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example222Line3824FoonnBar()
    {
        var input = @"
> foo
>
> bar
";
        var expected = @"
<blockquote>
<p>foo</p>
<p>bar</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example223Line3838FoonBar()
    {
        var input = @"
foo
> bar
";
        var expected = @"
<p>foo</p>
<blockquote>
<p>bar</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example224Line3852AaannBbb()
    {
        var input = @"
> aaa
***
> bbb
";
        var expected = @"
<blockquote>
<p>aaa</p>
</blockquote>
<hr />
<blockquote>
<p>bbb</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example225Line3870Barnbaz()
    {
        var input = @"
> bar
baz
";
        var expected = @"
<blockquote>
<p>bar
baz</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example226Line3881Barnnbaz()
    {
        var input = @"
> bar

baz
";
        var expected = @"
<blockquote>
<p>bar</p>
</blockquote>
<p>baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example227Line3893Barnnbaz()
    {
        var input = @"
> bar
>
baz
";
        var expected = @"
<blockquote>
<p>bar</p>
</blockquote>
<p>baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example228Line3909Foonbar()
    {
        var input = @"
> > > foo
bar
";
        var expected = @"
<blockquote>
<blockquote>
<blockquote>
<p>foo
bar</p>
</blockquote>
</blockquote>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example229Line3924FoonBarnbaz()
    {
        var input = @"
>>> foo
> bar
>>baz
";
        var expected = @"
<blockquote>
<blockquote>
<blockquote>
<p>foo
bar
baz</p>
</blockquote>
</blockquote>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example230Line3946CodennNotCode()
    {
        var input = @"
>     code

>    not code
";
        var expected = @"
<blockquote>
<pre><code>code
</code></pre>
</blockquote>
<blockquote>
<p>not code</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example231Line4000AParagraphnwithTwoLinesnnIndentedCodennABlockQuote()
    {
        var input = @"
A paragraph
with two lines.

    indented code

> A block quote.
";
        var expected = @"
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example232Line40221AParagraphnWithTwoLinesnnIndentedCodennABlockQuote()
    {
        var input = @"
1.  A paragraph
    with two lines.

        indented code

    > A block quote.
";
        var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example233Line4055OnennTwo()
    {
        var input = @"
- one

 two
";
        var expected = @"
<ul>
<li>one</li>
</ul>
<p>two</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example234Line4067OnennTwo()
    {
        var input = @"
- one

  two
";
        var expected = @"
<ul>
<li>
<p>one</p>
<p>two</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example235Line4081OnennTwo()
    {
        var input = @"
 -    one

     two
";
        var expected = @"
<ul>
<li>one</li>
</ul>
<pre><code> two
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example236Line4094OnennTwo()
    {
        var input = @"
 -    one

      two
";
        var expected = @"
<ul>
<li>
<p>one</p>
<p>two</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example237Line41161OnennTwo()
    {
        var input = @"
   > > 1.  one
>>
>>     two
";
        var expected = @"
<blockquote>
<blockquote>
<ol>
<li>
<p>one</p>
<p>two</p>
</li>
</ol>
</blockquote>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example238Line4143OnennTwo()
    {
        var input = @"
>>- one
>>
  >  > two
";
        var expected = @"
<blockquote>
<blockquote>
<ul>
<li>one</li>
</ul>
<p>two</p>
</blockquote>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example239Line4162Onenn2two()
    {
        var input = @"
-one

2.two
";
        var expected = @"
<p>-one</p>
<p>2.two</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example240Line4175FoonnnBar()
    {
        var input = @"
- foo


  bar
";
        var expected = @"
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example241Line41921FoonnNBarnNnBaznnBam()
    {
        var input = @"
1.  foo

    ```
    bar
    ```

    baz

    > bam
";
        var expected = @"
<ol>
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
<p>baz</p>
<blockquote>
<p>bam</p>
</blockquote>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example242Line4220FoonnBarnnnBaz()
    {
        var input = @"
- Foo

      bar


      baz
";
        var expected = @"
<ul>
<li>
<p>Foo</p>
<pre><code>bar


baz
</code></pre>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example243Line4242123456789Ok()
    {
        var input = @"
123456789. ok
";
        var expected = @"
<ol start=""123456789"">
<li>ok</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example244Line42511234567890NotOk()
    {
        var input = @"
1234567890. not ok
";
        var expected = @"
<p>1234567890. not ok</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example245Line42600Ok()
    {
        var input = @"
0. ok
";
        var expected = @"
<ol start=""0"">
<li>ok</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example246Line4269003Ok()
    {
        var input = @"
003. ok
";
        var expected = @"
<ol start=""3"">
<li>ok</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example247Line42801NotOk()
    {
        var input = @"
-1. not ok
";
        var expected = @"
<p>-1. not ok</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example248Line4303FoonnBar()
    {
        var input = @"
- foo

      bar
";
        var expected = @"
<ul>
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example249Line432010FoonnBar()
    {
        var input = @"
  10.  foo

           bar
";
        var expected = @"
<ol start=""10"">
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example250Line4339IndentedCodennparagraphnnMoreCode()
    {
        var input = @"
    indented code

paragraph

    more code
";
        var expected = @"
<pre><code>indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example251Line43541IndentedCodennParagraphnnMoreCode()
    {
        var input = @"
1.     indented code

   paragraph

       more code
";
        var expected = @"
<ol>
<li>
<pre><code>indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example252Line43761IndentedCodennParagraphnnMoreCode()
    {
        var input = @"
1.      indented code

   paragraph

       more code
";
        var expected = @"
<ol>
<li>
<pre><code> indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example253Line4403Foonnbar()
    {
        var input = @"
   foo

bar
";
        var expected = @"
<p>foo</p>
<p>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example254Line4413FoonnBar()
    {
        var input = @"
-    foo

  bar
";
        var expected = @"
<ul>
<li>foo</li>
</ul>
<p>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example255Line4430FoonnBar()
    {
        var input = @"
-  foo

   bar
";
        var expected = @"
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example256Line4458NFoonNNBarnNNBaz()
    {
        var input = @"
-
  foo
-
  ```
  bar
  ```
-
      baz
";
        var expected = @"
<ul>
<li>foo</li>
<li>
<pre><code>bar
</code></pre>
</li>
<li>
<pre><code>baz
</code></pre>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example257Line4484NFoo()
    {
        var input = @"
-   
  foo
";
        var expected = @"
<ul>
<li>foo</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example258Line4498NnFoo()
    {
        var input = @"
-

  foo
";
        var expected = @"
<ul>
<li></li>
</ul>
<p>foo</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example259Line4512FoonNBar()
    {
        var input = @"
- foo
-
- bar
";
        var expected = @"
<ul>
<li>foo</li>
<li></li>
<li>bar</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example260Line4527FoonNBar()
    {
        var input = @"
- foo
-   
- bar
";
        var expected = @"
<ul>
<li>foo</li>
<li></li>
<li>bar</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example261Line45421Foon2n3Bar()
    {
        var input = @"
1. foo
2.
3. bar
";
        var expected = @"
<ol>
<li>foo</li>
<li></li>
<li>bar</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example262Line4557()
    {
        var input = @"
*
";
        var expected = @"
<ul>
<li></li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example263Line4567Foonnnfoon1()
    {
        var input = @"
foo
*

foo
1.
";
        var expected = @"
<p>foo
*</p>
<p>foo
1.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example264Line45891AParagraphnWithTwoLinesnnIndentedCodennABlockQuote()
    {
        var input = @"
 1.  A paragraph
     with two lines.

         indented code

     > A block quote.
";
        var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example265Line46131AParagraphnWithTwoLinesnnIndentedCodennABlockQuote()
    {
        var input = @"
  1.  A paragraph
      with two lines.

          indented code

      > A block quote.
";
        var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example266Line46371AParagraphnWithTwoLinesnnIndentedCodennABlockQuote()
    {
        var input = @"
   1.  A paragraph
       with two lines.

           indented code

       > A block quote.
";
        var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example267Line46611AParagraphnWithTwoLinesnnIndentedCodennABlockQuote()
    {
        var input = @"
    1.  A paragraph
        with two lines.

            indented code

        > A block quote.
";
        var expected = @"
<pre><code>1.  A paragraph
    with two lines.

        indented code

    &gt; A block quote.
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example268Line46911AParagraphnwithTwoLinesnnIndentedCodennABlockQuote()
    {
        var input = @"
  1.  A paragraph
with two lines.

          indented code

      > A block quote.
";
        var expected = @"
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example269Line47151AParagraphnWithTwoLines()
    {
        var input = @"
  1.  A paragraph
    with two lines.
";
        var expected = @"
<ol>
<li>A paragraph
with two lines.</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example270Line47281BlockquotencontinuedHere()
    {
        var input = @"
> 1. > Blockquote
continued here.
";
        var expected = @"
<blockquote>
<ol>
<li>
<blockquote>
<p>Blockquote
continued here.</p>
</blockquote>
</li>
</ol>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example271Line47451BlockquotenContinuedHere()
    {
        var input = @"
> 1. > Blockquote
> continued here.
";
        var expected = @"
<blockquote>
<ol>
<li>
<blockquote>
<p>Blockquote
continued here.</p>
</blockquote>
</li>
</ol>
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example272Line4773FoonBarnBaznBoo()
    {
        var input = @"
- foo
  - bar
    - baz
      - boo
";
        var expected = @"
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>baz
<ul>
<li>boo</li>
</ul>
</li>
</ul>
</li>
</ul>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example273Line4799FoonBarnBaznBoo()
    {
        var input = @"
- foo
 - bar
  - baz
   - boo
";
        var expected = @"
<ul>
<li>foo</li>
<li>bar</li>
<li>baz</li>
<li>boo</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example274Line481610FoonBar()
    {
        var input = @"
10) foo
    - bar
";
        var expected = @"
<ol start=""10"">
<li>foo
<ul>
<li>bar</li>
</ul>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example275Line483210FoonBar()
    {
        var input = @"
10) foo
   - bar
";
        var expected = @"
<ol start=""10"">
<li>foo</li>
</ol>
<ul>
<li>bar</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example276Line4847Foo()
    {
        var input = @"
- - foo
";
        var expected = @"
<ul>
<li>
<ul>
<li>foo</li>
</ul>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example277Line486012Foo()
    {
        var input = @"
1. - 2. foo
";
        var expected = @"
<ol>
<li>
<ul>
<li>
<ol start=""2"">
<li>foo</li>
</ol>
</li>
</ul>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example278Line4879FoonBarnNBaz()
    {
        var input = @"
- # Foo
- Bar
  ---
  baz
";
        var expected = @"
<ul>
<li>
<h1>Foo</h1>
</li>
<li>
<h2>Bar</h2>
baz</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example279Line5108FoonXBar()
    {
        var input = @"
- [ ] foo
- [x] bar
";
        var expected = @"
<ul>
<li><input type=""checkbox"" disabled="""" /> foo</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> bar</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example280Line5120XFoonBarnXBaznBim()
    {
        var input = @"
- [x] foo
  - [ ] bar
  - [x] baz
- [ ] bim
";
        var expected = @"
<ul>
<li><input type=""checkbox"" checked="""" disabled="""" /> foo
<ul>
<li><input type=""checkbox"" disabled="""" /> bar</li>
<li><input type=""checkbox"" checked="""" disabled="""" /> baz</li>
</ul>
</li>
<li><input type=""checkbox"" disabled="""" /> bim</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example281Line5172FoonBarnBaz()
    {
        var input = @"
- foo
- bar
+ baz
";
        var expected = @"
<ul>
<li>foo</li>
<li>bar</li>
</ul>
<ul>
<li>baz</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example282Line51871Foon2Barn3Baz()
    {
        var input = @"
1. foo
2. bar
3) baz
";
        var expected = @"
<ol>
<li>foo</li>
<li>bar</li>
</ol>
<ol start=""3"">
<li>baz</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example283Line5206FoonBarnBaz()
    {
        var input = @"
Foo
- bar
- baz
";
        var expected = @"
<p>Foo</p>
<ul>
<li>bar</li>
<li>baz</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example284Line5283TheNumberOfWindowsInMyHouseIsn14TheNumberOfDoorsIs6()
    {
        var input = @"
The number of windows in my house is
14.  The number of doors is 6.
";
        var expected = @"
<p>The number of windows in my house is
14.  The number of doors is 6.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example285Line5293TheNumberOfWindowsInMyHouseIsn1TheNumberOfDoorsIs6()
    {
        var input = @"
The number of windows in my house is
1.  The number of doors is 6.
";
        var expected = @"
<p>The number of windows in my house is</p>
<ol>
<li>The number of doors is 6.</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example286Line5307FoonnBarnnnBaz()
    {
        var input = @"
- foo

- bar


- baz
";
        var expected = @"
<ul>
<li>
<p>foo</p>
</li>
<li>
<p>bar</p>
</li>
<li>
<p>baz</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example287Line5328FoonBarnBaznnnBim()
    {
        var input = @"
- foo
  - bar
    - baz


      bim
";
        var expected = @"
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>
<p>baz</p>
<p>bim</p>
</li>
</ul>
</li>
</ul>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example288Line5358FoonBarnnNnBaznBim()
    {
        var input = @"
- foo
- bar

<!-- -->

- baz
- bim
";
        var expected = @"
<ul>
<li>foo</li>
<li>bar</li>
</ul>
<!-- -->
<ul>
<li>baz</li>
<li>bim</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example289Line5379FoonnNotcodennFoonnNnCode()
    {
        var input = @"
-   foo

    notcode

-   foo

<!-- -->

    code
";
        var expected = @"
<ul>
<li>
<p>foo</p>
<p>notcode</p>
</li>
<li>
<p>foo</p>
</li>
</ul>
<!-- -->
<pre><code>code
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example290Line5410AnBnCnDnEnFnG()
    {
        var input = @"
- a
 - b
  - c
   - d
  - e
 - f
- g
";
        var expected = @"
<ul>
<li>a</li>
<li>b</li>
<li>c</li>
<li>d</li>
<li>e</li>
<li>f</li>
<li>g</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example291Line54311Ann2Bnn3C()
    {
        var input = @"
1. a

  2. b

   3. c
";
        var expected = @"
<ol>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>c</p>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example292Line5455AnBnCnDnE()
    {
        var input = @"
- a
 - b
  - c
   - d
    - e
";
        var expected = @"
<ul>
<li>a</li>
<li>b</li>
<li>c</li>
<li>d
- e</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example293Line54751Ann2Bnn3C()
    {
        var input = @"
1. a

  2. b

    3. c
";
        var expected = @"
<ol>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
</ol>
<pre><code>3. c
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example294Line5498AnBnnC()
    {
        var input = @"
- a
- b

- c
";
        var expected = @"
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>c</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example295Line5520AnnnC()
    {
        var input = @"
* a
*

* c
";
        var expected = @"
<ul>
<li>
<p>a</p>
</li>
<li></li>
<li>
<p>c</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example296Line5542AnBnnCnD()
    {
        var input = @"
- a
- b

  c
- d
";
        var expected = @"
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
<p>c</p>
</li>
<li>
<p>d</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example297Line5564AnBnnRefUrlnD()
    {
        var input = @"
- a
- b

  [ref]: /url
- d
";
        var expected = @"
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>d</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example298Line5587AnNBnnnNC()
    {
        var input = @"
- a
- ```
  b


  ```
- c
";
        var expected = @"
<ul>
<li>a</li>
<li>
<pre><code>b


</code></pre>
</li>
<li>c</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example299Line5613AnBnnCnD()
    {
        var input = @"
- a
  - b

    c
- d
";
        var expected = @"
<ul>
<li>a
<ul>
<li>
<p>b</p>
<p>c</p>
</li>
</ul>
</li>
<li>d</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example300Line5637AnBnNC()
    {
        var input = @"
* a
  > b
  >
* c
";
        var expected = @"
<ul>
<li>a
<blockquote>
<p>b</p>
</blockquote>
</li>
<li>c</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example301Line5657AnBnNCnND()
    {
        var input = @"
- a
  > b
  ```
  c
  ```
- d
";
        var expected = @"
<ul>
<li>a
<blockquote>
<p>b</p>
</blockquote>
<pre><code>c
</code></pre>
</li>
<li>d</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example302Line5680A()
    {
        var input = @"
- a
";
        var expected = @"
<ul>
<li>a</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example303Line5689AnB()
    {
        var input = @"
- a
  - b
";
        var expected = @"
<ul>
<li>a
<ul>
<li>b</li>
</ul>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example304Line57061NFoonNnBar()
    {
        var input = @"
1. ```
   foo
   ```

   bar
";
        var expected = @"
<ol>
<li>
<pre><code>foo
</code></pre>
<p>bar</p>
</li>
</ol>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example305Line5725FoonBarnnBaz()
    {
        var input = @"
* foo
  * bar

  baz
";
        var expected = @"
<ul>
<li>
<p>foo</p>
<ul>
<li>bar</li>
</ul>
<p>baz</p>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example306Line5743AnBnCnnDnEnF()
    {
        var input = @"
- a
  - b
  - c

- d
  - e
  - f
";
        var expected = @"
<ul>
<li>
<p>a</p>
<ul>
<li>b</li>
<li>c</li>
</ul>
</li>
<li>
<p>d</p>
<ul>
<li>e</li>
<li>f</li>
</ul>
</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example307Line5777Hilo()
    {
        var input = @"
`hi`lo`
";
        var expected = @"
<p><code>hi</code>lo`</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example308Line5791()
    {
        var input = @"
\!\""\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^\_\`\{\|\}\~
";
        var expected = @"
<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\]^_`{|}~</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example309Line5801Aa3()
    {
        var input = @"
\	\A\a\ \3\φ\«
";
        var expected = @"
<p>\	\A\a\ \3\φ\«</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example310Line5811NotEmphasizednbrNotATagnnotALinkfoonnotCoden1NotAListnNotAListnNotAHeadingnfooUrlNotAReferencenoumlNotACharacterEntity()
    {
        var input = @"
\*not emphasized*
\<br/> not a tag
\[not a link](/foo)
\`not code`
1\. not a list
\* not a list
\# not a heading
\[foo]: /url ""not a reference""
\&ouml; not a character entity
";
        var expected = @"
<p>*not emphasized*
&lt;br/&gt; not a tag
[not a link](/foo)
`not code`
1. not a list
* not a list
# not a heading
[foo]: /url &quot;not a reference&quot;
&amp;ouml; not a character entity</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example311Line5836Emphasis()
    {
        var input = @"
\\*emphasis*
";
        var expected = @"
<p>\<em>emphasis</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example312Line5845Foonbar()
    {
        var input = @"
foo\
bar
";
        var expected = @"
<p>foo<br />
bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example313Line5857()
    {
        var input = @"
`` \[\` ``
";
        var expected = @"
<p><code>\[\`</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example314Line5864()
    {
        var input = @"
    \[\]
";
        var expected = @"
<pre><code>\[\]
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example315Line5872Nn()
    {
        var input = @"
~~~
\[\]
~~~
";
        var expected = @"
<pre><code>\[\]
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example316Line5882Httpexamplecomfind()
    {
        var input = @"
<http://example.com?find=\*>
";
        var expected = @"
<p><a href=""http://example.com?find=%5C*"">http://example.com?find=\*</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example317Line5889AHrefbar()
    {
        var input = @"
<a href=""/bar\/)"">
";
        var expected = @"
<a href=""/bar\/)"">
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example318Line5899FoobarTitle()
    {
        var input = @"
[foo](/bar\* ""ti\*tle"")
";
        var expected = @"
<p><a href=""/bar*"" title=""ti*tle"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example319Line5906FoonnfooBarTitle()
    {
        var input = @"
[foo]

[foo]: /bar\* ""ti\*tle""
";
        var expected = @"
<p><a href=""/bar*"" title=""ti*tle"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example320Line5915Foobarnfoon()
    {
        var input = @"
``` foo\+bar
foo
```
";
        var expected = @"
<pre><code class=""language-foo+bar"">foo
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example321Line5952NbspAmpCopyAEligDcaronnfrac34HilbertSpaceDifferentialDnClockwiseContourIntegralNgE()
    {
        var input = @"
&nbsp; &amp; &copy; &AElig; &Dcaron;
&frac34; &HilbertSpace; &DifferentialD;
&ClockwiseContourIntegral; &ngE;
";
        var expected = @"
<p>  &amp; © Æ Ď
¾ ℋ ⅆ
∲ ≧̸</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example322Line59713512349920()
    {
        var input = @"
&#35; &#1234; &#992; &#0;
";
        var expected = @"
<p># Ӓ Ϡ �</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example323Line5984X22XD06Xcab()
    {
        var input = @"
&#X22; &#XD06; &#xcab;
";
        var expected = @"
<p>&quot; ആ ಫ</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example324Line5993NbspXXn987654321nabcdef0nThisIsNotDefinedHi()
    {
        var input = @"
&nbsp &x; &#; &#x;
&#987654321;
&#abcdef0;
&ThisIsNotDefined; &hi?;
";
        var expected = @"
<p>&amp;nbsp &amp;x; &amp;#; &amp;#x;
&amp;#987654321;
&amp;#abcdef0;
&amp;ThisIsNotDefined; &amp;hi?;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example325Line6010Copy()
    {
        var input = @"
&copy
";
        var expected = @"
<p>&amp;copy</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example326Line6020MadeUpEntity()
    {
        var input = @"
&MadeUpEntity;
";
        var expected = @"
<p>&amp;MadeUpEntity;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example327Line6031AHrefoumloumlhtml()
    {
        var input = @"
<a href=""&ouml;&ouml;.html"">
";
        var expected = @"
<a href=""&ouml;&ouml;.html"">
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example328Line6038FoofoumloumlFoumlouml()
    {
        var input = @"
[foo](/f&ouml;&ouml; ""f&ouml;&ouml;"")
";
        var expected = @"
<p><a href=""/f%C3%B6%C3%B6"" title=""föö"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example329Line6045FoonnfooFoumloumlFoumlouml()
    {
        var input = @"
[foo]

[foo]: /f&ouml;&ouml; ""f&ouml;&ouml;""
";
        var expected = @"
<p><a href=""/f%C3%B6%C3%B6"" title=""föö"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example330Line6054Foumloumlnfoon()
    {
        var input = @"
``` f&ouml;&ouml;
foo
```
";
        var expected = @"
<pre><code class=""language-föö"">foo
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example331Line6067Foumlouml()
    {
        var input = @"
`f&ouml;&ouml;`
";
        var expected = @"
<p><code>f&amp;ouml;&amp;ouml;</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example332Line6074Foumlfouml()
    {
        var input = @"
    f&ouml;f&ouml;
";
        var expected = @"
<pre><code>f&amp;ouml;f&amp;ouml;
</code></pre>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example333Line608642foo42nfoo()
    {
        var input = @"
&#42;foo&#42;
*foo*
";
        var expected = @"
<p>*foo*
<em>foo</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example334Line609442FoonnFoo()
    {
        var input = @"
&#42; foo

* foo
";
        var expected = @"
<p>* foo</p>
<ul>
<li>foo</li>
</ul>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example335Line6105Foo1010bar()
    {
        var input = @"
foo&#10;&#10;bar
";
        var expected = @"
<p>foo

bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example336Line61139foo()
    {
        var input = @"
&#9;foo
";
        var expected = @"
<p>	foo</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example337Line6120AurlQuottitquot()
    {
        var input = @"
[a](url &quot;tit&quot;)
";
        var expected = @"
<p>[a](url &quot;tit&quot;)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example338Line6148Foo()
    {
        var input = @"
`foo`
";
        var expected = @"
<p><code>foo</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example339Line6159FooBar()
    {
        var input = @"
`` foo ` bar ``
";
        var expected = @"
<p><code>foo ` bar</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example340Line6169()
    {
        var input = @"
` `` `
";
        var expected = @"
<p><code>``</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example341Line6177()
    {
        var input = @"
`  ``  `
";
        var expected = @"
<p><code> `` </code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example342Line6186A()
    {
        var input = @"
` a`
";
        var expected = @"
<p><code> a</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example343Line6195B()
    {
        var input = @"
` b `
";
        var expected = @"
<p><code> b </code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example344Line6203N()
    {
        var input = @"
` `
`  `
";
        var expected = @"
<p><code> </code>
<code>  </code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example345Line6214NfoonbarNbazn()
    {
        var input = @"
``
foo
bar  
baz
``
";
        var expected = @"
<p><code>foo bar   baz</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example346Line6224NfooN()
    {
        var input = @"
``
foo 
``
";
        var expected = @"
<p><code>foo </code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example347Line6235FooBarNbaz()
    {
        var input = @"
`foo   bar 
baz`
";
        var expected = @"
<p><code>foo   bar  baz</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example348Line6252Foobar()
    {
        var input = @"
`foo\`bar`
";
        var expected = @"
<p><code>foo\</code>bar`</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example349Line6263Foobar()
    {
        var input = @"
``foo`bar``
";
        var expected = @"
<p><code>foo`bar</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example350Line6269FooBar()
    {
        var input = @"
` foo `` bar `
";
        var expected = @"
<p><code>foo `` bar</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example351Line6281Foo()
    {
        var input = @"
*foo`*`
";
        var expected = @"
<p>*foo<code>*</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example352Line6290NotALinkfoo()
    {
        var input = @"
[not a `link](/foo`)
";
        var expected = @"
<p>[not a <code>link](/foo</code>)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example353Line6300AHref()
    {
        var input = @"
`<a href=""`"">`
";
        var expected = @"
<p><code>&lt;a href=&quot;</code>&quot;&gt;`</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example354Line6309AHref()
    {
        var input = @"
<a href=""`"">`
";
        var expected = @"
<p><a href=""`"">`</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example355Line6318Httpfoobarbaz()
    {
        var input = @"
`<http://foo.bar.`baz>`
";
        var expected = @"
<p><code>&lt;http://foo.bar.</code>baz&gt;`</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example356Line6327Httpfoobarbaz()
    {
        var input = @"
<http://foo.bar.`baz>`
";
        var expected = @"
<p><a href=""http://foo.bar.%60baz"">http://foo.bar.`baz</a>`</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example357Line6337Foo()
    {
        var input = @"
```foo``
";
        var expected = @"
<p>```foo``</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example358Line6344Foo()
    {
        var input = @"
`foo
";
        var expected = @"
<p>`foo</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example359Line6353Foobar()
    {
        var input = @"
`foo``bar``
";
        var expected = @"
<p>`foo<code>bar</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example360Line6570FooBar()
    {
        var input = @"
*foo bar*
";
        var expected = @"
<p><em>foo bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example361Line6580AFooBar()
    {
        var input = @"
a * foo bar*
";
        var expected = @"
<p>a * foo bar*</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example362Line6591Afoo()
    {
        var input = @"
a*""foo""*
";
        var expected = @"
<p>a*&quot;foo&quot;*</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example363Line6600A()
    {
        var input = @"
* a *
";
        var expected = @"
<p>* a *</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example364Line6609Foobar()
    {
        var input = @"
foo*bar*
";
        var expected = @"
<p>foo<em>bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example365Line66165678()
    {
        var input = @"
5*6*78
";
        var expected = @"
<p>5<em>6</em>78</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example366Line6625FooBar()
    {
        var input = @"
_foo bar_
";
        var expected = @"
<p><em>foo bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example367Line6635FooBar()
    {
        var input = @"
_ foo bar_
";
        var expected = @"
<p>_ foo bar_</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example368Line6645Afoo()
    {
        var input = @"
a_""foo""_
";
        var expected = @"
<p>a_&quot;foo&quot;_</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example369Line6654Foobar()
    {
        var input = @"
foo_bar_
";
        var expected = @"
<p>foo_bar_</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example370Line66615678()
    {
        var input = @"
5_6_78
";
        var expected = @"
<p>5_6_78</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example371Line6668()
    {
        var input = @"
пристаням_стремятся_
";
        var expected = @"
<p>пристаням_стремятся_</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example372Line6678Aabbcc()
    {
        var input = @"
aa_""bb""_cc
";
        var expected = @"
<p>aa_&quot;bb&quot;_cc</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example373Line6689FooBar()
    {
        var input = @"
foo-_(bar)_
";
        var expected = @"
<p>foo-<em>(bar)</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example374Line6701Foo()
    {
        var input = @"
_foo*
";
        var expected = @"
<p>_foo*</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example375Line6711FooBar()
    {
        var input = @"
*foo bar *
";
        var expected = @"
<p>*foo bar *</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example376Line6720FooBarn()
    {
        var input = @"
*foo bar
*
";
        var expected = @"
<p>*foo bar
*</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example377Line6733Foo()
    {
        var input = @"
*(*foo)
";
        var expected = @"
<p>*(*foo)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example378Line6743Foo()
    {
        var input = @"
*(*foo*)*
";
        var expected = @"
<p><em>(<em>foo</em>)</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example379Line6752Foobar()
    {
        var input = @"
*foo*bar
";
        var expected = @"
<p><em>foo</em>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example380Line6765FooBar()
    {
        var input = @"
_foo bar _
";
        var expected = @"
<p>_foo bar _</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example381Line6775Foo()
    {
        var input = @"
_(_foo)
";
        var expected = @"
<p>_(_foo)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example382Line6784Foo()
    {
        var input = @"
_(_foo_)_
";
        var expected = @"
<p><em>(<em>foo</em>)</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example383Line6793Foobar()
    {
        var input = @"
_foo_bar
";
        var expected = @"
<p>_foo_bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example384Line6800()
    {
        var input = @"
_пристаням_стремятся
";
        var expected = @"
<p>_пристаням_стремятся</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example385Line6807Foobarbaz()
    {
        var input = @"
_foo_bar_baz_
";
        var expected = @"
<p><em>foo_bar_baz</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example386Line6818Bar()
    {
        var input = @"
_(bar)_.
";
        var expected = @"
<p><em>(bar)</em>.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example387Line6827FooBar()
    {
        var input = @"
**foo bar**
";
        var expected = @"
<p><strong>foo bar</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example388Line6837FooBar()
    {
        var input = @"
** foo bar**
";
        var expected = @"
<p>** foo bar**</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example389Line6848Afoo()
    {
        var input = @"
a**""foo""**
";
        var expected = @"
<p>a**&quot;foo&quot;**</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example390Line6857Foobar()
    {
        var input = @"
foo**bar**
";
        var expected = @"
<p>foo<strong>bar</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example391Line6866FooBar()
    {
        var input = @"
__foo bar__
";
        var expected = @"
<p><strong>foo bar</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example392Line6876FooBar()
    {
        var input = @"
__ foo bar__
";
        var expected = @"
<p>__ foo bar__</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example393Line6884NfooBar()
    {
        var input = @"
__
foo bar__
";
        var expected = @"
<p>__
foo bar__</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example394Line6896Afoo()
    {
        var input = @"
a__""foo""__
";
        var expected = @"
<p>a__&quot;foo&quot;__</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example395Line6905Foobar()
    {
        var input = @"
foo__bar__
";
        var expected = @"
<p>foo__bar__</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example396Line69125678()
    {
        var input = @"
5__6__78
";
        var expected = @"
<p>5__6__78</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example397Line6919()
    {
        var input = @"
пристаням__стремятся__
";
        var expected = @"
<p>пристаням__стремятся__</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example398Line6926FooBarBaz()
    {
        var input = @"
__foo, __bar__, baz__
";
        var expected = @"
<p><strong>foo, <strong>bar</strong>, baz</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example399Line6937FooBar()
    {
        var input = @"
foo-__(bar)__
";
        var expected = @"
<p>foo-<strong>(bar)</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example400Line6950FooBar()
    {
        var input = @"
**foo bar **
";
        var expected = @"
<p>**foo bar **</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example401Line6963Foo()
    {
        var input = @"
**(**foo)
";
        var expected = @"
<p>**(**foo)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example402Line6973Foo()
    {
        var input = @"
*(**foo**)*
";
        var expected = @"
<p><em>(<strong>foo</strong>)</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example403Line6980GomphocarpusGomphocarpusPhysocarpusSynnAsclepiasPhysocarpa()
    {
        var input = @"
**Gomphocarpus (*Gomphocarpus physocarpus*, syn.
*Asclepias physocarpa*)**
";
        var expected = @"
<p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.
<em>Asclepias physocarpa</em>)</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example404Line6989FooBarFoo()
    {
        var input = @"
**foo ""*bar*"" foo**
";
        var expected = @"
<p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example405Line6998Foobar()
    {
        var input = @"
**foo**bar
";
        var expected = @"
<p><strong>foo</strong>bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example406Line7010FooBar()
    {
        var input = @"
__foo bar __
";
        var expected = @"
<p>__foo bar __</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example407Line7020Foo()
    {
        var input = @"
__(__foo)
";
        var expected = @"
<p>__(__foo)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example408Line7030Foo()
    {
        var input = @"
_(__foo__)_
";
        var expected = @"
<p><em>(<strong>foo</strong>)</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example409Line7039Foobar()
    {
        var input = @"
__foo__bar
";
        var expected = @"
<p>__foo__bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example410Line7046()
    {
        var input = @"
__пристаням__стремятся
";
        var expected = @"
<p>__пристаням__стремятся</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example411Line7053Foobarbaz()
    {
        var input = @"
__foo__bar__baz__
";
        var expected = @"
<p><strong>foo__bar__baz</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example412Line7064Bar()
    {
        var input = @"
__(bar)__.
";
        var expected = @"
<p><strong>(bar)</strong>.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example413Line7076FooBarurl()
    {
        var input = @"
*foo [bar](/url)*
";
        var expected = @"
<p><em>foo <a href=""/url"">bar</a></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example414Line7083Foonbar()
    {
        var input = @"
*foo
bar*
";
        var expected = @"
<p><em>foo
bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example415Line7095FooBarBaz()
    {
        var input = @"
_foo __bar__ baz_
";
        var expected = @"
<p><em>foo <strong>bar</strong> baz</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example416Line7102FooBarBaz()
    {
        var input = @"
_foo _bar_ baz_
";
        var expected = @"
<p><em>foo <em>bar</em> baz</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example417Line7109FooBar()
    {
        var input = @"
__foo_ bar_
";
        var expected = @"
<p><em><em>foo</em> bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example418Line7116FooBar()
    {
        var input = @"
*foo *bar**
";
        var expected = @"
<p><em>foo <em>bar</em></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example419Line7123FooBarBaz()
    {
        var input = @"
*foo **bar** baz*
";
        var expected = @"
<p><em>foo <strong>bar</strong> baz</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example420Line7129Foobarbaz()
    {
        var input = @"
*foo**bar**baz*
";
        var expected = @"
<p><em>foo<strong>bar</strong>baz</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example421Line7153Foobar()
    {
        var input = @"
*foo**bar*
";
        var expected = @"
<p><em>foo**bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example422Line7166FooBar()
    {
        var input = @"
***foo** bar*
";
        var expected = @"
<p><em><strong>foo</strong> bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example423Line7173FooBar()
    {
        var input = @"
*foo **bar***
";
        var expected = @"
<p><em>foo <strong>bar</strong></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example424Line7180Foobar()
    {
        var input = @"
*foo**bar***
";
        var expected = @"
<p><em>foo<strong>bar</strong></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example425Line7191Foobarbaz()
    {
        var input = @"
foo***bar***baz
";
        var expected = @"
<p>foo<em><strong>bar</strong></em>baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example426Line7197Foobarbaz()
    {
        var input = @"
foo******bar*********baz
";
        var expected = @"
<p>foo<strong><strong><strong>bar</strong></strong></strong>***baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example427Line7206FooBarBazBimBop()
    {
        var input = @"
*foo **bar *baz* bim** bop*
";
        var expected = @"
<p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example428Line7213FooBarurl()
    {
        var input = @"
*foo [*bar*](/url)*
";
        var expected = @"
<p><em>foo <a href=""/url""><em>bar</em></a></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example429Line7222IsNotAnEmptyEmphasis()
    {
        var input = @"
** is not an empty emphasis
";
        var expected = @"
<p>** is not an empty emphasis</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example430Line7229IsNotAnEmptyStrongEmphasis()
    {
        var input = @"
**** is not an empty strong emphasis
";
        var expected = @"
<p>**** is not an empty strong emphasis</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example431Line7242FooBarurl()
    {
        var input = @"
**foo [bar](/url)**
";
        var expected = @"
<p><strong>foo <a href=""/url"">bar</a></strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example432Line7249Foonbar()
    {
        var input = @"
**foo
bar**
";
        var expected = @"
<p><strong>foo
bar</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example433Line7261FooBarBaz()
    {
        var input = @"
__foo _bar_ baz__
";
        var expected = @"
<p><strong>foo <em>bar</em> baz</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example434Line7268FooBarBaz()
    {
        var input = @"
__foo __bar__ baz__
";
        var expected = @"
<p><strong>foo <strong>bar</strong> baz</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example435Line7275FooBar()
    {
        var input = @"
____foo__ bar__
";
        var expected = @"
<p><strong><strong>foo</strong> bar</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example436Line7282FooBar()
    {
        var input = @"
**foo **bar****
";
        var expected = @"
<p><strong>foo <strong>bar</strong></strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example437Line7289FooBarBaz()
    {
        var input = @"
**foo *bar* baz**
";
        var expected = @"
<p><strong>foo <em>bar</em> baz</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example438Line7296Foobarbaz()
    {
        var input = @"
**foo*bar*baz**
";
        var expected = @"
<p><strong>foo<em>bar</em>baz</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example439Line7303FooBar()
    {
        var input = @"
***foo* bar**
";
        var expected = @"
<p><strong><em>foo</em> bar</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example440Line7310FooBar()
    {
        var input = @"
**foo *bar***
";
        var expected = @"
<p><strong>foo <em>bar</em></strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example441Line7319FooBarBaznbimBop()
    {
        var input = @"
**foo *bar **baz**
bim* bop**
";
        var expected = @"
<p><strong>foo <em>bar <strong>baz</strong>
bim</em> bop</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example442Line7328FooBarurl()
    {
        var input = @"
**foo [*bar*](/url)**
";
        var expected = @"
<p><strong>foo <a href=""/url""><em>bar</em></a></strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example443Line7337IsNotAnEmptyEmphasis()
    {
        var input = @"
__ is not an empty emphasis
";
        var expected = @"
<p>__ is not an empty emphasis</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example444Line7344IsNotAnEmptyStrongEmphasis()
    {
        var input = @"
____ is not an empty strong emphasis
";
        var expected = @"
<p>____ is not an empty strong emphasis</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example445Line7354Foo()
    {
        var input = @"
foo ***
";
        var expected = @"
<p>foo ***</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example446Line7361Foo()
    {
        var input = @"
foo *\**
";
        var expected = @"
<p>foo <em>*</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example447Line7368Foo()
    {
        var input = @"
foo *_*
";
        var expected = @"
<p>foo <em>_</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example448Line7375Foo()
    {
        var input = @"
foo *****
";
        var expected = @"
<p>foo *****</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example449Line7382Foo()
    {
        var input = @"
foo **\***
";
        var expected = @"
<p>foo <strong>*</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example450Line7389Foo()
    {
        var input = @"
foo **_**
";
        var expected = @"
<p>foo <strong>_</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example451Line7400Foo()
    {
        var input = @"
**foo*
";
        var expected = @"
<p>*<em>foo</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example452Line7407Foo()
    {
        var input = @"
*foo**
";
        var expected = @"
<p><em>foo</em>*</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example453Line7414Foo()
    {
        var input = @"
***foo**
";
        var expected = @"
<p>*<strong>foo</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example454Line7421Foo()
    {
        var input = @"
****foo*
";
        var expected = @"
<p>***<em>foo</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example455Line7428Foo()
    {
        var input = @"
**foo***
";
        var expected = @"
<p><strong>foo</strong>*</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example456Line7435Foo()
    {
        var input = @"
*foo****
";
        var expected = @"
<p><em>foo</em>***</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example457Line7445Foo()
    {
        var input = @"
foo ___
";
        var expected = @"
<p>foo ___</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example458Line7452Foo()
    {
        var input = @"
foo _\__
";
        var expected = @"
<p>foo <em>_</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example459Line7459Foo()
    {
        var input = @"
foo _*_
";
        var expected = @"
<p>foo <em>*</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example460Line7466Foo()
    {
        var input = @"
foo _____
";
        var expected = @"
<p>foo _____</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example461Line7473Foo()
    {
        var input = @"
foo __\___
";
        var expected = @"
<p>foo <strong>_</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example462Line7480Foo()
    {
        var input = @"
foo __*__
";
        var expected = @"
<p>foo <strong>*</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example463Line7487Foo()
    {
        var input = @"
__foo_
";
        var expected = @"
<p>_<em>foo</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example464Line7498Foo()
    {
        var input = @"
_foo__
";
        var expected = @"
<p><em>foo</em>_</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example465Line7505Foo()
    {
        var input = @"
___foo__
";
        var expected = @"
<p>_<strong>foo</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example466Line7512Foo()
    {
        var input = @"
____foo_
";
        var expected = @"
<p>___<em>foo</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example467Line7519Foo()
    {
        var input = @"
__foo___
";
        var expected = @"
<p><strong>foo</strong>_</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example468Line7526Foo()
    {
        var input = @"
_foo____
";
        var expected = @"
<p><em>foo</em>___</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example469Line7536Foo()
    {
        var input = @"
**foo**
";
        var expected = @"
<p><strong>foo</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example470Line7543Foo()
    {
        var input = @"
*_foo_*
";
        var expected = @"
<p><em><em>foo</em></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example471Line7550Foo()
    {
        var input = @"
__foo__
";
        var expected = @"
<p><strong>foo</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example472Line7557Foo()
    {
        var input = @"
_*foo*_
";
        var expected = @"
<p><em><em>foo</em></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example473Line7567Foo()
    {
        var input = @"
****foo****
";
        var expected = @"
<p><strong><strong>foo</strong></strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example474Line7574Foo()
    {
        var input = @"
____foo____
";
        var expected = @"
<p><strong><strong>foo</strong></strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example475Line7585Foo()
    {
        var input = @"
******foo******
";
        var expected = @"
<p><strong><strong><strong>foo</strong></strong></strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example476Line7594Foo()
    {
        var input = @"
***foo***
";
        var expected = @"
<p><em><strong>foo</strong></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example477Line7601Foo()
    {
        var input = @"
_____foo_____
";
        var expected = @"
<p><em><strong><strong>foo</strong></strong></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example478Line7610FooBarBaz()
    {
        var input = @"
*foo _bar* baz_
";
        var expected = @"
<p><em>foo _bar</em> baz_</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example479Line7617FooBarBazBimBam()
    {
        var input = @"
*foo __bar *baz bim__ bam*
";
        var expected = @"
<p><em>foo <strong>bar *baz bim</strong> bam</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example480Line7626FooBarBaz()
    {
        var input = @"
**foo **bar baz**
";
        var expected = @"
<p>**foo <strong>bar baz</strong></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example481Line7633FooBarBaz()
    {
        var input = @"
*foo *bar baz*
";
        var expected = @"
<p>*foo <em>bar baz</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example482Line7642Barurl()
    {
        var input = @"
*[bar*](/url)
";
        var expected = @"
<p>*<a href=""/url"">bar*</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example483Line7649FooBarurl()
    {
        var input = @"
_foo [bar_](/url)
";
        var expected = @"
<p>_foo <a href=""/url"">bar_</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example484Line7656ImgSrcfooTitle()
    {
        var input = @"
*<img src=""foo"" title=""*""/>
";
        var expected = @"
<p>*<img src=""foo"" title=""*""/></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example485Line7663AHref()
    {
        var input = @"
**<a href=""**"">
";
        var expected = @"
<p>**<a href=""**""></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example486Line7670AHref()
    {
        var input = @"
__<a href=""__"">
";
        var expected = @"
<p>__<a href=""__""></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example487Line7677A()
    {
        var input = @"
*a `*`*
";
        var expected = @"
<p><em>a <code>*</code></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example488Line7684A()
    {
        var input = @"
_a `_`_
";
        var expected = @"
<p><em>a <code>_</code></em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example489Line7691Ahttpfoobarq()
    {
        var input = @"
**a<http://foo.bar/?q=**>
";
        var expected = @"
<p>**a<a href=""http://foo.bar/?q=**"">http://foo.bar/?q=**</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example490Line7698Ahttpfoobarq()
    {
        var input = @"
__a<http://foo.bar/?q=__>
";
        var expected = @"
<p>__a<a href=""http://foo.bar/?q=__"">http://foo.bar/?q=__</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example491Line7714HiHelloWorld()
    {
        var input = @"
~~Hi~~ Hello, world!
";
        var expected = @"
<p><del>Hi</del> Hello, world!</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example492Line7723ThisHasAnnnewParagraph()
    {
        var input = @"
This ~~has a

new paragraph~~.
";
        var expected = @"
<p>This ~~has a</p>
<p>new paragraph~~.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example493Line7734ThisWillNotStrike()
    {
        var input = @"
This will ~~~not~~~ strike.
";
        var expected = @"
<p>This will ~~~not~~~ strike.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example494Line7817LinkuriTitle()
    {
        var input = @"
[link](/uri ""title"")
";
        var expected = @"
<p><a href=""/uri"" title=""title"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example495Line7826Linkuri()
    {
        var input = @"
[link](/uri)
";
        var expected = @"
<p><a href=""/uri"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example496Line7835Link()
    {
        var input = @"
[link]()
";
        var expected = @"
<p><a href="""">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example497Line7842Link()
    {
        var input = @"
[link](<>)
";
        var expected = @"
<p><a href="""">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example498Line7851LinkmyUri()
    {
        var input = @"
[link](/my uri)
";
        var expected = @"
<p>[link](/my uri)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example499Line7857LinkmyUri()
    {
        var input = @"
[link](</my uri>)
";
        var expected = @"
<p><a href=""/my%20uri"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example500Line7866Linkfoonbar()
    {
        var input = @"
[link](foo
bar)
";
        var expected = @"
<p>[link](foo
bar)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example501Line7874Linkfoonbar()
    {
        var input = @"
[link](<foo
bar>)
";
        var expected = @"
<p>[link](<foo
bar>)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example502Line7885Abc()
    {
        var input = @"
[a](<b)c>)
";
        var expected = @"
<p><a href=""b)c"">a</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example503Line7893Linkfoo()
    {
        var input = @"
[link](<foo\>)
";
        var expected = @"
<p>[link](&lt;foo&gt;)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example504Line7902Abcnabcnabc()
    {
        var input = @"
[a](<b)c
[a](<b)c>
[a](<b>c)
";
        var expected = @"
<p>[a](&lt;b)c
[a](&lt;b)c&gt;
[a](<b>c)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example505Line7914Linkfoo()
    {
        var input = @"
[link](\(foo\))
";
        var expected = @"
<p><a href=""(foo)"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example506Line7923Linkfooandbar()
    {
        var input = @"
[link](foo(and(bar)))
";
        var expected = @"
<p><a href=""foo(and(bar))"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example507Line7932Linkfooandbar()
    {
        var input = @"
[link](foo\(and\(bar\))
";
        var expected = @"
<p><a href=""foo(and(bar)"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example508Line7939Linkfooandbar()
    {
        var input = @"
[link](<foo(and(bar)>)
";
        var expected = @"
<p><a href=""foo(and(bar)"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example509Line7949Linkfoo()
    {
        var input = @"
[link](foo\)\:)
";
        var expected = @"
<p><a href=""foo):"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example510Line7958Linkfragmentnnlinkhttpexamplecomfragmentnnlinkhttpexamplecomfoo3frag()
    {
        var input = @"
[link](#fragment)

[link](http://example.com#fragment)

[link](http://example.com?foo=3#frag)
";
        var expected = @"
<p><a href=""#fragment"">link</a></p>
<p><a href=""http://example.com#fragment"">link</a></p>
<p><a href=""http://example.com?foo=3#frag"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example511Line7974Linkfoobar()
    {
        var input = @"
[link](foo\bar)
";
        var expected = @"
<p><a href=""foo%5Cbar"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example512Line7990Linkfoo20bauml()
    {
        var input = @"
[link](foo%20b&auml;)
";
        var expected = @"
<p><a href=""foo%20b%C3%A4"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example513Line8001Linktitle()
    {
        var input = @"
[link](""title"")
";
        var expected = @"
<p><a href=""%22title%22"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example514Line8010LinkurlTitlenlinkurlTitlenlinkurlTitle()
    {
        var input = @"
[link](/url ""title"")
[link](/url 'title')
[link](/url (title))
";
        var expected = @"
<p><a href=""/url"" title=""title"">link</a>
<a href=""/url"" title=""title"">link</a>
<a href=""/url"" title=""title"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example515Line8024LinkurlTitleQuot()
    {
        var input = @"
[link](/url ""title \""&quot;"")
";
        var expected = @"
<p><a href=""/url"" title=""title &quot;&quot;"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example516Line8034LinkurlTitle()
    {
        var input = @"
[link](/url ""title"")
";
        var expected = @"
<p><a href=""/url%C2%A0%22title%22"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example517Line8043LinkurlTitleAndTitle()
    {
        var input = @"
[link](/url ""title ""and"" title"")
";
        var expected = @"
<p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example518Line8052LinkurlTitleAndTitle()
    {
        var input = @"
[link](/url 'title ""and"" title')
";
        var expected = @"
<p><a href=""/url"" title=""title &quot;and&quot; title"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example519Line8076LinkUrinTitle()
    {
        var input = @"
[link](   /uri
  ""title""  )
";
        var expected = @"
<p><a href=""/uri"" title=""title"">link</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example520Line8087LinkUri()
    {
        var input = @"
[link] (/uri)
";
        var expected = @"
<p>[link] (/uri)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example521Line8097LinkFooBaruri()
    {
        var input = @"
[link [foo [bar]]](/uri)
";
        var expected = @"
<p><a href=""/uri"">link [foo [bar]]</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example522Line8104LinkBaruri()
    {
        var input = @"
[link] bar](/uri)
";
        var expected = @"
<p>[link] bar](/uri)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example523Line8111LinkBaruri()
    {
        var input = @"
[link [bar](/uri)
";
        var expected = @"
<p>[link <a href=""/uri"">bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example524Line8118LinkBaruri()
    {
        var input = @"
[link \[bar](/uri)
";
        var expected = @"
<p><a href=""/uri"">link [bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example525Line8127LinkFooBarUri()
    {
        var input = @"
[link *foo **bar** `#`*](/uri)
";
        var expected = @"
<p><a href=""/uri"">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example526Line8134Moonmoonjpguri()
    {
        var input = @"
[![moon](moon.jpg)](/uri)
";
        var expected = @"
<p><a href=""/uri""><img src=""moon.jpg"" alt=""moon"" /></a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example527Line8143FooBaruriuri()
    {
        var input = @"
[foo [bar](/uri)](/uri)
";
        var expected = @"
<p>[foo <a href=""/uri"">bar</a>](/uri)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example528Line8150FooBarBazuriuriuri()
    {
        var input = @"
[foo *[bar [baz](/uri)](/uri)*](/uri)
";
        var expected = @"
<p>[foo <em>[bar <a href=""/uri"">baz</a>](/uri)</em>](/uri)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example529Line8157Foouri1uri2uri3()
    {
        var input = @"
![[[foo](uri1)](uri2)](uri3)
";
        var expected = @"
<p><img src=""uri3"" alt=""[foo](uri2)"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example530Line8167Foouri()
    {
        var input = @"
*[foo*](/uri)
";
        var expected = @"
<p>*<a href=""/uri"">foo*</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example531Line8174FooBarbaz()
    {
        var input = @"
[foo *bar](baz*)
";
        var expected = @"
<p><a href=""baz*"">foo *bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    // TODO:
    [Ignore]
    [TestMethod]
    public void Example532Line8184FooBarBaz()
    {
        var input = @"
*foo [bar* baz]
";
        var expected = @"
<p><em>foo [bar</em> baz]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example533Line8194FooBarAttrbaz()
    {
        var input = @"
[foo <bar attr=""](baz)"">
";
        var expected = @"
<p>[foo <bar attr=""](baz)""></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example534Line8201Foouri()
    {
        var input = @"
[foo`](/uri)`
";
        var expected = @"
<p>[foo<code>](/uri)</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example535Line8208Foohttpexamplecomsearchuri()
    {
        var input = @"
[foo<http://example.com/?search=](uri)>
";
        var expected = @"
<p>[foo<a href=""http://example.com/?search=%5D(uri)"">http://example.com/?search=](uri)</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example536Line8246FoobarnnbarUrlTitle()
    {
        var input = @"
[foo][bar]

[bar]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example537Line8261LinkFooBarrefnnrefUri()
    {
        var input = @"
[link [foo [bar]]][ref]

[ref]: /uri
";
        var expected = @"
<p><a href=""/uri"">link [foo [bar]]</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example538Line8270LinkBarrefnnrefUri()
    {
        var input = @"
[link \[bar][ref]

[ref]: /uri
";
        var expected = @"
<p><a href=""/uri"">link [bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example539Line8281LinkFooBarRefnnrefUri()
    {
        var input = @"
[link *foo **bar** `#`*][ref]

[ref]: /uri
";
        var expected = @"
<p><a href=""/uri"">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example540Line8290MoonmoonjpgrefnnrefUri()
    {
        var input = @"
[![moon](moon.jpg)][ref]

[ref]: /uri
";
        var expected = @"
<p><a href=""/uri""><img src=""moon.jpg"" alt=""moon"" /></a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example541Line8301FooBarurirefnnrefUri()
    {
        var input = @"
[foo [bar](/uri)][ref]

[ref]: /uri
";
        var expected = @"
<p>[foo <a href=""/uri"">bar</a>]<a href=""/uri"">ref</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example542Line8310FooBarBazrefrefnnrefUri()
    {
        var input = @"
[foo *bar [baz][ref]*][ref]

[ref]: /uri
";
        var expected = @"
<p>[foo <em>bar <a href=""/uri"">baz</a></em>]<a href=""/uri"">ref</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example543Line8325FoorefnnrefUri()
    {
        var input = @"
*[foo*][ref]

[ref]: /uri
";
        var expected = @"
<p>*<a href=""/uri"">foo*</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example544Line8334FooBarrefnnrefUri()
    {
        var input = @"
[foo *bar][ref]

[ref]: /uri
";
        var expected = @"
<p><a href=""/uri"">foo *bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example545Line8346FooBarAttrrefnnrefUri()
    {
        var input = @"
[foo <bar attr=""][ref]"">

[ref]: /uri
";
        var expected = @"
<p>[foo <bar attr=""][ref]""></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example546Line8355FoorefnnrefUri()
    {
        var input = @"
[foo`][ref]`

[ref]: /uri
";
        var expected = @"
<p>[foo<code>][ref]</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example547Line8364FoohttpexamplecomsearchrefnnrefUri()
    {
        var input = @"
[foo<http://example.com/?search=][ref]>

[ref]: /uri
";
        var expected = @"
<p>[foo<a href=""http://example.com/?search=%5D%5Bref%5D"">http://example.com/?search=][ref]</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example548Line8375FooBaRnnbarUrlTitle()
    {
        var input = @"
[foo][BaR]

[bar]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example549Line8386IsARussianWordnnUrl()
    {
        var input = @"
[Толпой][Толпой] is a Russian word.

[ТОЛПОЙ]: /url
";
        var expected = @"
<p><a href=""/url"">Толпой</a> is a Russian word.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example550Line8398FoonBarUrlnnBazFooBar()
    {
        var input = @"
[Foo
  bar]: /url

[Baz][Foo bar]
";
        var expected = @"
<p><a href=""/url"">Baz</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example551Line8411FooBarnnbarUrlTitle()
    {
        var input = @"
[foo] [bar]

[bar]: /url ""title""
";
        var expected = @"
<p>[foo] <a href=""/url"" title=""title"">bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example552Line8420FoonbarnnbarUrlTitle()
    {
        var input = @"
[foo]
[bar]

[bar]: /url ""title""
";
        var expected = @"
<p>[foo]
<a href=""/url"" title=""title"">bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example553Line8461FooUrl1nnfooUrl2nnbarfoo()
    {
        var input = @"
[foo]: /url1

[foo]: /url2

[bar][foo]
";
        var expected = @"
<p><a href=""/url1"">bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example554Line8476BarfoonnfooUrl()
    {
        var input = @"
[bar][foo\!]

[foo!]: /url
";
        var expected = @"
<p>[bar][foo!]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example555Line8488FoorefnnrefUri()
    {
        var input = @"
[foo][ref[]

[ref[]: /uri
";
        var expected = @"
<p>[foo][ref[]</p>
<p>[ref[]: /uri</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example556Line8498FoorefbarnnrefbarUri()
    {
        var input = @"
[foo][ref[bar]]

[ref[bar]]: /uri
";
        var expected = @"
<p>[foo][ref[bar]]</p>
<p>[ref[bar]]: /uri</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example557Line8508FoonnfooUrl()
    {
        var input = @"
[[[foo]]]

[[[foo]]]: /url
";
        var expected = @"
<p>[[[foo]]]</p>
<p>[[[foo]]]: /url</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example558Line8518FoorefnnrefUri()
    {
        var input = @"
[foo][ref\[]

[ref\[]: /uri
";
        var expected = @"
<p><a href=""/uri"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example559Line8529BarUrinnbar()
    {
        var input = @"
[bar\\]: /uri

[bar\\]
";
        var expected = @"
<p><a href=""/uri"">bar\</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example560Line8540NnUri()
    {
        var input = @"
[]

[]: /uri
";
        var expected = @"
<p>[]</p>
<p>[]: /uri</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example561Line8550NNnnUri()
    {
        var input = @"
[
 ]

[
 ]: /uri
";
        var expected = @"
<p>[
]</p>
<p>[
]: /uri</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example562Line8573FoonnfooUrlTitle()
    {
        var input = @"
[foo][]

[foo]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example563Line8582FooBarnnfooBarUrlTitle()
    {
        var input = @"
[*foo* bar][]

[*foo* bar]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title""><em>foo</em> bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example564Line8593FoonnfooUrlTitle()
    {
        var input = @"
[Foo][]

[foo]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title"">Foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example565Line8606FooNnnfooUrlTitle()
    {
        var input = @"
[foo] 
[]

[foo]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title"">foo</a>
[]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example566Line8626FoonnfooUrlTitle()
    {
        var input = @"
[foo]

[foo]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example567Line8635FooBarnnfooBarUrlTitle()
    {
        var input = @"
[*foo* bar]

[*foo* bar]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title""><em>foo</em> bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example568Line8644FooBarnnfooBarUrlTitle()
    {
        var input = @"
[[*foo* bar]]

[*foo* bar]: /url ""title""
";
        var expected = @"
<p>[<a href=""/url"" title=""title""><em>foo</em> bar</a>]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example569Line8653BarFoonnfooUrl()
    {
        var input = @"
[[bar [foo]

[foo]: /url
";
        var expected = @"
<p>[[bar <a href=""/url"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example570Line8664FoonnfooUrlTitle()
    {
        var input = @"
[Foo]

[foo]: /url ""title""
";
        var expected = @"
<p><a href=""/url"" title=""title"">Foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example571Line8675FooBarnnfooUrl()
    {
        var input = @"
[foo] bar

[foo]: /url
";
        var expected = @"
<p><a href=""/url"">foo</a> bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example572Line8687FoonnfooUrlTitle()
    {
        var input = @"
\[foo]

[foo]: /url ""title""
";
        var expected = @"
<p>[foo]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example573Line8699FooUrlnnfoo()
    {
        var input = @"
[foo*]: /url

*[foo*]
";
        var expected = @"
<p>*<a href=""/url"">foo*</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example574Line8711FoobarnnfooUrl1nbarUrl2()
    {
        var input = @"
[foo][bar]

[foo]: /url1
[bar]: /url2
";
        var expected = @"
<p><a href=""/url2"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example575Line8720FoonnfooUrl1()
    {
        var input = @"
[foo][]

[foo]: /url1
";
        var expected = @"
<p><a href=""/url1"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example576Line8730FoonnfooUrl1()
    {
        var input = @"
[foo]()

[foo]: /url1
";
        var expected = @"
<p><a href="""">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example577Line8738FoonotALinknnfooUrl1()
    {
        var input = @"
[foo](not a link)

[foo]: /url1
";
        var expected = @"
<p><a href=""/url1"">foo</a>(not a link)</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example578Line8749FoobarbaznnbazUrl()
    {
        var input = @"
[foo][bar][baz]

[baz]: /url
";
        var expected = @"
<p>[foo]<a href=""/url"">bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example579Line8761FoobarbaznnbazUrl1nbarUrl2()
    {
        var input = @"
[foo][bar][baz]

[baz]: /url1
[bar]: /url2
";
        var expected = @"
<p><a href=""/url2"">foo</a><a href=""/url1"">baz</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example580Line8774FoobarbaznnbazUrl1nfooUrl2()
    {
        var input = @"
[foo][bar][baz]

[baz]: /url1
[foo]: /url2
";
        var expected = @"
<p>[foo]<a href=""/url1"">bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example581Line8797FoourlTitle()
    {
        var input = @"
![foo](/url ""title"")
";
        var expected = @"
<p><img src=""/url"" alt=""foo"" title=""title"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example582Line8804FooBarnnfooBarTrainjpgTrainTracks()
    {
        var input = @"
![foo *bar*]

[foo *bar*]: train.jpg ""train & tracks""
";
        var expected = @"
<p><img src=""train.jpg"" alt=""foo bar"" title=""train &amp; tracks"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example583Line8813FooBarurlurl2()
    {
        var input = @"
![foo ![bar](/url)](/url2)
";
        var expected = @"
<p><img src=""/url2"" alt=""foo bar"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example584Line8820FooBarurlurl2()
    {
        var input = @"
![foo [bar](/url)](/url2)
";
        var expected = @"
<p><img src=""/url2"" alt=""foo bar"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example585Line8834FooBarnnfooBarTrainjpgTrainTracks()
    {
        var input = @"
![foo *bar*][]

[foo *bar*]: train.jpg ""train & tracks""
";
        var expected = @"
<p><img src=""train.jpg"" alt=""foo bar"" title=""train &amp; tracks"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example586Line8843FooBarfoobarnnFOOBARTrainjpgTrainTracks()
    {
        var input = @"
![foo *bar*][foobar]

[FOOBAR]: train.jpg ""train & tracks""
";
        var expected = @"
<p><img src=""train.jpg"" alt=""foo bar"" title=""train &amp; tracks"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example587Line8852Footrainjpg()
    {
        var input = @"
![foo](train.jpg)
";
        var expected = @"
<p><img src=""train.jpg"" alt=""foo"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example588Line8859MyFooBarpathtotrainjpgTitle()
    {
        var input = @"
My ![foo bar](/path/to/train.jpg  ""title""   )
";
        var expected = @"
<p>My <img src=""/path/to/train.jpg"" alt=""foo bar"" title=""title"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example589Line8866Foourl()
    {
        var input = @"
![foo](<url>)
";
        var expected = @"
<p><img src=""url"" alt=""foo"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example590Line8873Url()
    {
        var input = @"
![](/url)
";
        var expected = @"
<p><img src=""/url"" alt="""" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example591Line8882FoobarnnbarUrl()
    {
        var input = @"
![foo][bar]

[bar]: /url
";
        var expected = @"
<p><img src=""/url"" alt=""foo"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example592Line8891FoobarnnBARUrl()
    {
        var input = @"
![foo][bar]

[BAR]: /url
";
        var expected = @"
<p><img src=""/url"" alt=""foo"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example593Line8902FoonnfooUrlTitle()
    {
        var input = @"
![foo][]

[foo]: /url ""title""
";
        var expected = @"
<p><img src=""/url"" alt=""foo"" title=""title"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example594Line8911FooBarnnfooBarUrlTitle()
    {
        var input = @"
![*foo* bar][]

[*foo* bar]: /url ""title""
";
        var expected = @"
<p><img src=""/url"" alt=""foo bar"" title=""title"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example595Line8922FoonnfooUrlTitle()
    {
        var input = @"
![Foo][]

[foo]: /url ""title""
";
        var expected = @"
<p><img src=""/url"" alt=""Foo"" title=""title"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example596Line8934FooNnnfooUrlTitle()
    {
        var input = @"
![foo] 
[]

[foo]: /url ""title""
";
        var expected = @"
<p><img src=""/url"" alt=""foo"" title=""title"" />
[]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example597Line8947FoonnfooUrlTitle()
    {
        var input = @"
![foo]

[foo]: /url ""title""
";
        var expected = @"
<p><img src=""/url"" alt=""foo"" title=""title"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example598Line8956FooBarnnfooBarUrlTitle()
    {
        var input = @"
![*foo* bar]

[*foo* bar]: /url ""title""
";
        var expected = @"
<p><img src=""/url"" alt=""foo bar"" title=""title"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example599Line8967FoonnfooUrlTitle()
    {
        var input = @"
![[foo]]

[[foo]]: /url ""title""
";
        var expected = @"
<p>![[foo]]</p>
<p>[[foo]]: /url &quot;title&quot;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example600Line8979FoonnfooUrlTitle()
    {
        var input = @"
![Foo]

[foo]: /url ""title""
";
        var expected = @"
<p><img src=""/url"" alt=""Foo"" title=""title"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example601Line8991FoonnfooUrlTitle()
    {
        var input = @"
!\[foo]

[foo]: /url ""title""
";
        var expected = @"
<p>![foo]</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example602Line9003FoonnfooUrlTitle()
    {
        var input = @"
\![foo]

[foo]: /url ""title""
";
        var expected = @"
<p>!<a href=""/url"" title=""title"">foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example603Line9036Httpfoobarbaz()
    {
        var input = @"
<http://foo.bar.baz>
";
        var expected = @"
<p><a href=""http://foo.bar.baz"">http://foo.bar.baz</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example604Line9043Httpfoobarbaztestqhelloid22boolean()
    {
        var input = @"
<http://foo.bar.baz/test?q=hello&id=22&boolean>
";
        var expected = @"
<p><a href=""http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean"">http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example605Line9050Ircfoobar2233baz()
    {
        var input = @"
<irc://foo.bar:2233/baz>
";
        var expected = @"
<p><a href=""irc://foo.bar:2233/baz"">irc://foo.bar:2233/baz</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example606Line9059MAILTOFOOBARBAZ()
    {
        var input = @"
<MAILTO:FOO@BAR.BAZ>
";
        var expected = @"
<p><a href=""MAILTO:FOO@BAR.BAZ"">MAILTO:FOO@BAR.BAZ</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example607Line9071Abcd()
    {
        var input = @"
<a+b+c:d>
";
        var expected = @"
<p><a href=""a+b+c:d"">a+b+c:d</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example608Line9078MadeUpSchemefoobar()
    {
        var input = @"
<made-up-scheme://foo,bar>
";
        var expected = @"
<p><a href=""made-up-scheme://foo,bar"">made-up-scheme://foo,bar</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example609Line9085Http()
    {
        var input = @"
<http://../>
";
        var expected = @"
<p><a href=""http://../"">http://../</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example610Line9092Localhost5001foo()
    {
        var input = @"
<localhost:5001/foo>
";
        var expected = @"
<p><a href=""localhost:5001/foo"">localhost:5001/foo</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example611Line9101HttpfoobarbazBim()
    {
        var input = @"
<http://foo.bar/baz bim>
";
        var expected = @"
<p>&lt;http://foo.bar/baz bim&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example612Line9110Httpexamplecom()
    {
        var input = @"
<http://example.com/\[\>
";
        var expected = @"
<p><a href=""http://example.com/%5C%5B%5C"">http://example.com/\[\</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example613Line9132Foobarexamplecom()
    {
        var input = @"
<foo@bar.example.com>
";
        var expected = @"
<p><a href=""mailto:foo@bar.example.com"">foo@bar.example.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example614Line9139FoospecialBarbazBar0com()
    {
        var input = @"
<foo+special@Bar.baz-bar0.com>
";
        var expected = @"
<p><a href=""mailto:foo+special@Bar.baz-bar0.com"">foo+special@Bar.baz-bar0.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example615Line9148Foobarexamplecom()
    {
        var input = @"
<foo\+@bar.example.com>
";
        var expected = @"
<p>&lt;foo+@bar.example.com&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example616Line9157()
    {
        var input = @"
<>
";
        var expected = @"
<p>&lt;&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example617Line9164Httpfoobar()
    {
        var input = @"
< http://foo.bar >
";
        var expected = @"
<p>&lt; http://foo.bar &gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example618Line9171Mabc()
    {
        var input = @"
<m:abc>
";
        var expected = @"
<p>&lt;m:abc&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example619Line9178Foobarbaz()
    {
        var input = @"
<foo.bar.baz>
";
        var expected = @"
<p>&lt;foo.bar.baz&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example620Line9185Httpexamplecom()
    {
        var input = @"
http://example.com
";
        var expected = @"
<p><a href=""http://example.com"">http://example.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example621Line9192Foobarexamplecom()
    {
        var input = @"
foo@bar.example.com
";
        var expected = @"
<p><a href=""mailto:foo@bar.example.com"">foo@bar.example.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example622Line9221Wwwcommonmarkorg()
    {
        var input = @"
www.commonmark.org
";
        var expected = @"
<p><a href=""http://www.commonmark.org"">www.commonmark.org</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example623Line9229VisitWwwcommonmarkorghelpForMoreInformation()
    {
        var input = @"
Visit www.commonmark.org/help for more information.
";
        var expected = @"
<p>Visit <a href=""http://www.commonmark.org/help"">www.commonmark.org/help</a> for more information.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example624Line9241VisitWwwcommonmarkorgnnVisitWwwcommonmarkorgab()
    {
        var input = @"
Visit www.commonmark.org.

Visit www.commonmark.org/a.b.
";
        var expected = @"
<p>Visit <a href=""http://www.commonmark.org"">www.commonmark.org</a>.</p>
<p>Visit <a href=""http://www.commonmark.org/a.b"">www.commonmark.org/a.b</a>.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example625Line9255WwwgooglecomsearchqMarkupbusinessnnwwwgooglecomsearchqMarkupbusinessnnwwwgooglecomsearchqMarkupbusinessnnwwwgooglecomsearchqMarkupbusiness()
    {
        var input = @"
www.google.com/search?q=Markup+(business)

www.google.com/search?q=Markup+(business)))

(www.google.com/search?q=Markup+(business))

(www.google.com/search?q=Markup+(business)
";
        var expected = @"
<p><a href=""http://www.google.com/search?q=Markup+(business)"">www.google.com/search?q=Markup+(business)</a></p>
<p><a href=""http://www.google.com/search?q=Markup+(business)"">www.google.com/search?q=Markup+(business)</a>))</p>
<p>(<a href=""http://www.google.com/search?q=Markup+(business)"">www.google.com/search?q=Markup+(business)</a>)</p>
<p>(<a href=""http://www.google.com/search?q=Markup+(business)"">www.google.com/search?q=Markup+(business)</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example626Line9274Wwwgooglecomsearchqbusinessok()
    {
        var input = @"
www.google.com/search?q=(business))+ok
";
        var expected = @"
<p><a href=""http://www.google.com/search?q=(business))+ok"">www.google.com/search?q=(business))+ok</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example627Line9285Wwwgooglecomsearchqcommonmarkhlennnwwwgooglecomsearchqcommonmarkhl()
    {
        var input = @"
www.google.com/search?q=commonmark&hl=en

www.google.com/search?q=commonmark&hl;
";
        var expected = @"
<p><a href=""http://www.google.com/search?q=commonmark&amp;hl=en"">www.google.com/search?q=commonmark&amp;hl=en</a></p>
<p><a href=""http://www.google.com/search?q=commonmark"">www.google.com/search?q=commonmark</a>&amp;hl;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example628Line9296Wwwcommonmarkorghelp()
    {
        var input = @"
www.commonmark.org/he<lp
";
        var expected = @"
<p><a href=""http://www.commonmark.org/he"">www.commonmark.org/he</a>&lt;lp</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example629Line9307HttpcommonmarkorgnnVisitHttpsencryptedgooglecomsearchqMarkupbusinessnnAnonymousFTPIsAvailableAtFtpfoobarbaz()
    {
        var input = @"
http://commonmark.org

(Visit https://encrypted.google.com/search?q=Markup+(business))

Anonymous FTP is available at ftp://foo.bar.baz.
";
        var expected = @"
<p><a href=""http://commonmark.org"">http://commonmark.org</a></p>
<p>(Visit <a href=""https://encrypted.google.com/search?q=Markup+(business)"">https://encrypted.google.com/search?q=Markup+(business)</a>)</p>
<p>Anonymous FTP is available at <a href=""ftp://foo.bar.baz"">ftp://foo.bar.baz</a>.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example630Line9333Foobarbaz()
    {
        var input = @"
foo@bar.baz
";
        var expected = @"
<p><a href=""mailto:foo@bar.baz"">foo@bar.baz</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example631Line9341HellomailxyzexampleIsntValidButHelloxyzmailexampleIs()
    {
        var input = @"
hello@mail+xyz.example isn't valid, but hello+xyz@mail.example is.
";
        var expected = @"
<p>hello@mail+xyz.example isn't valid, but <a href=""mailto:hello+xyz@mail.example"">hello+xyz@mail.example</a> is.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example632Line9351AbCdabnnabCdabnnabCdabNnabCdab()
    {
        var input = @"
a.b-c_d@a.b

a.b-c_d@a.b.

a.b-c_d@a.b-

a.b-c_d@a.b_
";
        var expected = @"
<p><a href=""mailto:a.b-c_d@a.b"">a.b-c_d@a.b</a></p>
<p><a href=""mailto:a.b-c_d@a.b"">a.b-c_d@a.b</a>.</p>
<p>a.b-c_d@a.b-</p>
<p>a.b-c_d@a.b_</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example633Line9375MailtofoobarbaznnmailtoabCdabnnmailtoabCdabnnmailtoabCdabnnmailtoabCdabNnmailtoabCdabnnxmppfoobarbaznnxmppfoobarbaz()
    {
        var input = @"
mailto:foo@bar.baz

mailto:a.b-c_d@a.b

mailto:a.b-c_d@a.b.

mailto:a.b-c_d@a.b/

mailto:a.b-c_d@a.b-

mailto:a.b-c_d@a.b_

xmpp:foo@bar.baz

xmpp:foo@bar.baz.
";
        var expected = @"
<p><a href=""mailto:foo@bar.baz"">mailto:foo@bar.baz</a></p>
<p><a href=""mailto:a.b-c_d@a.b"">mailto:a.b-c_d@a.b</a></p>
<p><a href=""mailto:a.b-c_d@a.b"">mailto:a.b-c_d@a.b</a>.</p>
<p><a href=""mailto:a.b-c_d@a.b"">mailto:a.b-c_d@a.b</a>/</p>
<p>mailto:a.b-c_d@a.b-</p>
<p>mailto:a.b-c_d@a.b_</p>
<p><a href=""xmpp:foo@bar.baz"">xmpp:foo@bar.baz</a></p>
<p><a href=""xmpp:foo@bar.baz"">xmpp:foo@bar.baz</a>.</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example634Line9406Xmppfoobarbaztxtnnxmppfoobarbaztxtbinnnxmppfoobarbaztxtbincom()
    {
        var input = @"
xmpp:foo@bar.baz/txt

xmpp:foo@bar.baz/txt@bin

xmpp:foo@bar.baz/txt@bin.com
";
        var expected = @"
<p><a href=""xmpp:foo@bar.baz/txt"">xmpp:foo@bar.baz/txt</a></p>
<p><a href=""xmpp:foo@bar.baz/txt@bin"">xmpp:foo@bar.baz/txt@bin</a></p>
<p><a href=""xmpp:foo@bar.baz/txt@bin.com"">xmpp:foo@bar.baz/txt@bin.com</a></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example635Line9420Xmppfoobarbaztxtbin()
    {
        var input = @"
xmpp:foo@bar.baz/txt/bin
";
        var expected = @"
<p><a href=""xmpp:foo@bar.baz/txt"">xmpp:foo@bar.baz/txt</a>/bin</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example636Line9502Ababc2c()
    {
        var input = @"
<a><bab><c2c>
";
        var expected = @"
<p><a><bab><c2c></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example637Line9511Ab2()
    {
        var input = @"
<a/><b2/>
";
        var expected = @"
<p><a/><b2/></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example638Line9520AB2ndatafoo()
    {
        var input = @"
<a  /><b2
data=""foo"" >
";
        var expected = @"
<p><a  /><b2
data=""foo"" ></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example639Line9531AFoobarBamBazEmemnbooleanZoop33zoop33()
    {
        var input = @"
<a foo=""bar"" bam = 'baz <em>""</em>'
_boolean zoop:33=zoop:33 />
";
        var expected = @"
<p><a foo=""bar"" bam = 'baz <em>""</em>'
_boolean zoop:33=zoop:33 /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example640Line9542FooResponsiveImageSrcfoojpg()
    {
        var input = @"
Foo <responsive-image src=""foo.jpg"" />
";
        var expected = @"
<p>Foo <responsive-image src=""foo.jpg"" /></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example641Line955133()
    {
        var input = @"
<33> <__>
";
        var expected = @"
<p>&lt;33&gt; &lt;__&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example642Line9560AHrefhi()
    {
        var input = @"
<a h*#ref=""hi"">
";
        var expected = @"
<p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example643Line9569AHrefhiAHrefhi()
    {
        var input = @"
<a href=""hi'> <a href=hi'>
";
        var expected = @"
<p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example644Line9578AnfoobarNfooBarbaznbimbop()
    {
        var input = @"
< a><
foo><bar/ >
<foo bar=baz
bim!bop />
";
        var expected = @"
<p>&lt; a&gt;&lt;
foo&gt;&lt;bar/ &gt;
&lt;foo bar=baz
bim!bop /&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example645Line9593AHrefbartitletitle()
    {
        var input = @"
<a href='bar'title=title>
";
        var expected = @"
<p>&lt;a href='bar'title=title&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example646Line9602Afoo()
    {
        var input = @"
</a></foo >
";
        var expected = @"
<p></a></foo ></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example647Line9611AHreffoo()
    {
        var input = @"
</a href=""foo"">
";
        var expected = @"
<p>&lt;/a href=&quot;foo&quot;&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example648Line9620FooThisIsANcommentWithHyphens()
    {
        var input = @"
foo <!-- this is a --
comment - with hyphens -->
";
        var expected = @"
<p>foo <!-- this is a --
comment - with hyphens --></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example649Line9628FooThisIsANcommentWithHyphens()
    {
        var input = @"
foo <!-- this is a --
comment - with hyphens -->
";
        var expected = @"
<p>foo <!-- this is a --
comment - with hyphens --></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example650Line9636FooFooNnfooFoo()
    {
        var input = @"
foo <!--> foo -->

foo <!---> foo -->
";
        var expected = @"
<p>foo <!--> foo --&gt;</p>
<p>foo <!---> foo --&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example651Line9648FooPhpEchoA()
    {
        var input = @"
foo <?php echo $a; ?>
";
        var expected = @"
<p>foo <?php echo $a; ?></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example652Line9657FooELEMENTBrEMPTY()
    {
        var input = @"
foo <!ELEMENT br EMPTY>
";
        var expected = @"
<p>foo <!ELEMENT br EMPTY></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example653Line9666FooCDATA()
    {
        var input = @"
foo <![CDATA[>&<]]>
";
        var expected = @"
<p>foo <![CDATA[>&<]]></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example654Line9676FooAHrefouml()
    {
        var input = @"
foo <a href=""&ouml;"">
";
        var expected = @"
<p>foo <a href=""&ouml;""></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example655Line9685FooAHref()
    {
        var input = @"
foo <a href=""\*"">
";
        var expected = @"
<p>foo <a href=""\*""></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example656Line9692AHref()
    {
        var input = @"
<a href=""\"""">
";
        var expected = @"
<p>&lt;a href=&quot;&quot;&quot;&gt;</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    // TODO:
    [Ignore]
    [TestMethod]
    public void Example657Line9723StrongTitleStyleEmnnblockquotenXmpIsDisallowedXMPIsAlsoDisallowednblockquote()
    {
        var input = @"
<strong> <title> <style> <em>

<blockquote>
  <xmp> is disallowed.  <XMP> is also disallowed.
</blockquote>
";
        var expected = @"
<p><strong> &lt;title> &lt;style> <em></p>
<blockquote>
  &lt;xmp> is disallowed.  &lt;XMP> is also disallowed.
</blockquote>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example658Line9745FooNbaz()
    {
        var input = @"
foo  
baz
";
        var expected = @"
<p>foo<br />
baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example659Line9757Foonbaz()
    {
        var input = @"
foo\
baz
";
        var expected = @"
<p>foo<br />
baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example660Line9768FooNbaz()
    {
        var input = @"
foo       
baz
";
        var expected = @"
<p>foo<br />
baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example661Line9779FooNBar()
    {
        var input = @"
foo  
     bar
";
        var expected = @"
<p>foo<br />
bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example662Line9788FoonBar()
    {
        var input = @"
foo\
     bar
";
        var expected = @"
<p>foo<br />
bar</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example663Line9800FooNbar()
    {
        var input = @"
*foo  
bar*
";
        var expected = @"
<p><em>foo<br />
bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example664Line9809Foonbar()
    {
        var input = @"
*foo\
bar*
";
        var expected = @"
<p><em>foo<br />
bar</em></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example665Line9820CodeNspan()
    {
        var input = @"
`code  
span`
";
        var expected = @"
<p><code>code   span</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example666Line9828Codenspan()
    {
        var input = @"
`code\
span`
";
        var expected = @"
<p><code>code\ span</code></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example667Line9838AHreffooNbar()
    {
        var input = @"
<a href=""foo  
bar"">
";
        var expected = @"
<p><a href=""foo  
bar""></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example668Line9847AHreffoonbar()
    {
        var input = @"
<a href=""foo\
bar"">
";
        var expected = @"
<p><a href=""foo\
bar""></p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example669Line9860Foo()
    {
        var input = @"
foo\
";
        var expected = @"
<p>foo\</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example670Line9867Foo()
    {
        var input = @"
foo  
";
        var expected = @"
<p>foo</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example671Line9874Foo()
    {
        var input = @"
### foo\
";
        var expected = @"
<h3>foo\</h3>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example672Line9881Foo()
    {
        var input = @"
### foo  
";
        var expected = @"
<h3>foo</h3>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example673Line9896Foonbaz()
    {
        var input = @"
foo
baz
";
        var expected = @"
<p>foo
baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example674Line9908FooNBaz()
    {
        var input = @"
foo 
 baz
";
        var expected = @"
<p>foo
baz</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example675Line9928HelloThere()
    {
        var input = @"
hello $.;'there
";
        var expected = @"
<p>hello $.;'there</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example676Line9935Foo()
    {
        var input = @"
Foo χρῆν
";
        var expected = @"
<p>Foo χρῆν</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }

    [TestMethod]
    public void Example677Line9944MultipleSpaces()
    {
        var input = @"
Multiple     spaces
";
        var expected = @"
<p>Multiple     spaces</p>
".Substring(1);

        var htmlSpaced = Transformer.Execute(input, Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\n", "\r\n"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\r\n", "\n"));

        var htmlCr = Transformer.Execute(input.Replace("\n", "\r"), Gfm.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCr.Replace("\r", "\n"));
    }
}