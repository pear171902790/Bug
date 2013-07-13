using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Logic.Tests
{
    [TestFixture]
    public class BugLogicTests
    {


        [Test]
        public void GetTotal_ProjectIdIsZero_ReturnsZero()
        {
            var bugRepository = new Mock<IBugRepository>();
            
            var bugLogic = new BugLogic(bugRepository.Object,null,null,null,null,null,null);

            bugRepository.Setup(x => x.GetTotal(null)).Returns(0);

            var result=bugLogic.GetTotal(0);

            Assert.AreEqual(0,result);

        }
    }
}
