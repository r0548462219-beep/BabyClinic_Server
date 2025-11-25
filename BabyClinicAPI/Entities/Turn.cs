namespace BabyClinicAPI.Entities
{
    public class Turn
    {
        // מזהה תור
        public int Id { get; set; }
        // מזהה תינוק (מפתח זר)
        public int BabyId { get; set; }
        // מזהה אחות (מפתח זר)
        public int NurseId { get; set; }
        // תאריך ושעה
        public DateTime DateTime { get; set; }
        // סטטוס (נקבע / בוצע / בוטל)
        public string Status { get; set; }
    }
}
