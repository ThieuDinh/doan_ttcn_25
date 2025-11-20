# Hướng dẫn Cài đặt và Chạy Dự án ASP.NET Core

Đây là tài liệu hướng dẫn giúp bạn cài đặt và chạy dự án trên máy của mình.

## Yêu cầu môi trường

Để có thể chạy được dự án này, bạn cần cài đặt các công cụ và môi trường sau:

1.  **[.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)**: Đảm bảo bạn đã cài đặt phiên bản .NET 9.0 SDK.
2.  **[Visual Studio Code](https://code.visualstudio.com/)**: Trình soạn thảo mã nguồn nhẹ và mạnh mẽ.
    *   Cài đặt extension **C#** của Microsoft để hỗ trợ phát triển .NET.
3.  **[SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)**: Một phiên bản của SQL Server (ví dụ: SQL Server Express, SQL Server Developer) để lưu trữ cơ sở dữ liệu.

## Các bước cài đặt

### 1. Tải mã nguồn

Clone repository này về máy của bạn bằng lệnh sau:

```bash
git clone <URL_CUA_REPOSITORY>
cd doan_ttcn
```

### 2. Cấu hình Connection String

Mở file `appsettings.json` và cập nhật chuỗi kết nối `DefaultConnection` để trỏ đến instance SQL Server của bạn.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TEN_SERVER_SQL;Database=TenCSDL;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
//...
```

-   `Server`: Tên SQL Server instance của bạn (ví dụ: `.` hoặc `(localdb)\\mssqllocaldb`).
-   `Database`: Tên cơ sở dữ liệu bạn muốn tạo cho dự án.

### 3. Áp dụng Database Migrations

Mở terminal trong Visual Studio Code (Ctrl + `) và chạy lệnh sau để tạo cơ sở dữ liệu và các bảng cần thiết:

```bash
dotnet ef database update
```

### 4. Chạy ứng dụng

Bây giờ bạn có thể chạy dự án bằng cách sử dụng .NET CLI trong terminal của VS Code:

```bash
dotnet run
```

Ứng dụng sẽ khởi động và bạn có thể truy cập vào địa chỉ `https://localhost:<port>` được hiển thị trong terminal.

Chúc bạn thành công!
