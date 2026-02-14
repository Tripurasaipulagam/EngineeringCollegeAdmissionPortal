# Engineering CollegAdmissionPortal

## 📌 Project Description
Online Admission Portal built with ASP.NET Core Web API and MVC. Supports student admission, profile photo upload, filtering by gender and course, duplicate email validation, and proper database relationships using Entity Framework Core.


The system allows:
- Adding new students
- Uploading profile photos
- Filtering students by Gender and Course
- Duplicate email validation
- Proper form validations

---

## 🏗️ Architecture

Solution contains two projects:

1. EngineeringCollegeAPI
   - Web API
   - Entity Framework Core
   - SQL Server Database
   - Handles business logic and data access

2. EngineeringCollegeWeb
   - MVC Web Application
   - Consumes API using HttpClientFactory
   - Displays UI and handles user interaction

MVC project depends completely on API.

---

## 🗄️ Database Design

Tables:
- Students
- Courses

Relationships:
- One Course → Many Students
- CourseId is Foreign Key in Students table
- Email has Unique Index

---

## ✨ Features

- Add Student
- Upload Profile Photo
- Duplicate Email Validation
- Mandatory Field Validation
- Filter by Gender
- Filter by Course
- Display Student List

---

## 🛠️ Technologies Used

- .NET 10
- ASP.NET Core Web API
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- HttpClientFactory

---

## 🎥 Demo Video

[Click Here to Watch Demo](https://drive.google.com/file/d/1xTTKMc_NGD5qzw33ItSgw6eNdP8N2Oin/view?usp=sharing)

---

## ⚙️ How To Run

1. Clone repository
2. Update Connection String in API project
3. Run API project
4. Run MVC project
5. Access MVC UI

---

## 👨‍💻 Developed By

Pulagam Tripura Sai
