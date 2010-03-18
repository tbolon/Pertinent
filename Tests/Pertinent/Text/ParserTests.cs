using NUnit.Framework;

namespace Pertinent.Text
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Empty()
        {
            TextTemplate t = new TextTemplate("");
            Assert.AreEqual(0, t.Parameters.Count);
        }

        [Test]
        public void Only_Content()
        {
            TextTemplate t = new TextTemplate("foo bar");
            Assert.AreEqual(0, t.Parameters.Count);
        }

        [Test]
        public void Only_Parameter()
        {
            TextTemplate t = new TextTemplate("[foo]");
            Assert.AreEqual(1, t.Parameters.Count);
        }

        [Test]
        public void One()
        {
            TextTemplate t = new TextTemplate("ab [abd]");
            Assert.AreEqual(1, t.Parameters.Count);
        }

        [Test]
        public void MultiLine()
        {
            TextTemplate t = new TextTemplate("[fo\r\no]");
            Assert.AreEqual("fo\r\no", t.Parameters[0].Name);
        }

        [Test]
        public void Detect_Name()
        {
            TextTemplate t = new TextTemplate("ab [abd]");
            Assert.AreEqual("abd", t.Parameters[0].Name);
        }

        [Test]
        public void Ingore_Empty()
        {
            TextTemplate t = new TextTemplate("ab[]");
            Assert.AreEqual(0, t.Parameters.Count);
        }

        [Test]
        public void Two()
        {
            TextTemplate t = new TextTemplate("[foo] [bar]");
            Assert.AreEqual(2, t.Parameters.Count);
        }

        [Test]
        public void Correct_Index()
        {
            TextTemplate t = new TextTemplate("[foo] [bar]");
            Assert.AreEqual(6, t.Parameters[1].Index);
        }

        [Test]
        public void Correct_Length()
        {
            TextTemplate t = new TextTemplate("[foo] [barr]");
            Assert.AreEqual(6, t.Parameters[1].Length);
        }

        [Test]
        public void Detect_DefaultValue()
        {
            TextTemplate t = new TextTemplate("[foo:vide]");
            Assert.AreEqual("vide", t.Parameters[0].DefaultValue);
            Assert.AreEqual(null, t.Parameters[0].Format);
        }

        [Test]
        public void Detect_Empty_DefaultValue()
        {
            TextTemplate t = new TextTemplate("[foo:]");
            Assert.AreEqual("", t.Parameters[0].DefaultValue);
            Assert.AreEqual(null, t.Parameters[0].Format);
        }

        [Test]
        public void Detect_Empty_DefaultValue_And_Empty_Format()
        {
            TextTemplate t = new TextTemplate("[foo::]");
            Assert.AreEqual("", t.Parameters[0].DefaultValue);
            Assert.AreEqual("", t.Parameters[0].Format);
        }

        [Test]
        public void Detect_Format()
        {
            TextTemplate t = new TextTemplate("[foo::bar]");
            Assert.AreEqual("", t.Parameters[0].DefaultValue);
            Assert.AreEqual("bar", t.Parameters[0].Format);
        }

        [Test]
        public void Allow_Escaped_Name()
        {
            TextTemplate t = new TextTemplate(@"[fo\o]");
            Assert.AreEqual("foo", t.Parameters[0].Name);
        }

        [Test]
        public void Allow_Escaped_DoubleDot_In_Name()
        {
            TextTemplate t = new TextTemplate(@"[fo\:o]");
            Assert.AreEqual("fo:o", t.Parameters[0].Name);
        }

        [Test]
        public void Allow_Escaped_DefaultValue()
        {
            TextTemplate t = new TextTemplate(@"[foo:b\:oo]");
            Assert.AreEqual("b:oo", t.Parameters[0].DefaultValue);
        }

        [Test]
        public void Allow_Escaped_Ending_Squares()
        {
            TextTemplate t = new TextTemplate(@"[foo:b\]oo]");
            Assert.AreEqual("b]oo", t.Parameters[0].DefaultValue);
        }

        [Test]
        public void Non_Ending_Parameter_Are_Ignored()
        {
            TextTemplate t = new TextTemplate(@"[foo] [boo");
            Assert.AreEqual(1, t.Parameters.Count);
        }
    }
}
