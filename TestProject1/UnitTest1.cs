using System;
using System.IO;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test_IsJpg_Should_Be_True()
        {

            
                    var fs = new FileStream(@"c:\temp\foxhound.jpg", FileMode.Open);

                    var ba = new byte[4];

                    fs.Read(ba, 0, 4);



            var y = ConsoleApp1.Program.IsJpg(ba);

            Assert.True(y);
        }


          [Fact]
        public void Test_IsPdf_ShouldBeTrue()
        {

            
                    var fs = new FileStream(@"c:\temp\ttt.pdf", FileMode.Open);

                    var ba = new byte[4];

                    fs.Read(ba, 0, 4);



            var y = ConsoleApp1.Program.IsPdf(ba);

            Assert.True(y);
        }

    }
}
