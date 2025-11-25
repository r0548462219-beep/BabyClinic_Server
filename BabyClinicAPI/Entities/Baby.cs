namespace BabyClinicAPI.Entities
{
    public class Baby
    {
        // מזהה תינוק
        public int Id { get; set; }
        // שם
        public string Name { get; set; }
        // תאריך לידה
        public DateTime BirthDate { get; set; }
        // שם הורה / איש קשר
        public string ParentName { get; set; }
        // טלפון
        public string Phone { get; set; }
        // סטטוס (פעיל / לא פעיל)
        public string Status { get; set; }
    }
}
