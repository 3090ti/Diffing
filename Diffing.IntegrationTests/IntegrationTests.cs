using DiffingLibrary;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Diffing.TestData;
using DeepEqual.Syntax;

namespace Diffing.IntegrationTests
{
    public class IntegrationTests
    {
        static readonly HttpModel.PutData dataX = new HttpModel.PutData(TestData.TestData.X);
        static readonly HttpModel.PutData dataY = new HttpModel.PutData(TestData.TestData.Y);
        static readonly HttpModel.PutData dataZ = new HttpModel.PutData(TestData.TestData.Z);

        static readonly StringContent contentX = new System.Net.Http.StringContent(JsonConvert.SerializeObject(dataX), Encoding.UTF8, "application/json");
        static readonly StringContent contentY = new System.Net.Http.StringContent(JsonConvert.SerializeObject(dataY), Encoding.UTF8, "application/json");
        static readonly StringContent contentZ = new System.Net.Http.StringContent(JsonConvert.SerializeObject(dataZ), Encoding.UTF8, "application/json");

        [Fact]
        public async Task Get_BeforePut_ShouldNotFound()
        {
            using (var client = new TestClientProvider().HttpClient)
            {
                var resp = await client.GetAsync("/v1/diff/1");

                Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
            }
        }

        [Fact]
        public async Task Put_SameValues_ShouldBeEqual()
        {
            using (var client = new TestClientProvider().HttpClient)
            {
                var r1 = await client.PutAsync("/v1/diff/1/left", contentX);
                Assert.Equal(HttpStatusCode.Created, r1.StatusCode);

                var r2 = await client.PutAsync("/v1/diff/1/right", contentX);
                Assert.Equal(HttpStatusCode.Created, r2.StatusCode);

                var resp = await client.GetAsync("/v1/diff/1");
                Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

                var jsonResp = await resp.Content.ReadAsStringAsync();
                var dataActualValue = JsonConvert.DeserializeObject<HttpModel.GetData>(jsonResp).diffResultType;

                Assert.Equal(Model.DiffTypes.Equals, dataActualValue);
            }
        }

        [Fact]
        public async Task Put_DifferentLength_ShouldBeSizeDoNotMatch()
        {
            using (var client = new TestClientProvider().HttpClient)
            {
                var r1 = await client.PutAsync("/v1/diff/1/left", contentX);
                Assert.Equal(HttpStatusCode.Created, r1.StatusCode);

                var r2 = await client.PutAsync("/v1/diff/1/right", contentZ);
                Assert.Equal(HttpStatusCode.Created, r2.StatusCode);

                var resp = await client.GetAsync("/v1/diff/1");
                Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

                var jsonResp = await resp.Content.ReadAsStringAsync();
                var dataActualValue = JsonConvert.DeserializeObject<HttpModel.GetData>(jsonResp).diffResultType;

                Assert.Equal(Model.DiffTypes.SizeDoNotMatch, dataActualValue);
            }
        }

        [Fact]
        public async Task Put_SameLength_DifferentContent_ShouldMatch_PresetDiffList()
        {
            using (var client = new TestClientProvider().HttpClient)
            {
                var r1 = await client.PutAsync("/v1/diff/1/left", contentX);
                Assert.Equal(HttpStatusCode.Created, r1.StatusCode);

                var r2 = await client.PutAsync("/v1/diff/1/right", contentY);
                Assert.Equal(HttpStatusCode.Created, r2.StatusCode);

                var resp = await client.GetAsync("/v1/diff/1");
                Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

                var jsonResp = await resp.Content.ReadAsStringAsync();
                var dataActualValue = JsonConvert.DeserializeObject<HttpModel.GetData>(jsonResp);

                Assert.Equal(Model.DiffTypes.ContentDoNotMatch, dataActualValue.diffResultType);

                dataActualValue.diffs.ShouldDeepEqual(TestData.TestData.expectedDiffsXY);
            }
        }
    }
}
