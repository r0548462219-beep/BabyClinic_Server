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

        // -----------------------------------------------------
        // טסט 1: בדיקת שליפת רשימה (GET ALL)
        // -----------------------------------------------------

        [Fact]
        public void GetTurns_ReturnsOkResult()
        {
            // Act
            var result = _controller.GetTurns();

            // Assert: בדיקה שהוחזר קוד 200 OK עם תוכן (OkObjectResult)
            Assert.IsType<OkObjectResult>(result);
        }

        // -----------------------------------------------------
        // טסט 2: בדיקת הוספה (POST) מוצלחת
        // -----------------------------------------------------

        [Fact]
        public void PostTurn_ValidTurn_ReturnsCreatedAtAction()
        {
            // Arrange: יצירת אובייקט תור חדש
            var newTurn = new Turn
            {
                Id = 50,
                BabyId = 1, // מזהה תינוק קיים
                NurseId = 2, // מזהה אחות קיימת
                DateTime = DateTime.Now.AddDays(7),
                Status = "נקבע"
            };

            // Act
            var result = _controller.PostTurn(newTurn);

            // Assert: בדיקה שהוחזר קוד 201 Created (CreatedAtActionResult)
            Assert.IsType<CreatedAtActionResult>(result);
        }

        // -----------------------------------------------------
        // טסט 3: בדיקת עדכון (PUT) של תור קיים
        // -----------------------------------------------------

        [Fact]
        public void PutTurn_ExistingId_ReturnsNoContent()
        {
            // Arrange: מזהה תור קיים (בהנחה ש-ID=1 קיים) ואובייקט עדכון
            int existingId = 1;
            var updatedTurn = new Turn
            {
                Id = 1,
                BabyId = 1,
                NurseId = 2,
                DateTime = DateTime.Now.AddDays(5),
                Status = "בוצע"
            };

            // Act: ביצוע עדכון
            var result = _controller.PutTurn(existingId, updatedTurn);

            // Assert: בדיקה שהוחזר קוד 204 No Content (עדכון מוצלח)
            Assert.IsType<NoContentResult>(result);
        }
    }
}
