using Xunit;
using Microsoft.AspNetCore.Mvc;
using BabyClinicAPI;
using System;
using System.Collections.Generic;
using BabyClinicAPI.Controllers;
using BabyClinicAPI.Entities;

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

        // -----------------------------------------------------
        // טסט 1: בדיקת שליפת רשימה (GET ALL)
        // -----------------------------------------------------

        [Fact]
        public void GetBabies_ReturnsOkResult()
        {
            // Act: קריאה לפונקציית שליפת הרשימה
            var result = _controller.GetBabies();

            // Assert: בדיקה שהוחזר קוד 200 OK עם תוכן (OkObjectResult)
            Assert.IsType<OkObjectResult>(result);
        }

        // -----------------------------------------------------
        // טסט 2: בדיקת הוספה (POST) מוצלחת
        // -----------------------------------------------------

        [Fact]
        public void PostBaby_ValidBaby_ReturnsCreatedAtAction()
        {
            // Arrange: יצירת אובייקט תינוק תקין להוספה
            var newBaby = new Baby
            {
                Id = 10,
                Name = "נוי",
                // *** זהו התיקון: שימוש בשם המדויק BirthDate ***
                BirthDate = DateTime.Now,
                ParentName = "הורה נוי",
                Phone = "0501234567", // הוספתי שדה Phone כפי שקיים ב-Entity שלך
                Status = "פעיל"
            };

            // Act: ביצוע ההוספה
            var result = _controller.PostBaby(newBaby);

            // Assert: בדיקה שהוחזר קוד 201 Created (CreatedAtActionResult)
            Assert.IsType<CreatedAtActionResult>(result);
        }

        // -----------------------------------------------------
        // טסט 3: בדיקת מחיקה (DELETE) של מזהה שאינו קיים
        // -----------------------------------------------------

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
    }
}