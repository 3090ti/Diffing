using Microsoft.AspNetCore.Mvc;
using DiffingLibrary;
using Newtonsoft.Json;

namespace DiffingAPI.Controllers
{
    [Route("v1/diff")]
    [ApiController]
    [Produces("application/json")]
    public class Diff : Controller
    {
        /// <summary>
        /// Applies diffing algo for left & right side for specified Id. Returns 404 or diff information in JSON.
        /// </summary>
        /// <param name="id">Id of right/left side to apply diffing algo to.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (!Data.DiffData.ContainsKey(id))
                return NotFound();
            if (Data.DiffData[id].LeftData == null || Data.DiffData[id].RightData == null)
                return NotFound();

            var diffType = DiffingLibrary.Analyze.GetDiffType(Data.DiffData[id].LeftData, Data.DiffData[id].RightData);
            var ret = new DiffingLibrary.HttpModel.GetData
            {
                diffResultType = diffType
            };

            if (diffType == DiffingLibrary.Model.DiffTypes.ContentDoNotMatch)
                ret.diffs = DiffingLibrary.Analyze.GetListOfDiffs(Data.DiffData[id].LeftData, Data.DiffData[id].RightData);

            return Content(JsonConvert.SerializeObject(ret), "application/json");
        }

        /// <summary>
        /// Sets the value for specified side and id
        /// </summary>
        /// <param name="id">Specified Id, will be created or replaced if exists.</param>
        /// <param name="side">Accept only 'left' or 'right' side.</param>
        /// <param name="value">JSON object with encoded data.</param>
        [HttpPut("{id}/{side}")]
        public ActionResult Put(int id, string side, [FromBody] HttpModel.PutData value)
        {
            if (side == "right" || side == "left") { }
            else return BadRequest();
            if (value == null || value.data == null)
                return BadRequest();

            if (!Data.DiffData.ContainsKey(id))
                Data.DiffData.TryAdd(id, new DiffModel());

            if (side == "left")
                Data.DiffData[id].LeftData = value.GetDecoded();
            if (side == "right")
                Data.DiffData[id].RightData = value.GetDecoded();

            return Created(Request.Path, null);
        }
    }
}
