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

        // =====================================================
        // טסטים עבור GET ALL
        // =====================================================

        [Fact]
        public void GetNurses_ReturnsOkResult()
        {
            // Act
            var actionResult = _controller.GetNurses();

            // Assert: בדיקה שהוחזר קוד 200 OK עם תוכן (OkObjectResult)
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetNurses_ReturnsAllNurses()
        {
            // Act
            var actionResult = _controller.GetNurses();
            var okResult = actionResult.Result as OkObjectResult;
            var nurses = okResult.Value as IEnumerable<Nurse>;

            // Assert: בדיקה שמוחזרות לפחות 2 אחיות (הנתונים הראשוניים)
            Assert.NotNull(nurses);
            Assert.True(nurses.Count() >= 2);
        }

        // =====================================================
        // טסטים עבור GET BY ID
        // =====================================================

        [Fact]
        public void GetNurse_ExistingId_ReturnsOk()
        {
            // Arrange: מזהה אחות קיימת
            int existingId = 10;

            // Act
            var actionResult = _controller.GetNurse(existingId);

            // Assert: בדיקה שהוחזר קוד 200 OK
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetNurse_NonExistingId_ReturnsNotFound()
        {
            // Arrange: מזהה שלא קיים
            int nonExistingId = 999;

            // Act
            var actionResult = _controller.GetNurse(nonExistingId);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        // =====================================================
        // טסטים עבור POST (הוספה)
        // =====================================================

        [Fact]
        public void PostNurse_ValidNurse_ReturnsCreatedAtAction()
        {
            // Arrange: יצירת אחות חדשה
            var newNurse = new Nurse
            {
                Id = 0, // ה-ID יוחלף אוטומטית
                Name = "שירה כהן",
                Specialty = "התפתחות הילד",
                Phone = "0543334444",
                Status = "פעילה"
            };

            // Act
            var actionResult = _controller.PostNurse(newNurse);

            // Assert: בדיקה שהוחזר קוד 201 Created (CreatedAtActionResult)
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public void PostNurse_ValidNurse_ReturnsNurseWithNewId()
        {
            // Arrange
            var newNurse = new Nurse
            {
                Id = 0,
                Name = "רחל לוי",
                Specialty = "ייעוץ הנקה",
                Phone = "0525556666",
                Status = "פעילה"
            };

            // Act
            var actionResult = _controller.PostNurse(newNurse);
            var createdResult = actionResult.Result as CreatedAtActionResult;
            var createdNurse = createdResult.Value as Nurse;

            // Assert: בדיקה שהאחות קיבלה ID חדש
            Assert.NotNull(createdNurse);
            Assert.True(createdNurse.Id >= 12); // ה-ID הבא אחרי הנתונים הראשוניים
            Assert.Equal("רחל לוי", createdNurse.Name);
        }

        // =====================================================
        // טסטים עבור PUT (עדכון מלא)
        // =====================================================

        [Fact]
        public void PutNurse_ExistingId_ReturnsNoContent()
        {
            // Arrange: עדכון אחות קיימת
            int existingId = 10;
            var updatedNurse = new Nurse
            {
                Id = existingId,
                Name = "דינה ישראלי - מעודכנת",
                Specialty = "שקילה ומדידה",
                Phone = "054-9990000",
                Status = "פעילה"
            };

            // Act
            var result = _controller.PutNurse(existingId, updatedNurse);

            // Assert: בדיקה שהוחזר קוד 204 No Content (עדכון מוצלח)
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PutNurse_MismatchedId_ReturnsBadRequest()
        {
            // Arrange: ה-ID בנתיב שונה מה-ID באובייקט
            int pathId = 10;
            var updatedNurse = new Nurse
            {
                Id = 99, // ID שונה!
                Name = "שם כלשהו",
                Specialty = "התמחות",
                Phone = "050-1234567",
                Status = "פעילה"
            };

            // Act
            var result = _controller.PutNurse(pathId, updatedNurse);

            // Assert: בדיקה שהוחזר קוד 400 Bad Request
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutNurse_NonExistingId_ReturnsNotFound()
        {
            // Arrange: ניסיון לעדכן אחות שלא קיימת
            int nonExistingId = 999;
            var updatedNurse = new Nurse
            {
                Id = nonExistingId,
                Name = "אחות לא קיימת",
                Specialty = "התמחות",
                Phone = "050-1234567",
                Status = "פעילה"
            };

            // Act
            var result = _controller.PutNurse(nonExistingId, updatedNurse);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }

        // =====================================================
        // טסטים עבור DELETE (מחיקה)
        // =====================================================

        [Fact]
        public void DeleteNurse_ExistingId_ReturnsNoContent()
        {
            // Arrange: מחיקת אחות קיימת
            int existingId = 11;

            // Act
            var result = _controller.DeleteNurse(existingId);

            // Assert: בדיקה שהוחזר קוד 204 No Content (מחיקה מוצלחת)
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteNurse_NonExistingId_ReturnsNotFound()
        {
            // Arrange: ניסיון למחוק אחות שלא קיימת
            int nonExistingId = 999;

            // Act
            var result = _controller.DeleteNurse(nonExistingId);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }

        // =====================================================
        // טסטים עבור UPDATE STATUS (עדכון סטטוס)
        // =====================================================

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

        [Fact]
        public void UpdateNurseStatus_NonExistingId_ReturnsNotFound()
        {
            // Arrange: ניסיון לעדכן סטטוס של אחות שלא קיימת
            int nonExistingId = 999;
            var updatedStatus = "בחופשה";

            // Act
            var result = _controller.UpdateNurseStatus(nonExistingId, updatedStatus);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
