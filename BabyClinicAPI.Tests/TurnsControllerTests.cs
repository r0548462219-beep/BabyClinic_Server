using BabyClinicAPI.Controllers;
using BabyClinicAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyClinicAPI.Tests
{
    public class TurnsControllerTests
    {
        private readonly TurnsController _controller;

        public TurnsControllerTests()
        {
            // אתחול הקונטרולר לפני כל בדיקה
            _controller = new TurnsController();
        }

        // =====================================================
        // טסטים עבור GET ALL
        // =====================================================

        [Fact]
        public void GetTurns_ReturnsOkResult()
        {
            // Act
            var actionResult = _controller.GetTurns();

            // Assert: בדיקה שהוחזר קוד 200 OK עם תוכן (OkObjectResult)
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetTurns_ReturnsAllTurns()
        {
            // Act
            var actionResult = _controller.GetTurns();
            var okResult = actionResult.Result as OkObjectResult;
            var turns = okResult.Value as IEnumerable<Turn>;

            // Assert: בדיקה שמוחזרות לפחות 2 תורים (הנתונים הראשוניים)
            Assert.NotNull(turns);
            Assert.True(turns.Count() >= 2);
        }

        // =====================================================
        // טסטים עבור GET BY ID
        // =====================================================

        [Fact]
        public void GetTurn_ExistingId_ReturnsOk()
        {
            // Arrange: מזהה תור קיים
            int existingId = 100;

            // Act
            var actionResult = _controller.GetTurn(existingId);

            // Assert: בדיקה שהוחזר קוד 200 OK
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetTurn_NonExistingId_ReturnsNotFound()
        {
            // Arrange: מזהה שלא קיים
            int nonExistingId = 999;

            // Act
            var actionResult = _controller.GetTurn(nonExistingId);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        // =====================================================
        // טסטים עבור POST (הוספה)
        // =====================================================

        [Fact]
        public void PostTurn_ValidTurn_ReturnsCreatedAtAction()
        {
            // Arrange: יצירת אובייקט תור חדש
            var newTurn = new Turn
            {
                Id = 0, // ה-ID יוחלף אוטומטית
                BabyId = 1,
                NurseId = 10,
                DateTime = DateTime.Now.AddDays(7),
                Status = "נקבע"
            };

            // Act
            var actionResult = _controller.PostTurn(newTurn);

            // Assert: בדיקה שהוחזר קוד 201 Created (CreatedAtActionResult)
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public void PostTurn_ValidTurn_ReturnsTurnWithNewId()
        {
            // Arrange
            var newTurn = new Turn
            {
                Id = 0,
                BabyId = 2,
                NurseId = 11,
                DateTime = DateTime.Now.AddDays(3),
                Status = "נקבע"
            };

            // Act
            var actionResult = _controller.PostTurn(newTurn);
            var createdResult = actionResult.Result as CreatedAtActionResult;
            var createdTurn = createdResult.Value as Turn;

            // Assert: בדיקה שהתור קיבל ID חדש
            Assert.NotNull(createdTurn);
            Assert.True(createdTurn.Id >= 102); // ה-ID הבא אחרי הנתונים הראשוניים
        }

        // =====================================================
        // טסטים עבור PUT (עדכון מלא)
        // =====================================================

        [Fact]
        public void PutTurn_ExistingId_ReturnsNoContent()
        {
            // Arrange: עדכון תור קיים
            int existingId = 100;
            var updatedTurn = new Turn
            {
                Id = existingId,
                BabyId = 1,
                NurseId = 10,
                DateTime = DateTime.Now.AddDays(5),
                Status = "בוצע"
            };

            // Act: ביצוע עדכון
            var result = _controller.PutTurn(existingId, updatedTurn);

            // Assert: בדיקה שהוחזר קוד 204 No Content (עדכון מוצלחה)
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PutTurn_MismatchedId_ReturnsBadRequest()
        {
            // Arrange: ה-ID בנתיב שונה מה-ID באובייקט
            int pathId = 100;
            var updatedTurn = new Turn
            {
                Id = 999, // ID שונה!
                BabyId = 1,
                NurseId = 10,
                DateTime = DateTime.Now,
                Status = "נקבע"
            };

            // Act
            var result = _controller.PutTurn(pathId, updatedTurn);

            // Assert: בדיקה שהוחזר קוד 400 Bad Request
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutTurn_NonExistingId_ReturnsNotFound()
        {
            // Arrange: ניסיון לעדכן תור שלא קיים
            int nonExistingId = 999;
            var updatedTurn = new Turn
            {
                Id = nonExistingId,
                BabyId = 1,
                NurseId = 10,
                DateTime = DateTime.Now,
                Status = "נקבע"
            };

            // Act
            var result = _controller.PutTurn(nonExistingId, updatedTurn);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }

        // =====================================================
        // טסטים עבור DELETE (מחיקה)
        // =====================================================

        [Fact]
        public void DeleteTurn_ExistingId_ReturnsNoContent()
        {
            // Arrange: מחיקת תור קיים
            int existingId = 101;

            // Act
            var result = _controller.DeleteTurn(existingId);

            // Assert: בדיקה שהוחזר קוד 204 No Content (מחיקה מוצלחת)
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteTurn_NonExistingId_ReturnsNotFound()
        {
            // Arrange: ניסיון למחוק תור שלא קיים
            int nonExistingId = 999;

            // Act
            var result = _controller.DeleteTurn(nonExistingId);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }

        // =====================================================
        // טסטים עבור GET BY DATE (פונקציה מיוחדת)
        // =====================================================

        [Fact]
        public void GetTurnsByDate_ReturnsOkResult()
        {
            // Arrange: תאריך כלשהו
            DateTime date = DateTime.Today;

            // Act
            var actionResult = _controller.GetTurnsByDate(date);

            // Assert: בדיקה שהוחזר קוד 200 OK
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetTurnsByDate_ReturnsTurnsForDate()
        {
            // Arrange: התאריך של התור הראשון בנתונים
            DateTime date = DateTime.Today;

            // Act
            var actionResult = _controller.GetTurnsByDate(date);
            var okResult = actionResult.Result as OkObjectResult;
            var turns = okResult.Value as List<Turn>;

            // Assert: בדיקה שהתוצאה היא רשימה
            Assert.NotNull(turns);
            Assert.IsType<List<Turn>>(turns);
        }
    }
}
