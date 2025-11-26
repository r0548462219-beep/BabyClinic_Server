# 👶 מערכת לניהול תורים - תחנת טיפת חלב (RESTful API)

פרויקט זה מממש שירות **RESTful API** לניהול מלא של תורים, תינוקות ואחיות בתחנת טיפת חלב. המערכת מאפשרת פעולות CRUD סטנדרטיות (יצירה, קריאה, עדכון, מחיקה) וכן פעולות עדכון סטטוס ספציפיות.

## 🎯 ישויות מרכזיות (Entities)

| ישות | תיאור | מאפיינים עיקריים |
| :--- | :--- | :--- |
| **Baby** (תינוק) | פרטי התינוק המטופל ואיש קשר. | `BabyId`, `Name`, `DateOfBirth`, `ParentName`, `Phone`, `Status` (פעיל/לא פעיל). |
| **Nurse** (אחות) | פרטי האחות וסטטוס זמינותה. | `NurseId`, `Name`, `Specialization`, `Phone`, `WorkStatus` (פעילה/בחופשה/מושבתת). |
| **Turn** (תור) | פרטי התור שנקבע. | `TurnId`, `BabyId`, `NurseId`, `DateTime`, `Status` (נקבע/בוצע/בוטל). |

---

## 🗺️ תכנון ה-RESTful API (Endpoints)

כל ה-Endpoints משתמשים בבסיס כתובת (Base URL) דמיוני: `https://tipa.co.il`

### 1. תינוק - Baby (/babies)

| פעולה | HTTP Method | Route | תיאור |
| :--- | :--- | :--- | :--- |
| שליפת רשימה | `GET` | `/babies` | שליפת רשימה של כל התינוקות. |
| שליפת בודד | `GET` | `/babies/{id}` | שליפת תינוק לפי `BabyId`. **מחזיר 404 אם לא קיים.** |
| יצירה | `POST` | `/babies` | הוספת תינוק חדש. |
| עדכון מלא | `PUT` | `/babies/{id}` | עדכון פרטי תינוק קיים. **מחזיר 404 אם לא קיים.** |
| מחיקה | `DELETE` | `/babies/{id}` | מחיקת תינוק. **מחזיר 404 אם לא קיים.** |
| **עדכון סטטוס** | `PUT` | `/babies/{id}/status` | עדכון סטטוס התינוק (פעיל/לא פעיל). |

### 2. אחות - Nurse (/nurses)

| פעולה | HTTP Method | Route | תיאור |
| :--- | :--- | :--- | :--- |
| שליפת רשימה | `GET` | `/nurses` | שליפת רשימה של כל האחיות. |
| שליפת בודד | `GET` | `/nurses/{id}` | שליפת אחות לפי `NurseId`. **מחזיר 404 אם לא קיימת.** |
| יצירה | `POST` | `/nurses` | הוספת אחות חדשה. |
| עדכון מלא | `PUT` | `/nurses/{id}` | עדכון פרטי אחות קיימת. **מחזיר 404 אם לא קיימת.** |
| מחיקה | `DELETE` | `/nurses/{id}` | מחיקת אחות. **מחזיר 404 אם לא קיימת.** |
| **עדכון סטטוס** | `PUT` | `/nurses/{id}/status` | עדכון סטטוס עבודה של האחות (פעילה/בחופשה/מושבתת). |

### 3. תור - Turn (/turns)

| פעולה | HTTP Method | Route | תיאור |
| :--- | :--- | :--- | :--- |
| שליפת רשימה | `GET` | `/turns` | שליפת רשימה של כל התורים. |
| שליפת בודד | `GET` | `/turns/{id}` | שליפת תור לפי `TurnId`. **מחזיר 404 אם לא קיים.** |
| יצירה | `POST` | `/turns` | קביעת תור חדש. |
| עדכון מלא | `PUT` | `/turns/{id}` | עדכון פרטי תור קיים. **מחזיר 404 אם לא קיים.** |
| מחיקה | `DELETE` | `/turns/{id}` | ביטול/מחיקת תור. **מחזיר 404 אם לא קיים.** |
| **שליפה לפי תאריך**| `GET` | `/turns/by-date?date={yyyy-mm-dd}` | שליפת כל התורים בתאריך ספציפי. |

---

## 💻 מבנה פרויקט ה-Web API (C#)

### תיקיית Entities

#### 📝 Baby.cs (דוגמה)

```csharp
namespace TlpaApi.Entities
{
    public class Baby
    {
        public int BabyId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ParentName { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; } // לדוגמה: "פעיל", "לא פעיל"
    }
}
