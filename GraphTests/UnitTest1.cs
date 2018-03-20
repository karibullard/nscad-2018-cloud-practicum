using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Graph;
using API.GraphHelper;
using System.Threading.Tasks;

namespace GraphTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestGraphRepo()
        {
            APIAuthenticationProvider sampleAuthProvider = new APIAuthenticationProvider();
            GraphServiceClient graphServiceClient = new GraphServiceClient(sampleAuthProvider);
            //GraphRepository graphRepo = new GraphRepository(graphServiceClient);

            //String expected = "an id!";
            //String result = await graphRepo.GetUserID("tkarp87@live.com");

            //Assert.AreEqual(expected, result);
        }
    }
}
