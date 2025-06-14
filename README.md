# 📄 Docmino – Electronic Incoming/Outgoing Document Management

**Docmino** is a web-based platform designed to manage incoming, outgoing, and internal documents, fully complying with [Decree No. 30/2020/NĐ‑CP](https://thuvienphapluat.vn/van-ban/bo-may-hanh-chinh/Nghi-dinh-30-2020-ND-CP-cong-tac-van-thu-431077.aspx). The system supports lifecycle features such as approval workflows, digital signing, archival classification, and role-based access control.

---

## 📌 Project Overview

This capstone project, initiated in **March** and completed over two months, organizes the **Admin**, **Clerical Assistant**, and **Approver** roles into a seamless document workflow. The design mirrors real office operations and ensures transparency, security, and auditability.

---

## ✨ Key Features

### 📂 Document Management
- Handle **incoming**, **outgoing**, and **internal** documents  
- Support multiple approval levels (**user**, **group**, **department**, **position**)  
- Configure **PDF signature placement** before finalization  
- Electronically sign with **.pfx digital certificates**  
- Track complete **approval and signature history**  
- Future-ready: **Cardano blockchain-based** approval tracking (in development)

### 📦 Archival Module
- Organize storage hierarchically: **Warehouse → Shelf → Box → Dossier**  
- Enforce **document retention periods** per regulations

### 🗂 Classification Module
- Manage taxonomies: **Document types**, **registration log**, **issuing authority**, **fields**, **retention levels**

### 👥 User & Permission Module
- Administer user accounts and roles  
- Assign permissions at feature-level  
- Maintain action logs for traceability

---

## 🔧 Tech Stack & Architecture

- **Language**: C#  
- **Framework**: ASP.NET Core 8.0 Web API  
- **Architecture**: Clean Architecture (Domain, Application, Persistence, Infrastructure, Presentation)  
- **Response Patterns**: Unified `Result<T>` object  
- **Dependency Injection**: ASP.NET Core Native DI  

### 📦 Libraries & Tools
- EF Core (ORM & migrations)  
- JWT (authentication)  
- Coravel (background jobs + scheduling)  
- MailKit/MimeKit (email services)  
- SignalR (real-time notifications)  
- CardanoSharp (blockchain integration)  
- Cloudinary (file storage)  

### 🔧 Techniques Implemented
- Auditing via custom **interceptors** (e.g., `CreatedAt`, `UpdatedBy`)  
- Custom **Authentication & Authorization middleware**  
- Built-in **Background Services** for scheduled tasks  
- Integration with **Cloud file storage** and **.pfx certificate handling**

---

## 🚀 Setup Guide

## 🛠 Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/hugnt/.NET-Docmino-IncomingOutgoingDocumentManagement.git
   ```

2. Open the solution in **Visual Studio 2022**

---

## ⚙ Configuration

Update your `appsettings.json` with the following format:

```json
{
  "ConnectionStrings": {
    "Database": "Data Source=(Localdb)\\MSSQLLocalDB;Initial Catalog=DocminoV6;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "TokenSettings": {
    "SecretKey": "YOUR_SECRET_KEY",
    "AccessTokenExpirationInMinutes": 30,
    "RefreshTokenExpirationInMinutes": 50,
    "Issuer": "",
    "Audience": ""
  },
  "EmailSettings": {
    "Email": "your@email.com",
    "Password": "your-email-password",
    "Host": "smtp.gmail.com",
    "DisplayName": "Docmino - Document Management System",
    "Port": 587
  },
  "FileStorageSettings": {
    "Path": "D:\\Path\\To\\Your\\Files",
    "Test": "D:\\Path\\To\\Your\\Samples",
    "BaseFilePath": "/files",
    "BaseSamplePath": "/samples"
  },
  "CloudinarySettings": {
    "UrlFormat": "https://res.cloudinary.com/{cloudName}/{resourceType}/upload/{folderPath}/{fileName}",
    "CloudName": "your-cloud-name",
    "ApiKey": "your-api-key",
    "ApiSecret": "your-api-secret",
    "ExpireSignatureMinutes": 1,
    "MediaSettings": {
      "Certificates": {
        "AllowedExtensions": [ ".pfx" ],
        "MaxSizeBytes": 5242880,
        "FolderPath": "docmino/certificates"
      },
      "SignedDocuments": {
        "AllowedExtensions": [ ".pdf" ],
        "MaxSizeBytes": 10485760,
        "FolderPath": "docmino/signed-documents"
      },
      "Images": {
        "AllowedExtensions": [ ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg" ],
        "MaxSizeBytes": 5242880,
        "FolderPath": "docmino/images"
      },
      "Videos": {
        "AllowedExtensions": [ ".mp4", ".avi", ".mov", ".wmv", ".webm" ],
        "MaxSizeBytes": 104857600,
        "FolderPath": "docmino/videos"
      },
      "Documents": {
        "AllowedExtensions": [ ".pdf", ".docx", ".doc", ".xlsx", ".xls", ".pptx", ".ppt", ".txt" ],
        "MaxSizeBytes": 10485760,
        "FolderPath": "docmino/documents"
      }
    }
  }
}
```

> 🔒 Be sure to add `appsettings*.json` to `.gitignore` to avoid committing sensitive information.

---

## ▶ Execution

- Set `Docmino.API` as the **startup project**
- Run **migrations** or seed data if needed
- Start the app and open **Swagger UI** to test the API endpoints

---

## 🧪 Test Credentials

Use the following test account to log in:

- **Username**: `admin`  
- **Password**: `Admin@123`

---

## 💬 Support & Feedback

- Found a bug or want to suggest a feature?  
  👉 Open an issue on [GitHub](https://github.com/hugnt/.NET-Docmino-IncomingOutgoingDocumentManagement/issues)

- Contact us:
  - 📧 **Email**: thanh.hung.st302@gmail.com  
  - 📘 **Facebook**: [@docmino.devteam](https://facebook.com/hugnt.vn)

---

© 2025 **Docmino** 
