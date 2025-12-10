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
    public class NursesControllerTests
    {
        private readonly NursesController _controller;

        public NursesControllerTests()
        {
            // אתחול הקונטרולר לפני כל בדיקה
            _controller = new NursesController();
        }

        // -----------------------------------------------------
        // טסט 1: בדיקת שליפת רשימה (GET ALL)
        // -----------------------------------------------------

        [Fact]
        public void GetNurses_ReturnsOkResult()
        {
            // Act
            var result = _controller.GetNurses();

            // Assert: בדיקה שהוחזר קוד 200 OK עם תוכן (OkObjectResult)
            Assert.IsType<OkObjectResult>(result);
        }

        // -----------------------------------------------------
        // טסט 2: בדיקת הוספה (POST) מוצלחת
        // -----------------------------------------------------

        [Fact]
        public void PostNurse_ValidNurse_ReturnsCreatedAtAction()
        {
            // Arrange: יצירת אחות חדשה
            var newNurse = new Nurse
            {
                Id = 5,
                Name = "שירה כהן",
                Specialty = "ייעוץ הנקה",
                Phone = "0543334444",
                Status = "פעילה"
            };

            // Act
            var result = _controller.PostNurse(newNurse);

            // Assert: בדיקה שהוחזר קוד 201 Created (CreatedAtActionResult)
            Assert.IsType<CreatedAtActionResult>(result);
        }

        // -----------------------------------------------------
        // טסט 3: בדיקת עדכון סטטוס אחות קיימת (PUT)
        // -----------------------------------------------------

        [Fact]
        public void UpdateNurseStatus_ExistingId_ReturnsNoContent()
        {
            // Arrange: מזהה אחות קיימת (בהנחה ש-ID=10 קיים) וסטטוס חדש
            int existingId = 10;
            var updatedStatus = "בחופשה";

            // Act: קריאה לפונקציה לעדכון סטטוס
            var result = _controller.UpdateNurseStatus(existingId, updatedStatus);

            // Assert: בדיקה שהוחזר קוד 204 No Content (עדכון מוצלח)
            Assert.IsType<NoContentResult>(result);
        }
    }
}
