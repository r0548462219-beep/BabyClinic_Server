using BabyClinicAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BabyClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private static List<Nurse> _nurses = new List<Nurse>
        {
            new Nurse { Id = 10, Name = "דינה ישראלי", Specialty = "שקילה", Phone = "054-9990000", Status = "פעילה" },
            new Nurse { Id = 11, Name = "אסתר כהן", Specialty = "התפתחות", Phone = "054-1112222", Status = "פעילה" }
        };
        private static int _nextNurseId = 12;

        // --- פעולות CRUD ---

        // GET /api/nurses
        [HttpGet]
        public ActionResult<IEnumerable<Nurse>> GetNurses()
        {
            return Ok(_nurses);
        }

        // GET /api/nurses/{id}
        [HttpGet("{id}")]
        public ActionResult<Nurse> GetNurse(int id)
        {
            var nurse = _nurses.FirstOrDefault(n => n.Id == id);

            if (nurse == null)
            {
                return NotFound(); // 404
            }

            return Ok(nurse);
        }

        // POST /api/nurses
        [HttpPost]
        public ActionResult<Nurse> PostNurse(Nurse nurse)
        {
            nurse.Id = _nextNurseId++;
            _nurses.Add(nurse);

            return CreatedAtAction(nameof(GetNurse), new { id = nurse.Id }, nurse);
        }

        // PUT /api/nurses/{id}
        [HttpPut("{id}")]
        public IActionResult PutNurse(int id, Nurse updatedNurse)
        {
            if (id != updatedNurse.Id)
            {
                return BadRequest();
            }

            var existingIndex = _nurses.FindIndex(n => n.Id == id);

            if (existingIndex == -1)
            {
                return NotFound(); // 404
            }

            _nurses[existingIndex] = updatedNurse;
            return NoContent(); // 204
        }

        // DELETE /api/nurses/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteNurse(int id)
        {
            var nurse = _nurses.FirstOrDefault(n => n.Id == id);

            if (nurse == null)
            {
                return NotFound(); // 404
            }

            _nurses.Remove(nurse);
            return NoContent(); // 204
        }

        // --- פעולה מיוחדת: עדכון סטטוס ---

        // PUT /api/nurses/{id}/status
        [HttpPut("{id}/status")]
        public IActionResult UpdateNurseStatus(int id, [FromBody] string status)
        {
            var nurse = _nurses.FirstOrDefault(n => n.Id == id);

            if (nurse == null)
            {
                return NotFound(); // 404
            }

            nurse.Status = status;
            return NoContent();
        }
    }
}
