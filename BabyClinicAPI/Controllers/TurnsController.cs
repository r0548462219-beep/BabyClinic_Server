using BabyClinicAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BabyClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnsController : ControllerBase
    {
        private static List<Turn> _turns = new List<Turn>
        {
            new Turn { Id = 100, BabyId = 1, NurseId = 10, DateTime = DateTime.Today.AddHours(10), Status = "נקבע" },
            new Turn { Id = 101, BabyId = 2, NurseId = 11, DateTime = DateTime.Today.AddDays(1).AddHours(14), Status = "נקבע" }
        };
        private static int _nextTurnId = 102;

        // --- פעולות CRUD ---

        // GET /api/turns
        [HttpGet]
        public ActionResult<IEnumerable<Turn>> GetTurns()
        {
            return Ok(_turns);
        }

        // GET /api/turns/{id}
        [HttpGet("{id}")]
        public ActionResult<Turn> GetTurn(int id)
        {
            var turn = _turns.FirstOrDefault(t => t.Id == id);

            if (turn == null)
            {
                return NotFound(); // 404
            }

            return Ok(turn);
        }

        // POST /api/turns
        [HttpPost]
        public ActionResult<Turn> PostTurn(Turn turn)
        {
            turn.Id = _nextTurnId++;
            _turns.Add(turn);

            return CreatedAtAction(nameof(GetTurn), new { id = turn.Id }, turn);
        }

        // PUT /api/turns/{id}
        [HttpPut("{id}")]
        public IActionResult PutTurn(int id, Turn updatedTurn)
        {
            if (id != updatedTurn.Id)
            {
                return BadRequest();
            }

            var existingIndex = _turns.FindIndex(t => t.Id == id);

            if (existingIndex == -1)
            {
                return NotFound(); // 404
            }

            _turns[existingIndex] = updatedTurn;
            return NoContent();
        }

        // DELETE /api/turns/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTurn(int id)
        {
            var turn = _turns.FirstOrDefault(t => t.Id == id);

            if (turn == null)
            {
                return NotFound(); // 404
            }

            _turns.Remove(turn);
            return NoContent(); // 204
        }

        // --- פעולה מיוחדת: שליפה לפי תאריך ---

        // GET /api/turns/by-date?date=YYYY-MM-DD
        [HttpGet("by-date")]
        public ActionResult<IEnumerable<Turn>> GetTurnsByDate([FromQuery] DateTime date)
        {
            // .Date מבטיח השוואה לפי תאריך בלבד (ללא שעות ודקות)
            var filteredTurns = _turns.Where(t => t.DateTime.Date == date.Date).ToList();

            return Ok(filteredTurns);
        }
    }
}
