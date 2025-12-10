using Xunit;
using Microsoft.AspNetCore.Mvc;
using BabyClinicAPI;
using System;
using System.Collections.Generic;
using BabyClinicAPI.Controllers;
using BabyClinicAPI.Entities;
using System.Linq;

namespace BabyClinicAPI.Tests
{
    public class BabiesControllerTests
    {
        // Property פרטי readonly עבור הקונטרולר
        private readonly BabiesController _controller;

        // Constructor - מאתחל את המופע לפני כל טסט
        public BabiesControllerTests()
        {
            _controller = new BabiesController();
        }

        // =====================================================
        // טסטים עבור GET ALL
        // =====================================================

        [Fact]
        public void GetBabies_ReturnsOkResult()
        {
            // Act: קריאה לפונקציית שליפת הרשימה
            var actionResult = _controller.GetBabies();

            // Assert: בדיקה שהוחזר קוד 200 OK עם תוכן (OkObjectResult)
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetBabies_ReturnsAllBabies()
        {
            // Act
            var actionResult = _controller.GetBabies();
            var okResult = actionResult.Result as OkObjectResult;
            var babies = okResult.Value as IEnumerable<Baby>;

            // Assert: בדיקה שמוחזרות לפחות 2 תינוקות (הנתונים הראשוניים)
            Assert.NotNull(babies);
            Assert.True(babies.Count() >= 2);
        }

        // =====================================================
        // טסטים עבור GET BY ID
        // =====================================================

        [Fact]
        public void GetBaby_ExistingId_ReturnsOk()
        {
            // Arrange: מזהה תינוק קיים
            int existingId = 1;

            // Act
            var actionResult = _controller.GetBaby(existingId);

            // Assert: בדיקה שהוחזר קוד 200 OK
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetBaby_NonExistingId_ReturnsNotFound()
        {
            // Arrange: מזהה שלא קיים
            int nonExistingId = 999;

            // Act
            var actionResult = _controller.GetBaby(nonExistingId);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        // =====================================================
        // טסטים עבור POST (הוספה)
        // =====================================================

        [Fact]
        public void PostBaby_ValidBaby_ReturnsCreatedAtAction()
        {
            // Arrange: יצירת אובייקט תינוק תקין להוספה
            var newBaby = new Baby
            {
                Id = 0, // ה-ID יוחלף אוטומטית
                Name = "נוי",
                BirthDate = DateTime.Now,
                ParentName = "הורה נוי",
                Phone = "0501234567",
                Status = "פעיל"
            };

            // Act: ביצוע ההוספה
            var actionResult = _controller.PostBaby(newBaby);

            // Assert: בדיקה שהוחזר קוד 201 Created (CreatedAtActionResult)
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public void PostBaby_ValidBaby_ReturnsBabyWithNewId()
        {
            // Arrange
            var newBaby = new Baby
            {
                Id = 0,
                Name = "תמר",
                BirthDate = DateTime.Now.AddMonths(-1),
                ParentName = "הורי תמר",
                Phone = "0527778888",
                Status = "פעיל"
            };

            // Act
            var actionResult = _controller.PostBaby(newBaby);
            var createdResult = actionResult.Result as CreatedAtActionResult;
            var createdBaby = createdResult.Value as Baby;

            // Assert: בדיקה שהתינוק קיבל ID חדש
            Assert.NotNull(createdBaby);
            Assert.True(createdBaby.Id >= 3); // ה-ID הבא אחרי הנתונים הראשוניים
            Assert.Equal("תמר", createdBaby.Name);
        }

        // =====================================================
        // טסטים עבור PUT (עדכון מלא)
        // =====================================================

        [Fact]
        public void PutBaby_ExistingId_ReturnsNoContent()
        {
            // Arrange: עדכון תינוק קיים
            int existingId = 1;
            var updatedBaby = new Baby
            {
                Id = existingId,
                Name = "יוסי כהן - מעודכן",
                BirthDate = DateTime.Now.AddMonths(-6),
                ParentName = "אמא כהן",
                Phone = "050-1234567",
                Status = "פעיל"
            };

            // Act
            var result = _controller.PutBaby(existingId, updatedBaby);

            // Assert: בדיקה שהוחזר קוד 204 No Content (עדכון מוצלח)
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PutBaby_MismatchedId_ReturnsBadRequest()
        {
            // Arrange: ה-ID בנתיב שונה מה-ID באובייקט
            int pathId = 1;
            var updatedBaby = new Baby
            {
                Id = 99, // ID שונה!
                Name = "שם כלשהו",
                BirthDate = DateTime.Now,
                ParentName = "הורה",
                Phone = "050-1234567",
                Status = "פעיל"
            };

            // Act
            var result = _controller.PutBaby(pathId, updatedBaby);

            // Assert: בדיקה שהוחזר קוד 400 Bad Request
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutBaby_NonExistingId_ReturnsNotFound()
        {
            // Arrange: ניסיון לעדכן תינוק שלא קיים
            int nonExistingId = 999;
            var updatedBaby = new Baby
            {
                Id = nonExistingId,
                Name = "תינוק לא קיים",
                BirthDate = DateTime.Now,
                ParentName = "הורה",
                Phone = "050-1234567",
                Status = "פעיל"
            };

            // Act
            var result = _controller.PutBaby(nonExistingId, updatedBaby);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }

        // =====================================================
        // טסטים עבור DELETE (מחיקה)
        // =====================================================

        [Fact]
        public void DeleteBaby_ExistingId_ReturnsNoContent()
        {
            // Arrange: מחיקת תינוק קיים
            int existingId = 2;

            // Act
            var result = _controller.DeleteBaby(existingId);

            // Assert: בדיקה שהוחזר קוד 204 No Content (מחיקה מוצלחת)
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteBaby_NonExistingId_ReturnsNotFound()
        {
            // Arrange: מזהה לא קיים
            int nonExistingId = 999;

            // Act: ניסיון מחיקה
            var result = _controller.DeleteBaby(nonExistingId);

            // Assert: בדיקה שהוחזר קוד 404 Not Found (NotFoundResult)
            Assert.IsType<NotFoundResult>(result);
        }

        // =====================================================
        // טסטים עבור UPDATE STATUS (עדכון סטטוס)
        // =====================================================

        [Fact]
        public void UpdateBabyStatus_ExistingId_ReturnsNoContent()
        {
            // Arrange: עדכון סטטוס של תינוק קיים
            int existingId = 1;
            var updatedStatus = "לא פעיל";

            // Act
            var result = _controller.UpdateBabyStatus(existingId, updatedStatus);

            // Assert: בדיקה שהוחזר קוד 204 No Content
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateBabyStatus_NonExistingId_ReturnsNotFound()
        {
            // Arrange: ניסיון לעדכן סטטוס של תינוק שלא קיים
            int nonExistingId = 999;
            var updatedStatus = "לא פעיל";

            // Act
            var result = _controller.UpdateBabyStatus(nonExistingId, updatedStatus);

            // Assert: בדיקה שהוחזר קוד 404 Not Found
            Assert.IsType<NotFoundResult>(result);
        }
    }
}