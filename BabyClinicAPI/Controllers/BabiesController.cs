using BabyClinicAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BabyClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BabiesController : ControllerBase
    {
        // 1. רשימה סטטית המדמה את טבלת הנתונים (In-Memory DB)
        private static List<Baby> _babies = new List<Baby>
        {
            new Baby { Id = 1, Name = "יוסי כהן", BirthDate = DateTime.Now.AddMonths(-6), ParentName = "אמא כהן", Phone = "050-1234567", Status = "פעיל" },
            new Baby { Id = 2, Name = "שירה לוי", BirthDate = DateTime.Now.AddMonths(-2), ParentName = "אבא לוי", Phone = "052-7654321", Status = "פעיל" }
        };
        private static int _nextBabyId = 3; // מונה ID סטטי

        // --- פעולות CRUD ---

        // GET /api/babies
        [HttpGet]
        public ActionResult<IEnumerable<Baby>> GetBabies()
        {
            return Ok(_babies); // מחזיר 200 OK
        }

        // GET /api/babies/{id}
        [HttpGet("{id}")]
        public ActionResult<Baby> GetBaby(int id)
        {
            var baby = _babies.FirstOrDefault(b => b.Id == id);

            if (baby == null)
            {
                return NotFound(); // 404: המזהה לא נמצא
            }

            return Ok(baby);
        }

        // POST /api/babies
        [HttpPost]
        public ActionResult<Baby> PostBaby(Baby baby)
        {
            baby.Id = _nextBabyId++; // יצירת ID חדש
            _babies.Add(baby);

            // מחזיר 201 Created
            return CreatedAtAction(nameof(GetBaby), new { id = baby.Id }, baby);
        }

        // PUT /api/babies/{id}
        [HttpPut("{id}")]
        public IActionResult PutBaby(int id, Baby updatedBaby)
        {
            if (id != updatedBaby.Id)
            {
                return BadRequest(); // 400
            }

            var existingIndex = _babies.FindIndex(b => b.Id == id);

            if (existingIndex == -1)
            {
                return NotFound(); // 404
            }

            _babies[existingIndex] = updatedBaby;
            return NoContent(); // 204: עדכון בוצע בהצלחה
        }

        // DELETE /api/babies/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBaby(int id)
        {
            var baby = _babies.FirstOrDefault(b => b.Id == id);

            if (baby == null)
            {
                return NotFound(); // 404
            }

            _babies.Remove(baby);
            return NoContent(); // 204
        }

        // --- פעולה מיוחדת: עדכון סטטוס ---

        // PUT /api/babies/{id}/status
        [HttpPut("{id}/status")]
        public IActionResult UpdateBabyStatus(int id, [FromBody] string status)
        {
            var baby = _babies.FirstOrDefault(b => b.Id == id);

            if (baby == null)
            {
                return NotFound(); // 404
            }

            baby.Status = status;
            return NoContent(); // 204
        }
    }
}
